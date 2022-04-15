//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore.Metadata.Conventions;
//using System;
//using System.Linq;

//namespace Sandpit.SemiStaticEntity
//{

//    public class ModelBuilderDecorator : ModelBuilder
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly ModelBuilder m_ModelBuilder;

//        #endregion Fields

//        #region - - - - - - Constructors - - - - - -

//        public ModelBuilderDecorator(ModelBuilder modelBuilder) : base(new ConventionSet())
//            => this.m_ModelBuilder = modelBuilder;

//        #endregion Constructors

//        #region - - - - - - Methods - - - - - -

//        public override ModelBuilder ApplyConfiguration<TEntity>(IEntityTypeConfiguration<TEntity> configuration)
//        {
//            var _ModelBuilder = base.ApplyConfiguration(configuration);
//            var _Metadata = _ModelBuilder.Entity<TEntity>().Metadata;

//            foreach (var _Property in _Metadata.GetProperties().Where(p => p.FindAnnotation("TypeMapping2") != null).ToList())
//            {
//                //var _RelationalTypeMapping = (RelationalTypeMapping)_Property["TypeMapping"];

//                //_Property["TypeMapping"] = new RelationalTypeMappingDecorator(_RelationalTypeMapping);

//                var _XX = 0;
//                //_Metadata.RemoveProperty(_Property);
//                //_Metadata.AddProperty()

//            }


//            return _ModelBuilder;
//        }

//        //[Obsolete]
//        //public override ModelBuilder ApplyConfiguration<TQuery>(IQueryTypeConfiguration<TQuery> configuration)
//        //    => base.ApplyConfiguration(configuration);

//        //public override ModelBuilder ApplyConfigurationsFromAssembly(Assembly assembly, Func<Type, bool> predicate = null)
//        //    => base.ApplyConfigurationsFromAssembly(assembly, predicate);

//        public override EntityTypeBuilder Entity(string name)
//            => this.Entity(this.m_ModelBuilder.Entity(name).Metadata.ClrType);

//        public override ModelBuilder Entity(string name, Action<EntityTypeBuilder> buildAction)
//            => this.m_ModelBuilder.Entity(name, buildAction);

//        public override EntityTypeBuilder Entity(Type type)
//            //=> Activator.CreateInstance(typeof(EntityTypeBuilderDecorator<>).MakeGenericType(type),
//            //    this.m_ModelBuilder.Entity(type);
//            => throw new NotImplementedException(); // TODO

//        public override ModelBuilder Entity(Type type, Action<EntityTypeBuilder> buildAction)
//        {
//            buildAction(this.Entity(type));
//            return this;
//        }

//        public override EntityTypeBuilder<TEntity> Entity<TEntity>()
//            => this.m_ModelBuilder.Entity<TEntity>();// new EntityTypeBuilderReplacement<TEntity>(this.m_ModelBuilder.Entity<TEntity>().Metadata);

//        public override ModelBuilder Entity<TEntity>(Action<EntityTypeBuilder<TEntity>> buildAction)
//        {
//            buildAction(this.Entity<TEntity>());
//            return this;
//        }

//        public override IModel FinalizeModel()
//            => this.m_ModelBuilder.FinalizeModel();

//        public override ModelBuilder HasAnnotation(string annotation, object value)
//            => this.m_ModelBuilder.HasAnnotation(annotation, value);

//        public override ModelBuilder HasChangeTrackingStrategy(ChangeTrackingStrategy changeTrackingStrategy)
//            => this.m_ModelBuilder.HasChangeTrackingStrategy(changeTrackingStrategy);

//        public override ModelBuilder Ignore(Type type)
//            => this.m_ModelBuilder.Ignore(type);

//        public override ModelBuilder Ignore<TEntity>()
//            => this.m_ModelBuilder.Ignore<TEntity>();

//        public override IMutableModel Model
//            => this.m_ModelBuilder.Model;

//        public override OwnedEntityTypeBuilder Owned(Type type)
//            => this.m_ModelBuilder.Owned(type);

//        public override OwnedEntityTypeBuilder<T> Owned<T>()
//            => this.m_ModelBuilder.Owned<T>();

//        [Obsolete]
//        public override EntityTypeBuilder Query(Type type)
//            => this.m_ModelBuilder.Query(type);

//        [Obsolete]
//        public override ModelBuilder Query(Type type, Action<EntityTypeBuilder> buildAction)
//            => this.m_ModelBuilder.Query(type, buildAction);

//        [Obsolete]
//        public override QueryTypeBuilder<TQuery> Query<TQuery>()
//            => this.m_ModelBuilder.Query<TQuery>();

//        [Obsolete]
//        public override ModelBuilder Query<TQuery>(Action<QueryTypeBuilder<TQuery>> buildAction)
//            => this.m_ModelBuilder.Query(buildAction);

//        public override ModelBuilder UsePropertyAccessMode(PropertyAccessMode propertyAccessMode)
//            => this.m_ModelBuilder.UsePropertyAccessMode(propertyAccessMode);

//        #endregion Methods

//    }

//}
