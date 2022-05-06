using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Sandpit.SemiStaticEntity.Builders;
using Sandpit.SemiStaticEntity.Visitors;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sandpit.SemiStaticEntity.Decorators
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class MigrationsModelDiffer : IMigrationsModelDiffer
    {

        #region - - - - - - Fields - - - - - -

        private readonly Microsoft.EntityFrameworkCore.Migrations.Internal.MigrationsModelDiffer m_Differ;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public MigrationsModelDiffer(
            IRelationalTypeMappingSource typeMappingSource,
            IMigrationsAnnotationProvider migrationsAnnotations,
            IChangeDetector changeDetector,
            IUpdateAdapterFactory updateAdapterFactory,
            CommandBatchPreparerDependencies commandBatchPreparerDependencies)
            => this.m_Differ = new Microsoft.EntityFrameworkCore.Migrations.Internal.MigrationsModelDiffer(
                typeMappingSource, migrationsAnnotations, changeDetector, updateAdapterFactory, commandBatchPreparerDependencies);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public IReadOnlyList<MigrationOperation> GetDifferences(IModel source, IModel target)
        {
            if (target != null)
                _ = new MigrationsModelDifferModelVisitor().Visit(new ModelBuilder((Model)target));

            return ((IMigrationsModelDiffer)this.m_Differ).GetDifferences(source, target);
        }

        public bool HasDifferences(IModel source, IModel target)
            => ((IMigrationsModelDiffer)this.m_Differ).HasDifferences(source, target);

        #endregion Methods

    }

}
