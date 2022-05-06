using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sandpit.SemiStaticEntity.Internal
{

    public class StaticEntitySet<TStaticEntity>
        : DbSet<TStaticEntity>, IEnumerable<TStaticEntity>, IEnumerable
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

        //
        // Summary:
        //     Returns an System.Collections.Generic.IEnumerator`1 which when enumerated will
        //     execute a query against the database to load all entities from the database.
        //
        // Returns:
        //     The query results.
        IEnumerator<TStaticEntity> IEnumerable<TStaticEntity>.GetEnumerator()
            => ((IEnumerable<TStaticEntity>)this.m_StaticEntities).GetEnumerator();

        //
        // Summary:
        //     Returns an System.Collections.IEnumerator which when enumerated will execute
        //     a query against the database to load all entities from the database.
        //
        // Returns:
        //     The query results.
        IEnumerator IEnumerable.GetEnumerator()
            => throw new NotImplementedException();

        ////
        //// Summary:
        ////     Returns an System.Collections.Generic.IAsyncEnumerator`1 which when enumerated
        ////     will asynchronously execute a query against the database.
        ////
        //// Returns:
        ////     The query results.
        //IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        ////
        //// Summary:
        ////     This method is called by data binding frameworks when attempting to data bind
        ////     directly to a Microsoft.EntityFrameworkCore.DbSet`1.
        ////     This implementation always throws an exception as binding directly to a Microsoft.EntityFrameworkCore.DbSet`1
        ////     will result in a query being sent to the database every time the data binding
        ////     framework requests the contents of the collection. Instead load the results into
        ////     the context, for example, by using the Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load``1(System.Linq.IQueryable{``0})
        ////     extension method, and then bind to the local data through the Microsoft.EntityFrameworkCore.DbSet`1.Local
        ////     by calling Microsoft.EntityFrameworkCore.ChangeTracking.LocalView`1.ToObservableCollection
        ////     for WPF binding, or Microsoft.EntityFrameworkCore.ChangeTracking.LocalView`1.ToBindingList
        ////     for WinForms.
        ////
        //// Returns:
        ////     Never returns, always throws an exception.
        ////
        //// Exceptions:
        ////   T:System.NotSupportedException:
        ////     Always thrown.
        //IList IListSource.GetList()
        //{
        //    throw new NotSupportedException(CoreStrings.DataBindingWithIListSource);
        //}



    }

}
