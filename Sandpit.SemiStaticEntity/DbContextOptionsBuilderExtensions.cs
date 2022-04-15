using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Sandpit.SemiStaticEntity
{

    public static class DbContextOptionsBuilderExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static DbContextOptionsBuilder WithStaticEntities(this DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.ReplaceService<IShapedQueryCompilingExpressionVisitorFactory, ShapedQueryCompilingExpressionVisitorFactoryReplacement>();
            return optionsBuilder;
        }

        #endregion Methods

    }

}
