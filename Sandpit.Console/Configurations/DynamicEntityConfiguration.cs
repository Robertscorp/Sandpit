using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandpit.Console.Entities;

namespace Sandpit.Console.Configurations
{
    public class DynamicEntityConfiguration : IEntityTypeConfiguration<DynamicEntity>
    {

        #region - - - - - - Methods - - - - - -

        void IEntityTypeConfiguration<DynamicEntity>.Configure(EntityTypeBuilder<DynamicEntity> builder)
        {
            _ = builder.HasData(new DynamicEntity { ID = 1, Name = "DE1" });
            _ = builder.HasData(new DynamicEntity { ID = 2, Name = "DE2" });
            _ = builder.HasData(new DynamicEntity { ID = 3, Name = "DE3" });

            _ = builder.ToTable("DynamicEntity");
            _ = builder.HasKey(e => e.ID);
            _ = builder.Property(e => e.Name);
        }

        #endregion Methods

    }
}
