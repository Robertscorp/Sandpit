using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Sandpit.SemiStaticEntity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace Sandpit.Console.Persistence
{

    public class PersistenceContext : DbContext
    {

        #region - - - - - - Constructors - - - - - -

        public PersistenceContext(DbContextOptions<PersistenceContext> dbContextOptions) : base(dbContextOptions) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => base.OnConfiguring(optionsBuilder.WithStaticEntities());

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => base.OnModelCreating(modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceContext).Assembly));

        #endregion Methods

    }



    // TODO: Remove once ProjectionColumns behaviour is figured out.
    // Note: This class is not used.
    public class RelationalProjectionBindingRemovingExpressionVisitor : ExpressionVisitor
    {
        private static readonly MethodInfo s_IsDbNullMethod =
            typeof(DbDataReader).GetRuntimeMethod(nameof(DbDataReader.IsDBNull), new[] { typeof(int) });

        private readonly SelectExpression m_SelectExpression;
        private readonly ParameterExpression m_DbDataReaderParameter;
        private readonly ParameterExpression m_IndexMapParameter;
        private readonly ParameterExpression m_QueryContextParameter;

        private readonly IDictionary<ParameterExpression, IDictionary<IProperty, int>> m_MaterializationContextBindings
            = new Dictionary<ParameterExpression, IDictionary<IProperty, int>>();

        public RelationalProjectionBindingRemovingExpressionVisitor(
            SelectExpression selectExpression,
            ParameterExpression dbDataReaderParameter,
            ParameterExpression indexMapParameter,
            ParameterExpression queryContextParameter,
            bool buffer)
        {
            this.m_SelectExpression = selectExpression;
            this.m_DbDataReaderParameter = dbDataReaderParameter;
            this.m_IndexMapParameter = indexMapParameter;
            this.m_QueryContextParameter = queryContextParameter;
            if (buffer)
            {
                this.ProjectionColumns = new ReaderColumn[selectExpression.Projection.Count];
            }
        }

        private ReaderColumn[] ProjectionColumns { get; }

        public virtual Expression Visit(Expression node, out IReadOnlyList<ReaderColumn> projectionColumns)
        {
            var _Result = this.Visit(node);
            projectionColumns = this.ProjectionColumns;
            return _Result;
        }

        protected override Expression VisitBinary(BinaryExpression binaryExpression)
        {
            if (binaryExpression.NodeType == ExpressionType.Assign
                && binaryExpression.Left is ParameterExpression _ParameterExpression
                && _ParameterExpression.Type == typeof(MaterializationContext))
            {
                var _NewExpression = (NewExpression)binaryExpression.Right;
                var _ProjectionBindingExpression = (ProjectionBindingExpression)_NewExpression.Arguments[0];

                this.m_MaterializationContextBindings[_ParameterExpression]
                    = (IDictionary<IProperty, int>)this.GetProjectionIndex(_ProjectionBindingExpression);

                //var _UpdatedExpression = Expression.New(
                //    _NewExpression.Constructor,
                //    Expression.Constant(ValueBuffer.Empty),
                //    _NewExpression.Arguments[1]);

                //return Expression.MakeBinary(ExpressionType.Assign, binaryExpression.Left, _UpdatedExpression);
            }

            //if (binaryExpression.NodeType == ExpressionType.Assign
            //    && binaryExpression.Left is MemberExpression _MemberExpression
            //    && _MemberExpression.Member is FieldInfo _FieldInfo
            //    && _FieldInfo.IsInitOnly)
            //{
            //    return _MemberExpression.Assign(this.Visit(binaryExpression.Right));
            //}

            return base.VisitBinary(binaryExpression);
        }

        protected override Expression VisitMethodCall(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method.IsGenericMethod
                && methodCallExpression.Method.GetGenericMethodDefinition() == EntityMaterializerSource.TryReadValueMethod)
            {
                var _Property = (IProperty)((ConstantExpression)methodCallExpression.Arguments[2]).Value;
                var _PropertyProjectionMap = methodCallExpression.Arguments[0] is ProjectionBindingExpression _ProjectionBindingExpression
                    ? (IDictionary<IProperty, int>)this.GetProjectionIndex(_ProjectionBindingExpression)
                    : m_MaterializationContextBindings[
                        (ParameterExpression)((MethodCallExpression)methodCallExpression.Arguments[0]).Object];

                var _ProjectionIndex = _PropertyProjectionMap[_Property];
                var _Projection = m_SelectExpression.Projection[_ProjectionIndex];

                var _X = this.CreateGetValueExpression(
                    _Property,
                    _ProjectionIndex,
                    IsNullableProjection(_Projection),
                    _Property.GetRelationalTypeMapping(),
                    methodCallExpression.Type);

                if (_X != null)
                    return _X;
            }

            return base.VisitMethodCall(methodCallExpression);
        }

        //protected override Expression VisitExtension(Expression extensionExpression)
        //{
        //    if (extensionExpression is ProjectionBindingExpression _ProjectionBindingExpression)
        //    {
        //        var _ProjectionIndex = (int)this.GetProjectionIndex(_ProjectionBindingExpression);
        //        var _Projection = this.m_SelectExpression.Projection[_ProjectionIndex];

        //        return this.CreateGetValueExpression(
        //            m_DbDataReaderParameter,
        //            _ProjectionIndex,
        //            IsNullableProjection(_Projection),
        //            _Projection.Expression.TypeMapping,
        //            _ProjectionBindingExpression.Type);
        //    }

        //    return base.VisitExtension(extensionExpression);
        //}

        private object GetProjectionIndex(ProjectionBindingExpression projectionBindingExpression)
            => projectionBindingExpression.ProjectionMember != null
                ? ((ConstantExpression)m_SelectExpression.GetMappedProjection(projectionBindingExpression.ProjectionMember)).Value
                : (projectionBindingExpression.Index != null
                    ? (object)projectionBindingExpression.Index
                    : projectionBindingExpression.IndexMap);

        private static bool IsNullableProjection(ProjectionExpression projection)
            => !(projection.Expression is ColumnExpression _Column) || _Column.IsNullable;

        private Expression CreateGetValueExpression(
            IProperty property,
            int index,
            bool nullable,
            RelationalTypeMapping typeMapping,
            Type clrType)
        {
            var _TypeMapping2 = property.FindAnnotation("TypeMapping2");
            if (_TypeMapping2 == null)
                return null;

            var _GetMethod = typeMapping.GetDataReaderMethod();

            Expression _IndexExpression = Expression.Constant(index);
            if (this.m_IndexMapParameter != null)
                _IndexExpression = Expression.ArrayIndex(m_IndexMapParameter, _IndexExpression);

            Expression _ValueExpression
                = Expression.Call(
                    _GetMethod.DeclaringType != typeof(DbDataReader)
                        ? Expression.Convert(this.m_DbDataReaderParameter, _GetMethod.DeclaringType)
                        : (Expression)this.m_DbDataReaderParameter,
                    _GetMethod,
                    _IndexExpression);

            if (this.ProjectionColumns != null)
            {
                var _ColumnType = _ValueExpression.Type;
                if (!_ColumnType.IsValueType
                    || !BufferedDataReader.IsSupportedValueType(_ColumnType))
                {
                    _ColumnType = typeof(object);
                    _ValueExpression = Expression.Convert(_ValueExpression, typeof(object));
                }

                if (this.ProjectionColumns[index] == null)
                    this.ProjectionColumns[index] = ReaderColumn.Create(
                        _ColumnType,
                        nullable,
                        m_IndexMapParameter != null ? ((ColumnExpression)m_SelectExpression.Projection[index].Expression).Name : null,
                        Expression.Lambda(
                            _ValueExpression,
                            this.m_DbDataReaderParameter,
                            m_IndexMapParameter ?? Expression.Parameter(typeof(int[]))).Compile());

                if (_GetMethod.DeclaringType != typeof(DbDataReader))
                {
                    _ValueExpression
                        = Expression.Call(
                            this.m_DbDataReaderParameter,
                            RelationalTypeMapping.GetDataReaderMethod(_ColumnType),
                            _IndexExpression);
                }
            }

            _ValueExpression = typeMapping.CustomizeDataReaderExpression(_ValueExpression);

            //var _ModelType = _TypeMapping2.Value.GetType().GetGenericArguments()[0];
            //var _ProviderType = _TypeMapping2.Value.GetType().GetGenericArguments()[1];

            var _X = Expression.Constant(_TypeMapping2.Value);
            var _Y = Expression.Property(this.m_QueryContextParameter, nameof(QueryContext.Context));
            var _Z = Expression.Property(_X, "ToModel");

            _ValueExpression = Expression.Invoke(_Z, _Y, _ValueExpression);

            //_ValueExpression = Expression.Call(Expression.Property(_Z, "Target"), "Method", new Type[] { typeof(DbContext), _ProviderType, _ModelType }, _Y, _ValueExpression);


            //_ValueExpression = Expression.Call(_Z, "Invoke", new Type[] { typeof(DbContext), _ProviderType, _ModelType }, _Y, _ValueExpression);

            //var _Converter = typeMapping.Converter;

            //if (_Converter != null)
            //{
            //    if (_ValueExpression.Type != _Converter.ProviderClrType)
            //        _ValueExpression = Expression.Convert(_ValueExpression, _Converter.ProviderClrType);

            //    _ValueExpression = ReplacingExpressionVisitor.Replace(
            //        _Converter.ConvertFromProviderExpression.Parameters.Single(),
            //        _ValueExpression,
            //        _Converter.ConvertFromProviderExpression.Body);
            //}

            if (_ValueExpression.Type != clrType)
                _ValueExpression = Expression.Convert(_ValueExpression, clrType);

            if (nullable)
                _ValueExpression
                    = Expression.Condition(
                        Expression.Call(this.m_DbDataReaderParameter, s_IsDbNullMethod, _IndexExpression),
                        Expression.Default(_ValueExpression.Type),
                        _ValueExpression);

            return _ValueExpression;
        }
    }

}
