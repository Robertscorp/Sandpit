using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Sandpit.SemiStaticEntity.Extensions
{

    public static class EntityTypeBuilderExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static EntityTypeBuilder<TEntity> HasDynamicData<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Func<DbContext, IEnumerable<TEntity>> getDynamicDataFunc) where TEntity : class
        {
            _ = builder.Metadata.AddAnnotation("StaticEntity.HasDynamicData", getDynamicDataFunc);

            return builder;
        }

        public static EntityTypeBuilder<TEntity> IsStaticEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        {
            _ = builder.Metadata.AddAnnotation("StaticEntity.IsStaticEntity", true);

            return builder;
        }

        #endregion Methods

    }

}
