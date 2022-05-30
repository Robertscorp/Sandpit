using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sandpit.SemiStaticEntity.Internal
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class StaticEntitySet<TStaticEntity> :
        DbSet<TStaticEntity>, IEnumerable, IEnumerable<TStaticEntity>, IListSource, IQueryable, IQueryable<TStaticEntity>,
        IInfrastructure<IServiceProvider>
        where TStaticEntity : class
    {

        #region - - - - - - Fields - - - - - -

        private readonly Func<object[], TStaticEntity> m_EntityFinderFunc;
        private readonly LocalView<TStaticEntity> m_LocalView;
        private readonly IServiceProvider m_ServiceProvider;
        private readonly IEnumerable<TStaticEntity> m_StaticEntityEnumerable;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public StaticEntitySet(DbContext dbContext)
        {
            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));

            this.m_EntityFinderFunc = keyValues => dbContext.Find<TStaticEntity>(keyValues);
            this.m_ServiceProvider = ((IInfrastructure<IServiceProvider>)dbContext).Instance;
            this.m_StaticEntityEnumerable = new StaticEntityEnumerable<TStaticEntity>(dbContext);

            this.m_LocalView = new LocalView<TStaticEntity>(this);
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        bool IListSource.ContainsListCollection => true;

        public override LocalView<TStaticEntity> Local => this.m_LocalView;

        Type IQueryable.ElementType => typeof(TStaticEntity);

        Expression IQueryable.Expression => this.m_StaticEntityEnumerable.AsQueryable().Expression;

        IQueryProvider IQueryable.Provider => this.m_StaticEntityEnumerable.AsQueryable().Provider;

        IServiceProvider IInfrastructure<IServiceProvider>.Instance => this.m_ServiceProvider;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public override IQueryable<TStaticEntity> AsQueryable()
            => this.m_StaticEntityEnumerable.AsQueryable();

        public override TStaticEntity Find(params object[] keyValues)
            => this.m_EntityFinderFunc(keyValues);

        public override ValueTask<TStaticEntity> FindAsync(params object[] keyValues)
            => new ValueTask<TStaticEntity>(Task.FromResult(this.Find(keyValues)));

        public override ValueTask<TStaticEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
            => this.FindAsync(keyValues);

        //IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        IEnumerator IEnumerable.GetEnumerator()
            => this.m_StaticEntityEnumerable.GetEnumerator();

        IEnumerator<TStaticEntity> IEnumerable<TStaticEntity>.GetEnumerator()
            => this.m_StaticEntityEnumerable.GetEnumerator();

        IList IListSource.GetList()
            => this.m_StaticEntityEnumerable.ToList();

        #endregion Methods

    }

}
