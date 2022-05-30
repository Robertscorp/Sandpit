using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandpit.Console.Entities;
using Sandpit.SemiStaticEntity.Extensions;
using System.Linq;

namespace Sandpit.Console.Configurations
{

    public class BarConfiguration : IEntityTypeConfiguration<Bar>
    {

        #region - - - - - - Methods - - - - - -

        void IEntityTypeConfiguration<Bar>.Configure(EntityTypeBuilder<Bar> builder)
        {
            _ = builder.HasData(new Bar { ID = 1, Test = "Test1" });
            _ = builder.HasDynamicData(dbContext => dbContext.Set<Foo>().Select(f => new Bar()
            {
                ID = 2, //f.GetHashCode(),
                Test = "Dynamic Data - " + f.ID.ToString()
            }));

            //_ = builder.ToTable("Bar");
            _ = builder.IsStaticEntity();
            _ = builder.HasKey(e => e.ID);
        }

        #endregion Methods

    }

}
