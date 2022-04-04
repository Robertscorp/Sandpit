using Microsoft.EntityFrameworkCore;
using Sandpit.Console.Entities;
using System.Collections;
using System.Linq.Expressions;

namespace Sandpit.Console.Persistence
{

    public class ChildSet : DbSet<Child>, IQueryable<Child>
    {

        #region - - - - - - Methods - - - - - -

        Type IQueryable.ElementType
            => throw new NotImplementedException();

        Expression IQueryable.Expression
            => throw new NotImplementedException();

        IQueryProvider IQueryable.Provider
            => throw new NotImplementedException();

        IEnumerator<Child> IEnumerable<Child>.GetEnumerator()
            => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator()
            => throw new NotImplementedException();

        #endregion Methods

    }

    //public class TestSet : DbSet

}
