using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Sandpit.SemiStaticEntity.Extensions;
using Sandpit.SemiStaticEntity.Internal;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sandpit.SemiStaticEntity.Decorators
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class EntityFinderSource : IEntityFinderSource
    {

        #region - - - - - - Fields - - - - - -

        private readonly IEntityFinderSource m_EntityFinderSource = new Microsoft.EntityFrameworkCore.Internal.EntityFinderSource();

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public IEntityFinder Create(
            IStateManager stateManager,
            IDbSetSource setSource,
            IDbSetCache setCache,
            IEntityType type)
            => type.IsStaticEntity()
                ? (IEntityFinder)Activator.CreateInstance(typeof(StaticEntityFinder<>).MakeGenericType(type.ClrType), stateManager.Context, type)
                : this.m_EntityFinderSource.Create(stateManager, setSource, setCache, type);

        #endregion Methods

    }

}
