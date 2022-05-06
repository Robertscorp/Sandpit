//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Sandpit.Console.Entities;
//using System;

//namespace Sandpit.Console.Configurations
//{
//    public class SemiStaticEntityOwner2Configuration : IEntityTypeConfiguration<SemiStaticEntityOwner2>
//    {

//        #region - - - - - - Methods - - - - - -

//        void IEntityTypeConfiguration<SemiStaticEntityOwner2>.Configure(EntityTypeBuilder<SemiStaticEntityOwner2> builder)
//        {
//            //_ = builder.HasData(new SemiStaticEntityOwner(new SemiStaticEntity { ID = "DE1" }) { ID = 1, Name = "Owner1" });

//            _ = builder.ToTable("SemiStaticEntityOwner2");
//            _ = builder.HasKey(e => e.ID);

//            _ = builder.Property<string>("Key1");
//            _ = builder.Property<Guid?>("Key2");
//            _ = builder.Property(e => e.Name); //.HasConversion(x => X(x), x => X(x)).UsePropertyAccessMode(PropertyAccessMode.PreferProperty);

//            _ = builder.HasOne(e => e.SemiStaticEntity).WithMany();
//            _ = builder.HasOne(e => e.SemiStaticEntity2).WithMany().HasForeignKey("Key1", "Key2");



//            //_ = builder.Property(e => e.JSemiStaticEntity)
//            //        .HasConversion(x => "", x => default!);
//            //        .HasAnnotation("StaticEntity.TypeMapping",
//            //            new StaticEntityTypeMapping<Entities.SemiStaticEntity, string>(
//            //                (DbContext c, string s) =>
//            //                {
//            //                    return new Entities.SemiStaticEntity();
//            //                },
//            //                (DbContext c, Entities.SemiStaticEntity sse) => sse.ID));
//            //_ = builder.Property(e => e.SemiStaticEntity).HasConversion(sse => sse.ID, dbVal => default!);
//            //_ = builder.Property<string>("SemiStaticEntityID");
//        }

//        #endregion Methods

//        public static string X(string x)
//            => x.ToUpper();

//    }

//}
