using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sandpit.SemiStaticEntity.Internal
{

    public class StaticEntityEnumerable<TStaticEntity> : IEnumerable<TStaticEntity> where TStaticEntity : class
    {

        #region - - - - - - Fields - - - - - -

        private readonly Func<IEnumerator<TStaticEntity>> m_StaticEntityEnumeratorFactory;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public StaticEntityEnumerable(DbContext dbContext)
        {
            if (dbContext is null) throw new ArgumentNullException(nameof(dbContext));

            this.m_StaticEntityEnumeratorFactory = () => new StaticEntityEnumerator<TStaticEntity>(dbContext);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        IEnumerator IEnumerable.GetEnumerator()
            => this.m_StaticEntityEnumeratorFactory();

        IEnumerator<TStaticEntity> IEnumerable<TStaticEntity>.GetEnumerator()
            => this.m_StaticEntityEnumeratorFactory();

        #endregion Methods

    }

}
