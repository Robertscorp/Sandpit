using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandpit.Console.Entities;

namespace Sandpit.Console.Configurations
{

    public class EncapsulateParent2Configuration : IEntityTypeConfiguration<EncapsulateParent2>
    {

        #region - - - - - - Methods - - - - - -

        void IEntityTypeConfiguration<EncapsulateParent2>.Configure(EntityTypeBuilder<EncapsulateParent2> builder)
        {
            //var _Parent = new Parent();
            //_Parent.Children.Add(new Child("Child3", _Parent) { ID = 3, Date = new DateTime(2000, 2, 2) });
            //_Parent.Children.Add(new Child("Child4", _Parent) { ID = 4, Date = new DateTime(3000, 2, 2) });

            //_ = builder.HasData(new EncapsulateParent2(_Parent) { ID = 2 });

            _ = builder.ToTable("EncapsulateParent2");

            var _ParentBuilder = builder.OwnsOne(e => e.EncapsulatedParent);

            var _ChildBuilder = _ParentBuilder.OwnsMany(e => e.Children);
            _ = _ChildBuilder.ToTable("Child2");
            _ = _ChildBuilder.HasKey(e => e.ID);
            _ = _ChildBuilder.Property(e => e.Date);
            _ = _ChildBuilder.Property(e => e.Name);

            _ = builder.HasKey(e => e.ID);
        }

        #endregion Methods

    }

}
