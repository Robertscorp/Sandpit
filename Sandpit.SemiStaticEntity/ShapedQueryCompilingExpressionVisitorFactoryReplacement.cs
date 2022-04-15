using Microsoft.EntityFrameworkCore.Query;

namespace Sandpit.SemiStaticEntity
{

    public class ShapedQueryCompilingExpressionVisitorFactoryReplacement : IShapedQueryCompilingExpressionVisitorFactory
    {

        #region - - - - - - Fields - - - - - -

        private readonly ShapedQueryCompilingExpressionVisitorDependencies m_Dependencies;
        private readonly RelationalShapedQueryCompilingExpressionVisitorDependencies m_RelationalDependencies;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ShapedQueryCompilingExpressionVisitorFactoryReplacement(
            ShapedQueryCompilingExpressionVisitorDependencies dependencies,
            RelationalShapedQueryCompilingExpressionVisitorDependencies relationalDependencies)
        {
            this.m_Dependencies = dependencies;
            this.m_RelationalDependencies = relationalDependencies;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        ShapedQueryCompilingExpressionVisitor IShapedQueryCompilingExpressionVisitorFactory.Create(
            QueryCompilationContext queryCompilationContext)
            => new ShapedQueryCompilingExpressionVisitorReplacement(
                this.m_Dependencies,
                this.m_RelationalDependencies,
                queryCompilationContext);

        #endregion Methods

    }

}
