using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Sandpit.ConcatenateQueryables
{

    public class ConcatenatedQueryable<T> : IOrderedQueryable<T>, IQueryable<T>, IQueryProvider
    {

        #region - - - - - - Fields - - - - - -

        private static readonly MethodInfo s_ConcatMethodInfo = typeof(Queryable).GetMethod(nameof(Queryable.Concat))!;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ConcatenatedQueryable(IQueryable<T> first, IQueryable<T> second)
            : this(Expression.Constant(first), Expression.Constant(second)) { }

        private ConcatenatedQueryable(Expression first, Expression second)
            => this.Expression = Expression.Call(
                null,
                s_ConcatMethodInfo.MakeGenericMethod(this.ElementType),
                first,
                second);

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Type ElementType => typeof(T);

        public Expression Expression { get; }

        public IQueryProvider Provider => this;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public IQueryable CreateQuery(Expression expression)
            => throw new NotImplementedException();

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            => new ConcatenatedQueryable<TElement>(
                new ReplacementVisitor(this.Expression, this.GetFirst()).Visit(expression),
                new ReplacementVisitor(this.Expression, this.GetSecond()).Visit(expression));

        public object? Execute(Expression expression)
            => throw new NotImplementedException();

        public TResult Execute<TResult>(Expression expression)
            => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
            => Expression.Lambda<Func<IQueryable<T>>>(this.Expression).Compile().Invoke().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ((IQueryable<T>)this).GetEnumerator();

        private Expression GetFirst()
            => ((MethodCallExpression)this.Expression).Arguments[0];

        private Expression GetSecond()
            => ((MethodCallExpression)this.Expression).Arguments[1];

        #endregion Methods

        #region - - - - - - Nested Classes - - - - - -

        private class ReplacementVisitor : ExpressionVisitor
        {

            #region - - - - - - Fields - - - - - -

            private readonly Expression m_Replacement;
            private readonly Expression m_Source;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public ReplacementVisitor(Expression source, Expression replacement)
            {
                this.m_Source = source;
                this.m_Replacement = replacement;
            }

            #endregion Constructors

            #region - - - - - - Methods - - - - - -

            [return: NotNullIfNotNull("node")]
            public override Expression? Visit(Expression? node)
                => Equals(this.m_Source, node)
                    ? this.m_Replacement
                    : base.Visit(node);

            #endregion Methods

        }

        #endregion Nested Classes

    }

}
