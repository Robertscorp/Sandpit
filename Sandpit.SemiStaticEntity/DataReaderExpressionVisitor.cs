using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Sandpit.SemiStaticEntity
{

    public class DataReaderExpressionVisitor : ExpressionVisitor
    {

        #region - - - - - - Fields - - - - - -

        private readonly Expression m_QueryContextParameter;
        private readonly Expression m_TypeMappingExpression;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DataReaderExpressionVisitor(Expression queryContextParameter, IAnnotation typeMappingAnnotation)
        {
            if (typeMappingAnnotation is null)
                throw new ArgumentNullException(nameof(typeMappingAnnotation));

            this.m_TypeMappingExpression = Expression.Property(Expression.Constant(typeMappingAnnotation.Value), nameof(StaticEntityTypeMapping<object, object>.ToStaticEntity));
            this.m_QueryContextParameter = queryContextParameter ?? throw new ArgumentNullException(nameof(queryContextParameter));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Object.Type == typeof(DbDataReader) && node.Method.Name == nameof(DbDataReader.GetFieldValue))
            {
                var _ConverterInputType = this.m_TypeMappingExpression.Type.GenericTypeArguments[1];
                if (_ConverterInputType != node.Method.ReturnType)
                    node = Expression.Call(
                        node.Object,
                        node.Method.GetGenericMethodDefinition().MakeGenericMethod(_ConverterInputType),
                        node.Arguments.ToArray());
            }

            return Expression.Invoke(
                this.m_TypeMappingExpression,
                Expression.Property(this.m_QueryContextParameter, nameof(QueryContext.Context)),
                base.VisitMethodCall(node));
        }

        #endregion Methods

    }

}
