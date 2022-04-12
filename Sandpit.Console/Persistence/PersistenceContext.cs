using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

// ---------------------------------------
// TODO: Consider Decorating RelationalTypeMapping and overriding CustomizeDataReaderExpression to inject the custom converter.

// ---------------------------------------

namespace Sandpit.Console.Persistence
{

    //public class XXX : DbContextOptionsExtensionInfo
    //{
    //    public XXX(IDbContextOptionsExtension extension) : base(extension)
    //    {
    //    }

    //    public override bool IsDatabaseProvider
    //        => false;

    //    public override string LogFragment => "No";

    //    public override long GetServiceProviderHashCode() => 0;
    //    public override void PopulateDebugInfo(IDictionary<string, string> debugInfo) { }

    //}

    //public class XX : IDbContextOptionsExtension
    //{

    //    private readonly IDbContextOptionsExtension m_DbContextOptionsExtension;

    //    public XX(IDbContextOptionsExtension dbContextOptionsExtension)
    //        => this.m_DbContextOptionsExtension = dbContextOptionsExtension;

    //    DbContextOptionsExtensionInfo IDbContextOptionsExtension.Info
    //        => new XXX(this.m_DbContextOptionsExtension);

    //    void IDbContextOptionsExtension.ApplyServices(IServiceCollection services)
    //    {
    //        var _X = services.Single(s => s.ServiceType == typeof(IValueConverterSelector));
    //        _ = services.Remove(_X);
    //        _ = services.AddScoped<IValueConverterSelector, ValueConverterSelector>();

    //        _X = services.Single(s => s.ServiceType == typeof(IValueConverterSelector));


    //        //throw new NotImplementedException();
    //    }

    //    void IDbContextOptionsExtension.Validate(IDbContextOptions options)
    //    {
    //        //options.FindExtension<CoreOptionsExtension>().
    //    }

    //}

    public class XXConverter<TModel, TProvider>// : ValueConverter<TModel, TProvider>
    {

        public XXConverter(
            Func<DbContext, TProvider, TModel> toModel,
            Func<DbContext, TModel, TProvider> toProvider)//,
                                                          //ConverterMappingHints mappingHints = null)
                                                          //: base(default, default, mappingHints)
        {
            this.ToModel = toModel;
            this.ToProvider = toProvider;
        }

        public Func<DbContext, TProvider, TModel> ToModel { get; }

        public Func<DbContext, TModel, TProvider> ToProvider { get; }

    }

    public class PersistenceContext : DbContext
    {

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; } = Guid.NewGuid();

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public PersistenceContext(DbContextOptions<PersistenceContext> dbContextOptions) : base(dbContextOptions) { }

        #endregion Constructors


        //public override DbSet<TEntity> Set<TEntity>()
        //    => typeof(TEntity) == typeof(Child)
        //        ? (new ChildSet() as DbSet<TEntity>)!
        //        : base.Set<TEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceContext).Assembly);

            //_ = modelBuilder.Ignore<SemiStaticEntity>();
            //_ = modelBuilder.Ignore<EncapsulateParent1>();
            //_ = modelBuilder.Ignore<EncapsulateParent2>();

