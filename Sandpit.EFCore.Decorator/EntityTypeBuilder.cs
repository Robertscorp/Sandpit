using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sandpit.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using MetadataIndex = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Sandpit.EFCore.Decorator
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class EntityTypeBuilder : Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder
    {

        #region - - - - - - Fields - - - - - -

        private static readonly Func<EntityType, SortedSet<ForeignKey>> s_ForeignKeys
            = FieldAccessor.Get<EntityType, SortedSet<ForeignKey>>("_foreignKeys");

        private static readonly Func<EntityType, SortedDictionary<IReadOnlyList<IProperty>, MetadataIndex>> s_Indexes
            = FieldAccessor.Get<EntityType, SortedDictionary<IReadOnlyList<IProperty>, MetadataIndex>>("_indexes");

        private static readonly Func<EntityType, SortedDictionary<IReadOnlyList<IProperty>, Key>> s_Keys
            = FieldAccessor.Get<EntityType, SortedDictionary<IReadOnlyList<IProperty>, Key>>("_keys");

        private static readonly Func<EntityType, SortedDictionary<string, Navigation>> s_Navigations
            = FieldAccessor.Get<EntityType, SortedDictionary<string, Navigation>>("_navigations");

        private static readonly Func<EntityType, SortedDictionary<string, Property>> s_Properties
            = FieldAccessor.Get<EntityType, SortedDictionary<string, Property>>("_properties");

        private static readonly Func<TypeBase, Dictionary<string, FieldInfo>> s_RuntimeFields
            = FieldAccessor.Get<TypeBase, Dictionary<string, FieldInfo>>("_runtimeFields");

        private static readonly Func<TypeBase, Dictionary<string, PropertyInfo>> s_RuntimeProperties
            = FieldAccessor.Get<TypeBase, Dictionary<string, PropertyInfo>>("_runtimeProperties");


        //private readonly SortedDictionary<string, ServiceProperty> _serviceProperties = new SortedDictionary<string, ServiceProperty>(StringComparer.Ordinal);

        //private List<object> _data;

        //private Key _primaryKey;

        //private bool? _isKeyless;

        //private EntityType _baseType;

        //private ConfigurationSource? _primaryKeyConfigurationSource;

        //private ConfigurationSource? _isKeylessConfigurationSource;

        //private ConfigurationSource? _baseTypeConfigurationSource;

        //private PropertyCounts _counts;

        //private readonly SortedSet<EntityType> _directlyDerivedTypes = new SortedSet<EntityType>(EntityTypePathComparer.Instance);

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public EntityTypeBuilder(EntityType entityType, ModelBuilder modelBuilder) : base(entityType)
        {
            this.EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
            this.ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public EntityType EntityType { get; set; }

        public ModelBuilder ModelBuilder { get; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void AddForeignKey(ForeignKey foreignKey)
            => s_ForeignKeys(this.EntityType).Add(foreignKey);

        public void AddIndex(MetadataIndex index)
            => s_Indexes(this.EntityType).Add(index.Properties, index);

        public void AddKey(Key key)
            => s_Keys(this.EntityType).Add(key.Properties, key);

        public void AddNavigation(Navigation navigation)
            => s_Navigations(this.EntityType).Add(navigation.Name, navigation);

        public void AddProperty(Property property)
            => s_Properties(this.EntityType).Add(property.Name, property);

        public void AddRuntimeField(FieldInfo fieldInfo)
            => s_RuntimeFields(this.EntityType).Add(fieldInfo.Name, fieldInfo);

        public void AddRuntimeProperty(PropertyInfo propertyInfo)
            => s_RuntimeProperties(this.EntityType).Add(propertyInfo.Name, propertyInfo);

        public IEnumerable<ForeignKey> GetForeignKeys()
            => s_ForeignKeys(this.EntityType).ToList();

        public IEnumerable<MetadataIndex> GetIndexes()
            => s_Indexes(this.EntityType).Values.ToList();

        public IEnumerable<Key> GetKeys()
            => s_Keys(this.EntityType).Values.ToList();

        public IEnumerable<Navigation> GetNavigations()
            => s_Navigations(this.EntityType).Values.ToList();

        public IEnumerable<Property> GetProperties()
            => s_Properties(this.EntityType).Values.ToList();

        public IEnumerable<FieldInfo> GetRuntimeFields()
            => s_RuntimeFields(this.EntityType).Values.ToList();

        public IEnumerable<PropertyInfo> GetRuntimeProperties()
            => s_RuntimeProperties(this.EntityType).Values.ToList();

        public void RemoveForeignKey(ForeignKey foreignKey)
            => s_ForeignKeys(this.EntityType).Remove(foreignKey);

        public void RemoveIndex(MetadataIndex index)
            => s_Indexes(this.EntityType).Remove(index.Properties);

        public void RemoveKey(Key key)
            => s_Keys(this.EntityType).Remove(key.Properties);

        public void RemoveNavigation(Navigation navigation)
            => s_Navigations(this.EntityType).Remove(navigation.Name);

        public void RemoveProperty(Property property)
            => s_Properties(this.EntityType).Remove(property.Name);

        public void RemoveRuntimeField(FieldInfo fieldInfo)
            => s_RuntimeFields(this.EntityType).Remove(fieldInfo.Name);

        public void RemoveRuntimeProperty(PropertyInfo propertyInfo)
            => s_RuntimeProperties(this.EntityType).Remove(propertyInfo.Name);

        #endregion Methods

    }

}
