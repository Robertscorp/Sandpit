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

            //var _Bar = _PersistenceContext.Set<Bar>().ToList().FirstOrDefault();

            //_PersistenceContext.Add(new Foo() { ID = 1, Bar = _Bar });
            //_PersistenceContext.SaveChanges();

            using var _ServiceProvider2
                = new ServiceCollection()
                        .AddDbContext<PersistenceContext>(opts => opts.UseSqlite("Data Source=Database.db"))
                        .BuildServiceProvider();

            var _PersistenceContext2 = _ServiceProvider2.GetService<PersistenceContext>()!;
            //_PersistenceContext2.Database.Migrate();


            foreach (var _X in _PersistenceContext.Set<Bar>().Select(b => b.Test).ToList())
            {
                _ = 0;
            }

            //var _YX = _PersistenceContext.Set<Bar>().ToList();
            //var _XX = _PersistenceContext.Set<Foo>().ToList();

            foreach (var _X in _PersistenceContext.Set<Foo>().Select(x => x.Bar)) //.Select(x => x.JSemiStaticEntity).AsEnumerable())
            //foreach (var _X in _PersistenceContext.Set<SemiStaticEntityOwner>()) //.Select(x => x.JSemiStaticEntity).AsEnumerable())
            {
                _ = 0;
            }

            foreach (var _X in _PersistenceContext.Set<SemiStaticEntityOwner>().Select(x => x.JSemiStaticEntity.Name).AsEnumerable())
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