            base.OnModelCreating(modelBuilder);


        }

        public override TEntity Find<TEntity>(params object[] keyValues)
            => base.Find<TEntity>(keyValues);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //_ = optionsBuilder.ReplaceService<IDbContextServices, TestDbContextServices>();

            //var _ServiceCollection = new ServiceCollection();

            //foreach (var _X in optionsBuilder.Options.Extensions)
            //    _X.ApplyServices(_ServiceCollection);

            //_ = optionsBuilder.UseInternalServiceProvider(_ServiceCollection.BuildServiceProvider());

            //_ = optionsBuilder.ReplaceService<IValueConverterSelector, ValueConverterSelector>();

            _ = optionsBuilder.ReplaceService<IShapedQueryCompilingExpressionVisitorFactory, ShapedQueryCompilingExpressionVisitorFactoryDecorator>();

            //optionsBuilder.EnableServiceProviderCaching(false);

            //optionsBuilder.
            _ = 0;

            base.OnConfiguring(optionsBuilder);
        }

    }

    //public class TestDbContextServices : DbContextServices
    //{

    //    #region - - - - - - Methods - - - - - -

    //    public override IDbContextServices Initialize(
    //        IServiceProvider scopedProvider,
    //        IDbContextOptions contextOptions,
    //        DbContext context)
    //        => base.Initialize(new ServiceProviderDecorator(context, scopedProvider), contextOptions, context);

    //    #endregion Methods

    //}

    //public class ServiceProviderDecorator : IServiceProvider
    //{

    //    private readonly DbContext m_Context;
    //    private readonly IServiceProvider m_ServiceProvider;

    //    public ServiceProviderDecorator(DbContext context, IServiceProvider serviceProvider)
    //    {
    //        this.m_Context = context;
    //        this.m_ServiceProvider = serviceProvider;
    //    }

    //    object IServiceProvider.GetService(Type serviceType)
    //    {
    //        //if (serviceType == typeof(IValueConverterSelector))
    //        //    return new ValueConverterSelector(
    //        //        this.m_Context,
    //        //        (IValueConverterSelector)this.m_ServiceProvider.GetService(serviceType)!);

    //        if (serviceType == typeof(IShapedQueryCompilingExpressionVisitorFactory))
    //            return new ShapedQueryCompilingExpressionVisitorFactoryDecorator(
    //                (ShapedQueryCompilingExpressionVisitorDependencies)
    //                    this.m_ServiceProvider.GetService(typeof(ShapedQueryCompilingExpressionVisitorDependencies)),
    //                (IShapedQueryCompilingExpressionVisitorFactory)
    //                    this.m_ServiceProvider.GetService(typeof(IShapedQueryCompilingExpressionVisitorFactory)));

    //        return this.m_ServiceProvider.GetService(serviceType);
    //        throw new NotImplementedException();
    //    }
    //}


    public class ShapedQueryCompilingExpressionVisitorFactoryDecorator : IShapedQueryCompilingExpressionVisitorFactory
    {

        #region - - - - - - Fields - - - - - -

        private readonly ShapedQueryCompilingExpressionVisitorDependencies m_Dependencies;
        private readonly RelationalShapedQueryCompilingExpressionVisitorDependencies m_RelationalDependencies;

        //private readonly IShapedQueryCompilingExpressionVisitorFactory m_QueryCompilingExpressionVisitorFactory;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ShapedQueryCompilingExpressionVisitorFactoryDecorator(
            ShapedQueryCompilingExpressionVisitorDependencies dependencies,
            RelationalShapedQueryCompilingExpressionVisitorDependencies relationalDependencies)
        {
            this.m_Dependencies = dependencies;
            this.m_RelationalDependencies = relationalDependencies;
            //this.m_QueryCompilingExpressionVisitorFactory
            //    = new RelationalShapedQueryCompilingExpressionVisitorFactory(dependencies, relationalDependencies);// queryCompilingExpressionVisitorFactory;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        ShapedQueryCompilingExpressionVisitor IShapedQueryCompilingExpressionVisitorFactory.Create(
            QueryCompilationContext queryCompilationContext)
            => new SemiStaticEntityShapedQueryVisitor(
                this.m_Dependencies,
                this.m_RelationalDependencies,
                queryCompilationContext);

        #endregion Methods

    }

    public class SemiStaticEntityShapedQueryVisitor : RelationalShapedQueryCompilingExpressionVisitor
    {

        #region - - - - - - Fields - - - - - -

        private ShapedQueryExpression m_ShapedQueryExpression;
        private IReadOnlyList<ReaderColumn> m_ProjectionColumns;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public SemiStaticEntityShapedQueryVisitor(
            ShapedQueryCompilingExpressionVisitorDependencies dependencies,
            RelationalShapedQueryCompilingExpressionVisitorDependencies relationalDependencies,
            QueryCompilationContext queryCompilationContext)
            : base(dependencies, relationalDependencies, queryCompilationContext)
        {
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        protected override Expression InjectEntityMaterializers(Expression expression)
        {
            var _SelectExpression = (SelectExpression)this.m_ShapedQueryExpression.QueryExpression;
            var _LambdaExpression = (LambdaExpression)expression;
            var _DataReaderParameter = _LambdaExpression.Parameters.Single(p => p.Type == typeof(DbDataReader));
            var _IndexMapParameter = _LambdaExpression.Parameters.Single(p => p.Type == typeof(int[]));
            var _QueryContextParameter = _LambdaExpression.Parameters.Single(p => p.Type == typeof(QueryContext));

            var _Expression = base.InjectEntityMaterializers(expression);

            // 100% this is the wrong place to be injecting this behaviour... but there's not really an alternative.
            _Expression = new RelationalProjectionBindingRemovingExpressionVisitor(
                    _SelectExpression,
                    _DataReaderParameter,
                    _SelectExpression.IsNonComposedFromSql() ? _IndexMapParameter : null,
                    _QueryContextParameter,
                    this.IsBuffering)
                .Visit(_Expression, out this.m_ProjectionColumns);


            return _Expression;
        }

        protected override Expression VisitShapedQueryExpression(ShapedQueryExpression shapedQueryExpression)
        {
            this.m_ShapedQueryExpression = shapedQueryExpression;

            return base.VisitShapedQueryExpression(shapedQueryExpression);
        }

        #endregion Methods

    }


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

    //public class ShapedQueryCompilingExpressionVisitorDecorator : ShapedQueryCompilingExpressionVisitor
    //{

    //    #region - - - - - - Fields - - - - - -

    //    private readonly ShapedQueryCompilingExpressionVisitor m_ShapedQueryCompilingExpressionVisitor;

    //    #endregion Fields

    //    #region - - - - - - Constructors - - - - - -

    //    public ShapedQueryCompilingExpressionVisitorDecorator(
    //        ShapedQueryCompilingExpressionVisitorDependencies dependencies,
    //        QueryCompilationContext queryCompilationContext,
    //        ShapedQueryCompilingExpressionVisitor shapedQueryCompilingExpressionVisitor) : base(dependencies, queryCompilationContext)
    //        => this.m_ShapedQueryCompilingExpressionVisitor = shapedQueryCompilingExpressionVisitor;

    //    #endregion Constructors

    //    #region - - - - - - Methods - - - - - -

    //    protected override Expression VisitShapedQueryExpression(ShapedQueryExpression shapedQueryExpression)
    //    {
    //        if (this.m_ShapedQueryCompilingExpressionVisitor is RelationalShapedQueryCompilingExpressionVisitor)
    //        {
    //            var _X = VisitX(this.m_ShapedQueryCompilingExpressionVisitor, shapedQueryExpression);// this.m_ShapedQueryCompilingExpressionVisitor.VisitShapedQueryExpression(shapedQueryExpression);
    //            return _X;
    //        }

    //        xxx // Rather than creating a decorator,
    //            // just inherit from RelationalShapedQueryCompilingExpressionVisitor 
    //            // and override the InjectEntityMaterializers method.
    //            // This will only work for the relational shaped visitor, so figure out how that plays into the solution
    //            // It should work for this scenario, future releases can solve the problem betterer.


    //        // TODO: FIX
    //        throw new NotImplementedException();
    //        //return this.m_ShapedQueryCompilingExpressionVisitor.VisitShapedQueryExpression(shapedQueryExpression);
    //    }

    //    private static Expression VisitX(ShapedQueryCompilingExpressionVisitor visitor, ShapedQueryExpression expression)
    //        => (Expression)visitor.GetType()
    //            .GetMethod("VisitShapedQueryExpression", BindingFlags.NonPublic | BindingFlags.Instance)
    //            .Invoke(visitor, new object[] { expression });//return x.VisitShapedQueryExpression(this);

    //    #endregion Methods

    //}




    //public class ValueConverterSelector : IValueConverterSelector
    //{

    //    //private readonly DbContext m_DbContext;
    //    //private readonly IValueConverterSelector m_ValueConverterSelector;

    //    public ValueConverterSelector()
    //    {

    //    }

    //    //public ValueConverterSelector(DbContext dbContext, IValueConverterSelector valueConverterSelector)
    //    //{
    //    //    this.m_DbContext = dbContext;
    //    //    this.m_ValueConverterSelector = valueConverterSelector;
    //    //}

    //    //IEnumerable<ValueConverterInfo> IValueConverterSelector.Select(Type modelClrType, Type providerClrType)
    //    //    => modelClrType == typeof(SemiStaticEntity) && providerClrType == typeof(string)
    //    //        ? (new[] { this.GetStaticEntityConverter() })
    //    //        : this.m_ValueConverterSelector.Select(modelClrType, providerClrType);

    //    //private ValueConverterInfo GetStaticEntityConverter()
    //    //    => new ValueConverterInfo(
    //    //        typeof(SemiStaticEntity),
    //    //        typeof(string),
    //    //        vci => new ValueConverter<SemiStaticEntity, string>(
    //    //            sse => sse.ID,
    //    //            id => this.m_DbContext.Find<SemiStaticEntity>(id)));

    //    IEnumerable<ValueConverterInfo> IValueConverterSelector.Select(Type modelClrType, Type providerClrType)
    //        => Enumerable.Empty<ValueConverterInfo>();
    //}

    //public interface IValueConverterFactory
    //{

    //    public Func<ValueConverterInfo, ValueConverter> GetValueConverterFunc();

    //}

    //public class ValueConverterFactory : IValueConverterFactory
    //{

    //    public ValueConverterFactory()
    //    {

    //    }

    //    Func<ValueConverterInfo, ValueConverter> IValueConverterFactory.GetValueConverterFunc()
    //        => throw new NotImplementedException();

    //}

}
