using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sandpit.SemiStaticEntity.Replacements
{

    public class ModelSource : Microsoft.EntityFrameworkCore.Infrastructure.ModelSource
    {

        #region - - - - - - Constructors - - - - - -

        public ModelSource([NotNull] ModelSourceDependencies dependencies) : base(dependencies)
        {
            this.ConventionSetFactory = () => this.ConventionSetBuilder?.CreateConventionSet();
            this.ModelBuilderFactory = () => new Builders.ModelBuilder(this.ConventionSetFactory());
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public IConventionSetBuilder ConventionSetBuilder { get; set; }

        public Func<ConventionSet> ConventionSetFactory { get; set; }

        public Func<ModelBuilder> ModelBuilderFactory { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        protected override IModel CreateModel(DbContext context, IConventionSetBuilder conventionSetBuilder)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            var _ModelBuilder = this.ModelBuilderFactory();

            this.Dependencies.ModelCustomizer.Customize(_ModelBuilder, context);

            return _ModelBuilder.FinalizeModel();
        }

        public override IModel GetModel(DbContext context, IConventionSetBuilder conventionSetBuilder)
        {
            if (this.ConventionSetBuilder == null)
                this.ConventionSetBuilder = conventionSetBuilder;

            return base.GetModel(context, conventionSetBuilder);
        }

        #endregion Methods

    }

}
