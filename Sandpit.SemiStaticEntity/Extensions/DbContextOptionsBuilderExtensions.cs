using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Sandpit.SemiStaticEntity.Decorators;

namespace Sandpit.SemiStaticEntity.Extensions
{

    public static class DbContextOptionsBuilderExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static DbContextOptionsBuilder WithStaticEntities(this DbContextOptionsBuilder optionsBuilder)
        {
            //_ = optionsBuilder.ReplaceService<IDbContextServices, DbContextServicesReplacement>();
            _ = optionsBuilder.ReplaceService<IDbSetSource, Replacements.DbSetSource>();
            _ = optionsBuilder.ReplaceService<IEntityFinderSource, Decorators.EntityFinderSource>();
            _ = optionsBuilder.ReplaceService<IMigrationsModelDiffer, MigrationsModelDiffer>();
            _ = optionsBuilder.ReplaceService<IModelSource, Replacements.ModelSource>();
            //_ = optionsBuilder.ReplaceService<IQueryCompilationContextFactory, QueryCompilationContextFactoryDecorator>();
            //_ = optionsBuilder.ReplaceService<IShapedQueryCompilingExpressionVisitorFactory, ShapedQueryCompilingExpressionVisitorFactoryReplacement>();
            return optionsBuilder;
        }

        #endregion Methods

    }

}
