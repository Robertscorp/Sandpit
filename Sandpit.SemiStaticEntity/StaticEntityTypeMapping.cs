using Microsoft.EntityFrameworkCore;
using System;

namespace Sandpit.SemiStaticEntity
{

    public class StaticEntityTypeMapping<TStaticEntity, TProvider>
    {

        #region - - - - - - Constructors - - - - - -

        public StaticEntityTypeMapping(
            Func<DbContext, TProvider, TStaticEntity> toStaticEntity,
            Func<DbContext, TStaticEntity, TProvider> toProvider)
        {
            this.ToStaticEntity = toStaticEntity;
            this.ToProvider = toProvider;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Func<DbContext, TProvider, TStaticEntity> ToStaticEntity { get; }

        public Func<DbContext, TStaticEntity, TProvider> ToProvider { get; }

        #endregion Properties

    }

}
