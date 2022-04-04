using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sandpit.Console.Persistence
{

    internal class PersistenceContextFactory : IDesignTimeDbContextFactory<PersistenceContext>
    {

        #region - - - - - - Methods - - - - - -

        public PersistenceContext CreateDbContext(string[] args)
            => new(new DbContextOptionsBuilder<PersistenceContext>()
                        .UseSqlite("Data Source=Database.db")
                        .Options);

        #endregion Methods

    }

}
