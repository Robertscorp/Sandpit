using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace Sandpit.SemiStaticEntity
{

    public class ShapedQueryCompilingExpressionVisitorReplacement : RelationalShapedQueryCompilingExpressionVisitor
    {

        #region - - - - - - Fields - - - - - -

        // TODO: Implement Projection Columns if required.
        //private readonly IReadOnlyList<ReaderColumn> m_ProjectionColumns; 

        private SelectExpression m_SelectExpression;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ShapedQueryCompilingExpressionVisitorReplacement(
            ShapedQueryCompilingExpressionVisitorDependencies dependencies,
            RelationalShapedQueryCompilingExpressionVisitorDependencies relationalDependencies,
            QueryCompilationContext queryCompilationContext)
            : base(dependencies, relationalDependencies, queryCompilationContext)
        {
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        // 100% this is the wrong place to be injecting this behaviour... but there's not really an alternative.
        protected override Expression InjectEntityMaterializers(Expression expression)
            => new SemiStaticEntityBehaviourInjectionExpressionVisitor(this.m_SelectExpression)
                .Visit(base.InjectEntityMaterializers(expression));

        protected override Expression VisitShapedQueryExpression(ShapedQueryExpression shapedQueryExpression)
        {
            this.m_SelectExpression = (SelectExpression)shapedQueryExpression.QueryExpression;

            return base.VisitShapedQueryExpression(shapedQueryExpression);
        }

        #endregion Methods

    }

}
