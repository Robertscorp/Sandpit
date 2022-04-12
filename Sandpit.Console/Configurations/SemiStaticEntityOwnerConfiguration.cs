using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandpit.Console.Entities;
using Sandpit.Console.Persistence;

namespace Sandpit.Console.Configurations
{
    public class SemiStaticEntityOwnerConfiguration : IEntityTypeConfiguration<SemiStaticEntityOwner>
    {

        #region - - - - - - Methods - - - - - -

        void IEntityTypeConfiguration<SemiStaticEntityOwner>.Configure(EntityTypeBuilder<SemiStaticEntityOwner> builder)
        {
            //_ = builder.HasData(new SemiStaticEntityOwner(new SemiStaticEntity { ID = "DE1" }) { ID = 1, Name = "Owner1" });

            _ = builder.ToTable("SemiStaticEntityOwner");
            _ = builder.HasKey(e => e.ID);
            _ = builder.Property(e => e.Name).HasConversion(x => X(x), x => X(x)).UsePropertyAccessMode(PropertyAccessMode.PreferProperty);

            _ = builder.Property(e => e.SemiStaticEntity)
                    .HasConversion(x => "", x => default!)
                    .HasAnnotation("TypeMapping2",
                        new XXConverter<SemiStaticEntity, string>(
                            (DbContext c, string s) =>
                            {
                                return new SemiStaticEntity();
                            },
                            (DbContext c, SemiStaticEntity sse) => sse.ID));
            //_ = builder.Property(e => e.SemiStaticEntity).HasConversion(sse => sse.ID, dbVal => default!);
            //_ = builder.Property<string>("SemiStaticEntityID");
        }

        #endregion Methods

        public static string X(string x)
            => x.ToUpper();

    }

}
