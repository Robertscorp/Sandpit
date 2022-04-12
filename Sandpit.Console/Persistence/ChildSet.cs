using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sandpit.Console.Entities;

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
