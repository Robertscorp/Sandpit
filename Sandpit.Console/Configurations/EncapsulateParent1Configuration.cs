using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandpit.Console.Entities;

namespace Sandpit.Console.Configurations
{

    public class EncapsulateParent1Configuration : IEntityTypeConfiguration<EncapsulateParent1>
    {

        #region - - - - - - Methods - - - - - -

        void IEntityTypeConfiguration<EncapsulateParent1>.Configure(EntityTypeBuilder<EncapsulateParent1> builder)
        {
            //var _Parent = new Parent();
            //_Parent.Children.Add(new Child("Child1", _Parent) { ID = 1, Date = new DateTime(2000, 1, 1) });
            //_Parent.Children.Add(new Child("Child2", _Parent) { ID = 2, Date = new DateTime(3000, 1, 1) });

            //_ = builder.HasData(new EncapsulateParent1(_Parent) { ID = 1 });

            _ = builder.ToTable("EncapsulateParent1");

            var _ParentBuilder = builder.OwnsOne(e => e.EncapsulatedParent);
            //_ = _ParentBuilder.HasData(_Parent);
            _ = _ParentBuilder.ToTable("Parent1");

            var _ChildBuilder = _ParentBuilder.OwnsMany(e => e.Children);
            //_ = _ChildBuilder.HasData(_Parent.Children.ToArray());
            _ = _ChildBuilder.ToTable("Child1");
            _ = _ChildBuilder.HasKey(e => e.ID);
            _ = _ChildBuilder.Property(e => e.Date);
            _ = _ChildBuilder.Property(e => e.Name);

            _ = builder.HasKey(e => e.ID);
        }

        #endregion Methods

    }

}
