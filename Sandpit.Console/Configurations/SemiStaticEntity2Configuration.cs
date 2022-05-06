//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Sandpit.Console.Entities;
//using Sandpit.SemiStaticEntity;

//namespace Sandpit.Console.Configurations
//{

//    public class SemiStaticEntity2Configuration : IEntityTypeConfiguration<Entities.SemiStaticEntity2>
//    {

//        #region - - - - - - Methods - - - - - -

//        void IEntityTypeConfiguration<SemiStaticEntity2>.Configure(EntityTypeBuilder<SemiStaticEntity2> builder)
//        {
//            _ = builder.HasData(
//                    new SemiStaticEntity2 { ID = "SE1", Name = "Static Entity 1" },
//                    new SemiStaticEntity2 { ID = "SE2", Name = "Static Entity 2" });

//            //_ = builder.ToTable("ASF");

//            _ = builder.IsStaticEntity();
//            _ = builder.HasKey(nameof(SemiStaticEntity2.ID), nameof(SemiStaticEntity2.ID2));
//            _ = builder.Property(e => e.Name);
//        }

//        #endregion Methods

//    }

//}
