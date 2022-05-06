using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sandpit.SemiStaticEntity.Builders;
using Sandpit.SemiStaticEntity.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Sandpit.SemiStaticEntity.Visitors
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class StaticEntityModelVisitor : ModelVisitor
    {

        #region - - - - - - Methods - - - - - -

        protected override Model VisitModel(ModelBuilder modelBuilder)
        {
            //var _X = new EntityTypeBuilder(modelBuilder.GetEntityTypes().Single(e => e.ClrType.Name == "SemiStaticEntityOwner"), modelBuilder);
            //var _X2 = new EntityTypeBuilder(modelBuilder.GetEntityTypes().Single(e => e.ClrType.Name == "SemiStaticEntityOwner2"), modelBuilder);

            //var _RTF = _X.GetProperties().Single(f => f.Name.Contains("Static"));
            //var _RTF2 = _X2.GetProperties().Single(f => f.Name.Contains("Static"));

            //var _N = _X2.GetNavigations().SingleOrDefault(n => n.Name.Contains("Static"));
            //if (_N != null)
            //{
            //    _X2.RemoveNavigation(_N);
            //    _ = _X2.Property(_N.PropertyInfo.PropertyType, _N.PropertyInfo.Name);
            //}
            _ = 0;
            return base.VisitModel(modelBuilder);
        }

        protected override EntityType VisitEntityType(EntityTypeBuilder entityTypeBuilder)
        {
            if (entityTypeBuilder.EntityType.IsStaticEntity())
                entityTypeBuilder.ModelBuilder.IgnoreEntityType(entityTypeBuilder.EntityType);

            foreach (var (_Navigation, _EntityType) in entityTypeBuilder.GetNavigations()
                .Select(n => (Navigation: n, EntityType: entityTypeBuilder.ModelBuilder.GetEntityType(n.ClrType)))
                .Where(net => net.EntityType.IsStaticEntity()))
            {
                entityTypeBuilder.RemoveNavigation(_Navigation);
                entityTypeBuilder.RemoveForeignKey(_Navigation.ForeignKey);

                //_ = entityTypeBuilder
                //        .Property(_Navigation.PropertyInfo.PropertyType, _Navigation.PropertyInfo.Name)
                //        .HasConversion(new EmptyConverter(_Navigation, _EntityType));
                //.HasAnnotation();

                foreach (var _Property in _Navigation.ForeignKey.Properties)
                {
                    //entityTypeBuilder.RemoveProperty(_Property);

                    foreach (var _Index in _Property.Indexes ?? Enumerable.Empty<Index>())
                        entityTypeBuilder.RemoveIndex(_Index);
                }

                //foreach (var _Key in _EntityType.GetKeys())
                //    foreach (var _Property in _Key.Properties)
                //    {
                //        _ = 0;
                //    }
                //var _X = _EntityType.GetKeys();
                //var _Y = _X.ToList();

                //_ = entityTypeBuilder.Property(_Navigation.Name);

                // TODO: Properties contain a PropertyIndex, which keeps track of their relative index.
                // This will need to be updated when a property is added / removed.
                // The entityType also tracks counts of properties and things.
            }

            return base.VisitEntityType(entityTypeBuilder);
        }

        protected override Property VisitProperty(PropertyBuilder propertyBuilder)
        {
            if (propertyBuilder.PropertyEntityType.IsStaticEntity())
            {


                //propertyBuilder.Property.F

                _ = 0;
            }

            return base.VisitProperty(propertyBuilder);
        }

        #endregion Methods

        #region - - - - - - Nested Classes - - - - - -

        private class EmptyConverter : ValueConverter
        {

            #region - - - - - - Fields - - - - - -

            private readonly Type m_ModelClrType;
            private readonly Type m_ProviderClrType;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public EmptyConverter(Navigation navigationToStaticEntity, EntityType staticEntityType) : base(GetExpression(), GetExpression(), null)
            {
                this.m_ModelClrType = navigationToStaticEntity.PropertyInfo.PropertyType;
                this.m_ProviderClrType = staticEntityType.GetKeys().Single().Properties.Single().PropertyInfo.PropertyType;
            }

            #endregion Constructors

            #region - - - - - - Properties - - - - - -

            public override Func<object, object> ConvertFromProvider
                => (Func<object, object>)this.ConvertFromProviderExpression.Compile();

            public override Func<object, object> ConvertToProvider
                => (Func<object, object>)this.ConvertToProviderExpression.Compile();

            public override Type ModelClrType => this.m_ModelClrType;

            public override Type ProviderClrType => this.m_ProviderClrType;

            #endregion Properties

            #region - - - - - - Methods - - - - - -

            private static Expression<Func<object, object>> GetExpression() => o => default;

            #endregion Methods

        }

        #endregion Nested Classes

    }

}
