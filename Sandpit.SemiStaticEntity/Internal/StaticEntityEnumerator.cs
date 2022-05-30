using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sandpit.SemiStaticEntity.Extensions;
using Sandpit.SemiStaticEntity.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sandpit.SemiStaticEntity.Internal
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class StaticEntityEnumerator<TStaticEntity> : IEnumerator, IEnumerator<TStaticEntity> where TStaticEntity : class
    {

        #region - - - - - - Fields - - - - - -

        private static readonly Func<EntityType, List<object>> s_EntityTypeData
            = FieldAccessor.Get<EntityType, List<object>>("_data");

        private readonly Func<TStaticEntity, TStaticEntity> m_GetOrAttachEntityFunc;
        private readonly IEnumerator<TStaticEntity> m_StaticEntitiesEnumerator;
        private readonly IEnumerator<TStaticEntity> m_SemiStaticEntitiesEnumerator;

        private TStaticEntity m_Current;
        private bool m_IsDisposed;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public StaticEntityEnumerator(DbContext dbContext)
        {
            if (dbContext is null) throw new ArgumentNullException(nameof(dbContext));

            if (!(dbContext.Model.FindEntityType(typeof(TStaticEntity)) is EntityType _StaticEntityType))
                throw new Exception($"{typeof(TStaticEntity)} is not on the Model.");

            this.m_StaticEntitiesEnumerator = s_EntityTypeData(_StaticEntityType).OfType<TStaticEntity>().ToList().GetEnumerator();
            this.m_SemiStaticEntitiesEnumerator
                = (_StaticEntityType.FindAnnotation("StaticEntity.HasDynamicData").Value as Func<DbContext, IEnumerable<TStaticEntity>>)?
                    .Invoke(dbContext)
                    .GetEnumerator()
                        ?? Enumerable.Empty<TStaticEntity>().GetEnumerator();

            var _EntityKeyFunc = _StaticEntityType.GetPrimaryKeyValuesFunc<TStaticEntity>();

            this.m_GetOrAttachEntityFunc = entity =>
            {
                var _FoundEntity = dbContext
                                    .Set<TStaticEntity>()
                                    .Local
                                    .FirstOrDefault(e => _EntityKeyFunc(e).SequenceEqual(_EntityKeyFunc(entity)));

                return _FoundEntity ?? dbContext.Attach(entity).Entity;
            };
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        TStaticEntity IEnumerator<TStaticEntity>.Current
        {
            get
            {
                this.CheckIfDisposed();
                return this.m_Current;
            }
        }

        object IEnumerator.Current => ((IEnumerator<TStaticEntity>)this).Current;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        private void CheckIfDisposed()
        {
            if (this.m_IsDisposed)
                throw new ObjectDisposedException(nameof(StaticEntityEnumerator<TStaticEntity>));
        }

        void IDisposable.Dispose()
        {
            this.CheckIfDisposed();
            this.m_IsDisposed = true;
        }

        bool IEnumerator.MoveNext()
        {
            this.CheckIfDisposed();
            return this.MoveNext(this.m_StaticEntitiesEnumerator) || this.MoveNext(this.m_SemiStaticEntitiesEnumerator);
        }

        void IEnumerator.Reset()
        {
            this.CheckIfDisposed();
            this.m_Current = default;
            this.m_StaticEntitiesEnumerator.Reset();
            this.m_SemiStaticEntitiesEnumerator.Reset();
        }

        private bool MoveNext(IEnumerator<TStaticEntity> staticEntityEnumerator)
        {
            if (!staticEntityEnumerator.MoveNext())
                return false;

            this.m_Current = this.m_GetOrAttachEntityFunc(staticEntityEnumerator.Current);

            return true;
        }

        #endregion Methods

    }

}
