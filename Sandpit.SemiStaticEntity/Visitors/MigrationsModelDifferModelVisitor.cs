using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sandpit.SemiStaticEntity.Builders;
using Sandpit.SemiStaticEntity.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Sandpit.SemiStaticEntity.Visitors
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class MigrationsModelDifferModelVisitor : ModelVisitor
    {

        #region - - - - - - Methods - - - - - -

        protected override EntityType VisitEntityType(EntityTypeBuilder entityTypeBuilder)
        {
            if (entityTypeBuilder.EntityType.IsStaticEntity())
                entityTypeBuilder.ModelBuilder.IgnoreEntityType(entityTypeBuilder.EntityType);

            foreach (var (_Navigation, _EntityType) in entityTypeBuilder.GetNavigations()
                .Select(n => (Navigation: n, EntityType: entityTypeBuilder.ModelBuilder.GetEntityType(n.ClrType)))
                .Where(net => net.EntityType.IsStaticEntity()))
            {
                entityTypeBuilder.RemoveNavigation(_Navigation);
                entityTypeBuilder.RemoveForeignKey(_Navigation.ForeignKey);

                foreach (var _Property in _Navigation.ForeignKey.Properties)
                    foreach (var _Index in _Property.Indexes ?? Enumerable.Empty<Index>())
                        entityTypeBuilder.RemoveIndex(_Index);
            }

            return base.VisitEntityType(entityTypeBuilder);
        }

        #endregion Methods

    }

}
