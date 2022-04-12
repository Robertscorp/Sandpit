using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sandpit.Console.Entities;
using Sandpit.Console.Persistence;
using System.Linq;


namespace Sandpit.Console
{

    internal class Program
    {
        static void Main(string[] args)
        {
            using var _ServiceProvider
                = new ServiceCollection()
                        .AddDbContext<PersistenceContext>(opts => opts.UseSqlite("Data Source=Database.db"))
                        .BuildServiceProvider();

            var _PersistenceContext = _ServiceProvider.GetService<PersistenceContext>()!;
            _PersistenceContext.Database.Migrate();

            using var _ServiceProvider2
                = new ServiceCollection()
                        .AddDbContext<PersistenceContext>(opts => opts.UseSqlite("Data Source=Database.db"))
                        .BuildServiceProvider();

            var _PersistenceContext2 = _ServiceProvider2.GetService<PersistenceContext>()!;
            //_PersistenceContext2.Database.Migrate();



            foreach (var _X in _PersistenceContext.Set<SemiStaticEntityOwner>().AsEnumerable())
            {
                _ = 0;
            }

            //foreach (var _X in _PersistenceContext.Set<SemiStaticEntityOwner>().AsEnumerable())
            //{
            //    _ = 0;
            //}

            foreach (var _X in _PersistenceContext2.Set<SemiStaticEntityOwner>().AsEnumerable())
            {
                _ = 0;
            }

            _ = 0;
            //var _Context = (PersistenceContext)_PersistenceContext;
            //_Context.Database.Migrate();



        }
    }

}
