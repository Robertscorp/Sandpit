using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Sandpit.SemiStaticEntity
{

    public static class DbContextOptionsBuilderExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static DbContextOptionsBuilder EnableDecorators(this DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.ReplaceService<IModelSource, ModelSource>();

        #endregion Methods

    }

}
