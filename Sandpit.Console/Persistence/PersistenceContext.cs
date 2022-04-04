using Microsoft.EntityFrameworkCore;

namespace Sandpit.Console.Persistence
{

    public class PersistenceContext : DbContext
    {

        #region - - - - - - Constructors - - - - - -

        public PersistenceContext(DbContextOptions<PersistenceContext> dbContextOptions) : base(dbContextOptions) { }

        #endregion Constructors


        //public override DbSet<TEntity> Set<TEntity>()
        //    => typeof(TEntity) == typeof(Child)
        //        ? (new ChildSet() as DbSet<TEntity>)!
        //        : base.Set<TEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceContext).Assembly);

            //_ = modelBuilder.Ignore<Parent>();
            //_ = modelBuilder.Ignore<EncapsulateParent1>();
            //_ = modelBuilder.Ignore<EncapsulateParent2>();

            base.OnModelCreating(modelBuilder);
        }

    }

}
