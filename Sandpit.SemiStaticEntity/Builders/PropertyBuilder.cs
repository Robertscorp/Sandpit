using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sandpit.SemiStaticEntity.Builders
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class PropertyBuilder
    {

        #region - - - - - - Constructors - - - - - -

        public PropertyBuilder(Property property)
            => this.Property = property ?? throw new ArgumentNullException(nameof(property));

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Model Model => this.Property.DeclaringEntityType.Model;

        public Property Property { get; set; }

        public EntityType PropertyEntityType => this.Model.FindEntityType(this.Property.ClrType);

        #endregion Properties

    }

}
