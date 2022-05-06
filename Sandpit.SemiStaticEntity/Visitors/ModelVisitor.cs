using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sandpit.SemiStaticEntity.Builders;
using System;
using System.Linq;

namespace Sandpit.SemiStaticEntity.Visitors
{

    public abstract class ModelVisitor
    {

        #region - - - - - - Methods - - - - - -

        private void Visit(EntityTypeBuilder entityTypeBuilder, Action<EntityType> newEntityTypeAction)
        {
            var _VisitedEntityType = this.VisitEntityType(entityTypeBuilder);
            if (_VisitedEntityType != entityTypeBuilder.EntityType)
                newEntityTypeAction(_VisitedEntityType);
        }

        public Model Visit(ModelBuilder modelBuilder)
            => this.VisitModel(modelBuilder);

        private void Visit(Property property, Action<Property> newPropertyAction)
        {
            var _VisitedProperty = this.VisitProperty(new PropertyBuilder(property));
            if (_VisitedProperty != property)
                newPropertyAction(_VisitedProperty);
        }

        protected virtual EntityType VisitEntityType(EntityTypeBuilder entityTypeBuilder)
        {
            foreach (var _Property in entityTypeBuilder.GetProperties())
                this.Visit(_Property, newProperty =>
                {
                    entityTypeBuilder.RemoveProperty(_Property);

                    if (newProperty != null)
                        entityTypeBuilder.AddProperty(newProperty);
                });

            return entityTypeBuilder.EntityType;
        }

        protected virtual Property VisitProperty(PropertyBuilder propertyBuilder)
            => propertyBuilder.Property;

        protected virtual Model VisitModel(ModelBuilder modelBuilder)
        {
            foreach (var _EntityTypeBuilder in modelBuilder.GetEntityTypes().Select(et => new EntityTypeBuilder(et, modelBuilder)))
                this.Visit(_EntityTypeBuilder, newType =>
                {
                    modelBuilder.RemoveEntityType(_EntityTypeBuilder.EntityType);

                    if (newType != null)
                        modelBuilder.AddEntityType(newType);
                });

            return (Model)modelBuilder.Model;
        }

        #endregion Methods

    }

}
