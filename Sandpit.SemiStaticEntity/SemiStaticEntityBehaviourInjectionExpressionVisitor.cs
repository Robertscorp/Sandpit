using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sandpit.SemiStaticEntity
{

    public class SemiStaticEntityBehaviourInjectionExpressionVisitor : ExpressionVisitor
    {

        #region - - - - - - Fields - - - - - -

        private readonly Dictionary<IProperty, IProperty> m_DecoratorLookup = new Dictionary<IProperty, IProperty>();
        private readonly List<ProjectionBindingExpression> m_ProjectionBindings = new List<ProjectionBindingExpression>();
        private readonly SelectExpression m_SelectExpression;

        private Expression m_QueryContextParameter;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public SemiStaticEntityBehaviourInjectionExpressionVisitor(SelectExpression selectExpression)
            => this.m_SelectExpression = selectExpression ?? throw new ArgumentNullException(nameof(selectExpression));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private IProperty GetPropertyDecorator(IProperty property)
        {
            if (!this.m_DecoratorLookup.TryGetValue(property, out var _Decorator))
            {
                _Decorator = new PropertyDecorator(property, this.m_QueryContextParameter);
                this.m_DecoratorLookup.Add(property, _Decorator);
            }

            return _Decorator;
        }

        protected override Expression VisitConstant(ConstantExpression node)
            => typeof(IPropertyBase).IsAssignableFrom(node?.Type)
                ? Expression.Constant(this.GetPropertyDecorator((IProperty)node.Value)) // The nodes are typed as IPropertyBase, but they are cast as IProperty in the shaper.
                : base.VisitConstant(node);

        protected override Expression VisitExtension(Expression node)
        {
            if (node is ProjectionBindingExpression _Node)
                this.m_ProjectionBindings.Add(_Node);

            return base.VisitExtension(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            this.m_QueryContextParameter = node.Parameters.Single(p => p.Type == typeof(QueryContext));

            var _Lambda = base.VisitLambda(node);

            this.m_SelectExpression.ReplaceProjectionMapping(
                this.m_ProjectionBindings
                    .Select(b => (b, (IDictionary<IProperty, int>)((ConstantExpression)this.m_SelectExpression.GetMappedProjection(b.ProjectionMember)).Value))
                    .ToDictionary(
                        bp => bp.b.ProjectionMember,
                        bp => (Expression)Expression.Constant(
                            bp.Item2.ToDictionary(pi => this.m_DecoratorLookup[pi.Key], pi => pi.Value))));

            return _Lambda;
        }

        #endregion Methods

    }

}
