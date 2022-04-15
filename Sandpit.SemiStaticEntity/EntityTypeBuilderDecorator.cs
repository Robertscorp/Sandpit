//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;

//namespace Sandpit.SemiStaticEntity
//{

//    public class EntityTypeBuilderDecorator<TEntity> : EntityTypeBuilder<TEntity> where TEntity : class
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly EntityTypeBuilder<TEntity> m_EntityTypeBuilder;

//        #endregion Fields

//        #region - - - - - - Constructors - - - - - -

//        public EntityTypeBuilderDecorator(EntityTypeBuilder<TEntity> entityTypeBuilder) : base(entityTypeBuilder.Metadata)
//            => this.m_EntityTypeBuilder = entityTypeBuilder;

//        #endregion Constructors

//        #region - - - - - - Properties - - - - - -

//        public override IMutableEntityType Metadata => this.m_EntityTypeBuilder.Metadata;

//        #endregion Properties

//        #region - - - - - - Methods - - - - - -

//        public override KeyBuilder HasAlternateKey(Expression<Func<TEntity, object>> keyExpression)
//            => this.m_EntityTypeBuilder.HasAlternateKey(keyExpression);

//        public override KeyBuilder HasAlternateKey(params string[] propertyNames)
//            => this.m_EntityTypeBuilder.HasAlternateKey(propertyNames);

//        public override EntityTypeBuilder<TEntity> HasAnnotation(string annotation, object value)
//            => this.m_EntityTypeBuilder.HasAnnotation(annotation, value);

//        public override EntityTypeBuilder<TEntity> HasBaseType(string name)
//            => this.m_EntityTypeBuilder.HasBaseType(name);

//        public override EntityTypeBuilder<TEntity> HasBaseType(Type entityType)
//            => this.m_EntityTypeBuilder.HasBaseType(entityType);

//        public override EntityTypeBuilder<TEntity> HasBaseType<TBaseType>()
//            => this.m_EntityTypeBuilder.HasBaseType<TBaseType>();

//        public override EntityTypeBuilder<TEntity> HasChangeTrackingStrategy(ChangeTrackingStrategy changeTrackingStrategy)
//            => this.m_EntityTypeBuilder.HasChangeTrackingStrategy(changeTrackingStrategy);

//        public override DataBuilder<TEntity> HasData(params object[] data)
//            => this.m_EntityTypeBuilder.HasData(data);

//        public override DataBuilder<TEntity> HasData(params TEntity[] data)
//            => this.m_EntityTypeBuilder.HasData(data);

//        public override DataBuilder<TEntity> HasData(IEnumerable<object> data)
//            => this.m_EntityTypeBuilder.HasData(data);

//        public override DataBuilder<TEntity> HasData(IEnumerable<TEntity> data)
//            => this.m_EntityTypeBuilder.HasData(data);

//        public override DiscriminatorBuilder HasDiscriminator()
//            => this.m_EntityTypeBuilder.HasDiscriminator();

//        public override DiscriminatorBuilder HasDiscriminator(string name, Type type)
//            => this.m_EntityTypeBuilder.HasDiscriminator(name, type);

//        public override DiscriminatorBuilder<TDiscriminator> HasDiscriminator<TDiscriminator>(Expression<Func<TEntity, TDiscriminator>> propertyExpression)
//            => this.m_EntityTypeBuilder.HasDiscriminator(propertyExpression);

//        public override DiscriminatorBuilder<TDiscriminator> HasDiscriminator<TDiscriminator>(string name)
//            => this.m_EntityTypeBuilder.HasDiscriminator<TDiscriminator>(name);

//        public override IndexBuilder<TEntity> HasIndex(Expression<Func<TEntity, object>> indexExpression)
//            => this.m_EntityTypeBuilder.HasIndex(indexExpression);

//        public override IndexBuilder HasIndex(params string[] propertyNames)
//            => this.m_EntityTypeBuilder.HasIndex(propertyNames);

//        public override KeyBuilder HasKey(Expression<Func<TEntity, object>> keyExpression)
//            => this.m_EntityTypeBuilder.HasKey(keyExpression);

//        public override KeyBuilder HasKey(params string[] propertyNames)
//            => this.m_EntityTypeBuilder.HasKey(propertyNames);

//        public override CollectionNavigationBuilder HasMany(string navigationName)
//            => this.m_EntityTypeBuilder.HasMany(navigationName);

//        public override CollectionNavigationBuilder HasMany(string relatedTypeName, string navigationName)
//            => this.m_EntityTypeBuilder.HasMany(relatedTypeName, navigationName);

//        public override CollectionNavigationBuilder HasMany(Type relatedType, string navigationName = null)
//            => this.m_EntityTypeBuilder.HasMany(relatedType, navigationName);

//        public override CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> navigationExpression = null)
//            => this.m_EntityTypeBuilder.HasMany(navigationExpression);

//        public override CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(string navigationName)
//            => this.m_EntityTypeBuilder.HasMany<TRelatedEntity>(navigationName);

//        public override EntityTypeBuilder<TEntity> HasNoDiscriminator()
//            => this.m_EntityTypeBuilder.HasNoDiscriminator();

//        public override EntityTypeBuilder<TEntity> HasNoKey()
//            => this.m_EntityTypeBuilder.HasNoKey();

//        public override ReferenceNavigationBuilder HasOne(string navigationName)
//            => this.m_EntityTypeBuilder.HasOne(navigationName);

