using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sandpit.Console.Entities;
using Sandpit.Console.Persistence;

using var _ServiceProvider
    = new ServiceCollection()
            .AddDbContext<PersistenceContext>(opts => opts.UseSqlite("Data Source=Database.db"))
            .BuildServiceProvider();

var _PersistenceContext = _ServiceProvider.GetService<PersistenceContext>()!;
_PersistenceContext.Database.Migrate();

foreach (var _X in _PersistenceContext.Set<Child>().AsEnumerable())
{
    _ = 0;
}

_ = 0;
//var _Context = (PersistenceContext)_PersistenceContext;
//_Context.Database.Migrate();

