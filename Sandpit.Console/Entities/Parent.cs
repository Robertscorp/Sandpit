using System.Collections.Generic;

namespace Sandpit.Console.Entities
{

    public class Parent
    {

        #region - - - - - - Properties - - - - - -

        public ICollection<Child> Children { get; set; } = new HashSet<Child>();

        #endregion Properties

    }

}
