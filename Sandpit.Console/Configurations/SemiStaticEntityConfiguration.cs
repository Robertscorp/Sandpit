//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Sandpit.Console.Configurations
//{

//    public class SemiStaticEntityConfiguration : IEntityTypeConfiguration<Entities.SemiStaticEntity>
//    {

//        #region - - - - - - Methods - - - - - -

//        void IEntityTypeConfiguration<Entities.SemiStaticEntity>.Configure(EntityTypeBuilder<Entities.SemiStaticEntity> builder)
//        {
//            _ = builder.HasData(
//                    new Entities.SemiStaticEntity { ID = "SE1", Name = "Static Entity 1" },
//                    new Entities.SemiStaticEntity { ID = "SE2", Name = "Static Entity 2" });

//            //_ = builder.ToTable("ASF");

//            //_ = builder.IsStaticEntity();
//            _ = builder.HasKey(e => e.ID);
//            _ = builder.Property(e => e.Name);
//        }

//        #endregion Methods

//    }

//}
