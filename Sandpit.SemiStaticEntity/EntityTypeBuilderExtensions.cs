using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sandpit.SemiStaticEntity
{

    public static class EntityTypeBuilderExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static EntityTypeBuilder<TEntity> IsStaticEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        {
            _ = builder.Metadata.AddAnnotation("StaticEntity.IsStaticEntity", true);

            return builder;
        }

        #endregion Methods

    }

}
