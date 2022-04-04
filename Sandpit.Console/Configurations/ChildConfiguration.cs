//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Sandpit.Console.Entities;

//namespace Sandpit.Console.Configurations
//{
//    public class ChildConfiguration : IEntityTypeConfiguration<Child>
//    {

//        #region - - - - - - Fields - - - - - -

//        //public static Child Child1 = new()

//        #endregion Fields

//        #region - - - - - - Methods - - - - - -

//        void IEntityTypeConfiguration<Child>.Configure(EntityTypeBuilder<Child> builder)
//        {
//            //_ = builder.HasData(new[]
//            //{
//            //    new Child(GenderSet.Male, "Hugh Mann") { ID= 1 },
//            //    new Child(GenderSet.Female, "Amanda Huggen-Kiss") { ID= 2 },
//            //    new Child(GenderSet.Mayonnaise, "GennY McStable") { ID= 3 }
//            //});

//            var _X = builder.Metadata.Model.AddEntityType("Test");

//            _ = builder.ToTable("asd");

//            _ = builder.HasKey(e => e.ID);
//            _ = builder.Property(e => e.Name);
//            _ = builder.HasOne(e => e.Parent).WithMany(e => e.Children);
//        }

//        #endregion Methods

//    }

//}
