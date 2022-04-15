using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Sandpit.SemiStaticEntity
{

    public class ShapedQueryCompilingExpressionVisitorReplacement : RelationalShapedQueryCompilingExpressionVisitor
    {

        #region - - - - - - Fields - - - - - -

        private ShapedQueryExpression m_ShapedQueryExpression;
        private readonly IReadOnlyList<ReaderColumn> m_ProjectionColumns;
        private readonly Dictionary<IPropertyBase, IProperty> m_PropertyDecoratorMappings = new Dictionary<IPropertyBase, IProperty>();

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

        protected override Expression InjectEntityMaterializers(Expression expression)
        {
            var _SelectExpression = (SelectExpression)this.m_ShapedQueryExpression.QueryExpression;
            var _LambdaExpression = (LambdaExpression)expression;
            var _DataReaderParameter = _LambdaExpression.Parameters.Single(p => p.Type == typeof(DbDataReader));
            var _IndexMapParameter = _LambdaExpression.Parameters.Single(p => p.Type == typeof(int[]));
            var _QueryContextParameter = _LambdaExpression.Parameters.Single(p => p.Type == typeof(QueryContext));

            var _Expression = base.InjectEntityMaterializers(expression);

            // 100% this is the wrong place to be injecting this behaviour... but there's not really an alternative.

            _Expression = new SemiStaticEntityBehaviourInjectionExpressionVisitor(_SelectExpression).Visit(_Expression);

            //_Expression = new RelationalProjectionBindingRemovingExpressionVisitor(
            //        _SelectExpression,
            //        _DataReaderParameter,
            //        _SelectExpression.IsNonComposedFromSql() ? _IndexMapParameter : null,
            //        _QueryContextParameter,
            //        this.IsBuffering)
            //    .Visit(_Expression, out this.m_ProjectionColumns);


            return _Expression;
        }

        protected override Expression VisitShapedQueryExpression(ShapedQueryExpression shapedQueryExpression)
        {
            this.m_ShapedQueryExpression = shapedQueryExpression;

            return base.VisitShapedQueryExpression(shapedQueryExpression);
        }

        #endregion Methods

    }

}