//        public override ReferenceNavigationBuilder HasOne(string relatedTypeName, string navigationName)
//            => this.m_EntityTypeBuilder.HasOne(relatedTypeName, navigationName);

//        public override ReferenceNavigationBuilder HasOne(Type relatedType, string navigationName = null)
//            => this.m_EntityTypeBuilder.HasOne(relatedType, navigationName);

//        public override ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> navigationExpression = null)
//            => this.m_EntityTypeBuilder.HasOne(navigationExpression);

//        public override ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(string navigationName)
//            => this.m_EntityTypeBuilder.HasOne<TRelatedEntity>(navigationName);

//        public override EntityTypeBuilder HasQueryFilter(LambdaExpression filter)
//            => this.m_EntityTypeBuilder.HasQueryFilter(filter);

//        public override EntityTypeBuilder<TEntity> HasQueryFilter(Expression<Func<TEntity, bool>> filter)
//            => this.m_EntityTypeBuilder.HasQueryFilter(filter);

//        public override EntityTypeBuilder<TEntity> Ignore(Expression<Func<TEntity, object>> propertyExpression)
//            => this.m_EntityTypeBuilder.Ignore(propertyExpression);

//        public override EntityTypeBuilder<TEntity> Ignore(string propertyName)
//            => this.m_EntityTypeBuilder.Ignore(propertyName);

//        public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, string navigationName)
//            => this.m_EntityTypeBuilder.OwnsMany(ownedTypeName, navigationName);

//        public override EntityTypeBuilder OwnsMany(string ownedTypeName, string navigationName, Action<OwnedNavigationBuilder> buildAction)
//            => this.m_EntityTypeBuilder.OwnsMany(ownedTypeName, navigationName, buildAction);

//        public override OwnedNavigationBuilder OwnsMany(Type ownedType, string navigationName)
//            => this.m_EntityTypeBuilder.OwnsMany(ownedType, navigationName);

//        public override EntityTypeBuilder OwnsMany(Type ownedType, string navigationName, Action<OwnedNavigationBuilder> buildAction)
//            => this.m_EntityTypeBuilder.OwnsMany(ownedType, navigationName, buildAction);

//        public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> navigationExpression)
//            => this.m_EntityTypeBuilder.OwnsMany(navigationExpression);

//        public override EntityTypeBuilder<TEntity> OwnsMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> navigationExpression, Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
//            => this.m_EntityTypeBuilder.OwnsMany(navigationExpression, buildAction);

//        public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsMany<TRelatedEntity>(string navigationName)
//            => this.m_EntityTypeBuilder.OwnsMany<TRelatedEntity>(navigationName);

//        public override EntityTypeBuilder<TEntity> OwnsMany<TRelatedEntity>(string navigationName, Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
//            => this.m_EntityTypeBuilder.OwnsMany(navigationName, buildAction);

//        public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, string navigationName)
//            => this.m_EntityTypeBuilder.OwnsOne(ownedTypeName, navigationName);

//        public override EntityTypeBuilder OwnsOne(string ownedTypeName, string navigationName, Action<OwnedNavigationBuilder> buildAction)
//            => this.m_EntityTypeBuilder.OwnsOne(ownedTypeName, navigationName, buildAction);

//        public override OwnedNavigationBuilder OwnsOne(Type ownedType, string navigationName)
//            => this.m_EntityTypeBuilder.OwnsOne(ownedType, navigationName);

//        public override EntityTypeBuilder OwnsOne(Type ownedType, string navigationName, Action<OwnedNavigationBuilder> buildAction)
//            => this.m_EntityTypeBuilder.OwnsOne(ownedType, navigationName, buildAction);

//        public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> navigationExpression)
//            => this.m_EntityTypeBuilder.OwnsOne(navigationExpression);

//        public override EntityTypeBuilder<TEntity> OwnsOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> navigationExpression, Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
//            => this.m_EntityTypeBuilder.OwnsOne(navigationExpression, buildAction);

//        public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsOne<TRelatedEntity>(string navigationName)
//            => this.m_EntityTypeBuilder.OwnsOne<TRelatedEntity>(navigationName);

//        public override EntityTypeBuilder<TEntity> OwnsOne<TRelatedEntity>(string navigationName, Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
//            => this.m_EntityTypeBuilder.OwnsOne(navigationName, buildAction);

//        public override PropertyBuilder Property(string propertyName)
//            //=> this.m_EntityTypeBuilder.Property(propertyName);
//            => throw new NotImplementedException(); // TODO

//        public override PropertyBuilder Property(Type propertyType, string propertyName)
//            //=> this.m_EntityTypeBuilder.Property(propertyType, propertyName);
//            => throw new NotImplementedException(); // TODO

//        public override PropertyBuilder<TProperty> Property<TProperty>(string propertyName)
//            => new PropertyBuilderDecorator<TProperty>(this.m_EntityTypeBuilder.Property<TProperty>(propertyName));

//        public override PropertyBuilder<TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
//            => new PropertyBuilderDecorator<TProperty>(this.m_EntityTypeBuilder.Property<TProperty>(propertyExpression));

//        public override EntityTypeBuilder<TEntity> ToQuery(Expression<Func<IQueryable<TEntity>>> query)
//            => this.m_EntityTypeBuilder.ToQuery(query);

//        public override EntityTypeBuilder<TEntity> UsePropertyAccessMode(PropertyAccessMode propertyAccessMode)
//            => this.m_EntityTypeBuilder.UsePropertyAccessMode(propertyAccessMode);

//        #endregion Methods

//    }

//}
