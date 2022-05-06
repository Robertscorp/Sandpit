using Microsoft.EntityFrameworkCore.Metadata;

namespace Sandpit.SemiStaticEntity.Extensions
{

    public static class IEntityTypeExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static bool IsStaticEntity(this IEntityType entityType)
            => entityType?.FindAnnotation("StaticEntity.IsStaticEntity") != null; // TODO: Hard-coded string

        #endregion Methods

    }

}
