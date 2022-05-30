using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sandpit.SemiStaticEntity.Internal
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class StaticEntityFinder<TStaticEntity> : IEntityFinder<TStaticEntity> where TStaticEntity : class
    {

        #region - - - - - - Fields - - - - - -

        private readonly DbSet<TStaticEntity> m_DbSet;
        private readonly Func<TStaticEntity, object[], bool> m_EntityFindFunc;
        private readonly IEntityType m_EntityType;
        private readonly IReadOnlyCollection<IProperty> m_PrimaryKeyProperties;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public StaticEntityFinder(DbContext dbContext, IEntityType entityType)
        {
            if (dbContext is null) throw new ArgumentNullException(nameof(dbContext));

            this.m_DbSet = dbContext.Set<TStaticEntity>();
            this.m_EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
            this.m_PrimaryKeyProperties = this.m_EntityType.FindPrimaryKey().Properties;

            this.m_EntityFindFunc = this.GetEntityFindFunc();
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        object IEntityFinder.Find(object[] keyValues)
            => ((IEntityFinder<TStaticEntity>)this).Find(keyValues);

        ValueTask<object> IEntityFinder.FindAsync(object[] keyValues, CancellationToken cancellationToken)
            => new ValueTask<object>(((IEntityFinder<TStaticEntity>)this).Find(keyValues));

        TStaticEntity IEntityFinder<TStaticEntity>.Find(object[] keyValues)
            => this.m_PrimaryKeyProperties.Count != keyValues.Length ||
                this.m_PrimaryKeyProperties.Zip(keyValues).Any(pv => !pv.First.ClrType.IsAssignableFrom(pv.Second.GetType()))
                ? null
                : this.m_DbSet.Local.FirstOrDefault(e => this.m_EntityFindFunc(e, keyValues))
                    ?? this.m_DbSet.FirstOrDefault(e => this.m_EntityFindFunc(e, keyValues));

        ValueTask<TStaticEntity> IEntityFinder<TStaticEntity>.FindAsync(object[] keyValues, CancellationToken cancellationToken)
            => new ValueTask<TStaticEntity>(((IEntityFinder<TStaticEntity>)this).Find(keyValues));

        object[] IEntityFinder.GetDatabaseValues(InternalEntityEntry entry)
            => throw new NotImplementedException();

        Task<object[]> IEntityFinder.GetDatabaseValuesAsync(InternalEntityEntry entry, CancellationToken cancellationToken)
            => Task.FromResult(((IEntityFinder)this).GetDatabaseValues(entry));

        void IEntityFinder.Load(INavigation navigation, InternalEntityEntry entry)
            => throw new NotImplementedException();

        Task IEntityFinder.LoadAsync(INavigation navigation, InternalEntityEntry entry, CancellationToken cancellationToken)
        {
            ((IEntityFinder)this).Load(navigation, entry);
            return Task.CompletedTask;
        }

        IQueryable IEntityFinder.Query(INavigation navigation, InternalEntityEntry entry)
            => ((IEntityFinder<TStaticEntity>)this).Query(navigation, entry);

        IQueryable<TStaticEntity> IEntityFinder<TStaticEntity>.Query(INavigation navigation, InternalEntityEntry entry)
            => throw new NotImplementedException();


        private Func<TStaticEntity, object[], bool> GetEntityFindFunc()
        {
            var _StaticEntity = Expression.Parameter(typeof(TStaticEntity));
            var _Param = Expression.Parameter(typeof(object[]));

            return Expression
                .Lambda<Func<TStaticEntity, object[], bool>>(
                    this.m_EntityType
                        .FindPrimaryKey()
                        .Properties
                        .Select((p, i)
                            => Expression.Equal(
                                Expression.Property(_StaticEntity, p.PropertyInfo),
                                Expression.Convert(Expression.ArrayIndex(_Param, Expression.Constant(i)), p.PropertyInfo.PropertyType)))
                        .Aggregate((agg, inc) => Expression.AndAlso(agg, inc)),
                    _StaticEntity,
                    _Param)
                .Compile();
        }

        #endregion Methods

    }

}
