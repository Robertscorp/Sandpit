using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;

namespace Sandpit.SemiStaticEntity.Modelx
{

    // TODO: Decorate Interfaces.
    public class EntityTypeDecorator : IEntityType
    {

        #region - - - - - - Fields - - - - - -

        private readonly IEntityType m_EntityType;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public EntityTypeDecorator(IEntityType entityType, ModelDecorator model)
        {
            this.m_EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public IEntityType BaseType => this.m_EntityType.BaseType;

        public string DefiningNavigationName => this.m_EntityType.DefiningNavigationName;

        public IEntityType DefiningEntityType => this.m_EntityType.DefiningEntityType;

        public IModel Model { get; private set; }

        public string Name => this.m_EntityType.Name;

        public Type ClrType => this.m_EntityType.ClrType;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public IAnnotation FindAnnotation(string name)
            => this.m_EntityType.FindAnnotation(name);

        public IForeignKey FindForeignKey(IReadOnlyList<IProperty> properties, IKey principalKey, IEntityType principalEntityType)
            => this.m_EntityType.FindForeignKey(properties, principalKey, principalEntityType);

        public IIndex FindIndex(IReadOnlyList<IProperty> properties)
            => this.m_EntityType.FindIndex(properties);

        public IKey FindKey(IReadOnlyList<IProperty> properties)
            => this.m_EntityType.FindKey(properties);

        public IKey FindPrimaryKey()
            => this.m_EntityType.FindPrimaryKey();

        public IProperty FindProperty(string name)
            => this.m_EntityType.FindProperty(name);

        public IServiceProperty FindServiceProperty(string name)
            => this.m_EntityType.FindServiceProperty(name);

        public IEnumerable<IAnnotation> GetAnnotations()
            => this.m_EntityType.GetAnnotations();

        public IEnumerable<IForeignKey> GetForeignKeys()
            => this.m_EntityType.GetForeignKeys();

        public IEnumerable<IIndex> GetIndexes()
            => this.m_EntityType.GetIndexes();

        public IEnumerable<IKey> GetKeys()
            => this.m_EntityType.GetKeys();

        public IEnumerable<IProperty> GetProperties()
            => this.m_EntityType.GetProperties();

        public IEnumerable<IServiceProperty> GetServiceProperties()
            => this.m_EntityType.GetServiceProperties();

        #endregion Methods

        #region - - - - - - Operators - - - - - -

        public object this[string name] => this.m_EntityType[name];

        #endregion Operators

    }

}
