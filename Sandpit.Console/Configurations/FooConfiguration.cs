using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandpit.Console.Entities;

namespace Sandpit.Console.Configurations
{
    public class FooConfiguration : IEntityTypeConfiguration<Foo>
    {

        #region - - - - - - Methods - - - - - -

        void IEntityTypeConfiguration<Foo>.Configure(EntityTypeBuilder<Foo> builder)
        {
            _ = builder.ToTable("Foo");
            _ = builder.HasKey(e => e.ID);

            _ = builder.HasOne(e => e.Bar).WithMany().IsRequired();
        }

        #endregion Methods

    }

}
