using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Sandpit.SemiStaticEntity.Internal
{

    public class StaticEntitySet<TStaticEntity>
        : DbSet<TStaticEntity>, IEnumerable, IEnumerable<TStaticEntity>, IListSource, IQueryable, IQueryable<TStaticEntity>
        where TStaticEntity : class
    {

        #region - - - - - - Fields - - - - - -

        private readonly TStaticEntity[] m_StaticEntities;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public StaticEntitySet(List<object> staticEntities)
            => this.m_StaticEntities = staticEntities?.OfType<TStaticEntity>().ToArray()
                                        ?? throw new ArgumentNullException(nameof(staticEntities));

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        bool IListSource.ContainsListCollection => true;

        Type IQueryable.ElementType => typeof(TStaticEntity);

        Expression IQueryable.Expression => this.m_StaticEntities.AsQueryable().Expression;

        IQueryProvider IQueryable.Provider => this.m_StaticEntities.AsQueryable().Provider;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public override IQueryable<TStaticEntity> AsQueryable()
            => this.m_StaticEntities.AsQueryable();

        //IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        IEnumerator IEnumerable.GetEnumerator()
            => this.m_StaticEntities.GetEnumerator();

        IEnumerator<TStaticEntity> IEnumerable<TStaticEntity>.GetEnumerator()
            => ((IEnumerable<TStaticEntity>)this.m_StaticEntities).GetEnumerator();

        IList IListSource.GetList()
            => this.m_StaticEntities;

        #endregion Methods

    }

}
