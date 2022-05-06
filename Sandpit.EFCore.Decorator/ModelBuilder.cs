using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sandpit.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace Sandpit.EFCore.Decorator
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class ModelBuilder : Microsoft.EntityFrameworkCore.ModelBuilder
    {

        #region - - - - - - Fields - - - - - -

        private static readonly Func<Model, SortedDictionary<string, EntityType>> s_EntityTypes
            = FieldAccessor.Get<Model, SortedDictionary<string, EntityType>>("_entityTypes");

        private static readonly Func<Model, Dictionary<string, ConfigurationSource>> s_IgnoredTypeNames
            = FieldAccessor.Get<Model, Dictionary<string, ConfigurationSource>>("_ignoredTypeNames");

        private readonly Dictionary<Type, EntityType> m_IgnoredEntityTypes
            = new Dictionary<Type, EntityType>();

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ModelBuilder([NotNull] ConventionSet conventions) : base(conventions)
        {
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Action AfterFinaliseModelAction { get; set; }

        public Action BeforeFinaliseModelAction { get; set; }

        private new Model Model => (Model)base.Model;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void AddEntityType(EntityType entityType)
            => s_EntityTypes(this.Model).Add(entityType.Name, entityType);

        public override IModel FinalizeModel()
        {
            this.BeforeFinaliseModelAction?.Invoke();
            var _Model = base.FinalizeModel();
            this.AfterFinaliseModelAction?.Invoke();

            return _Model;
        }

        public EntityType GetEntityType(Type clrType)
            => s_EntityTypes(this.Model).TryGetValue(clrType.FullName, out var _EntityType) ||
                this.m_IgnoredEntityTypes.TryGetValue(clrType, out _EntityType)
                ? _EntityType
                : null;

        public IEnumerable<EntityType> GetEntityTypes()
            => s_EntityTypes(this.Model).Values.ToList();

        public void IgnoreEntityType(EntityType entityType)
        {
            this.RemoveEntityType(entityType);
            s_IgnoredTypeNames(this.Model).Add(entityType.ClrType.FullName, ConfigurationSource.Explicit);
            this.m_IgnoredEntityTypes[entityType.ClrType] = entityType;
        }

        public void RemoveEntityType(EntityType entityType)
            => s_EntityTypes(this.Model).Remove(entityType.Name);


        #endregion Methods

    }

}
