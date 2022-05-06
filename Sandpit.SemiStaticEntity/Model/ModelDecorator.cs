using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandpit.SemiStaticEntity.Modelx
{

    public class ModelDecorator : IModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly IModel m_Model;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ModelDecorator(IModel model)
            => this.m_Model = model ?? throw new ArgumentNullException(nameof(model));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        // TODO
        public IAnnotation FindAnnotation(string name)
            => new AnnotationDecorator(this.m_Model.FindAnnotation(name));

        // TODO
        public IEntityType FindEntityType(string name)
            => new EntityTypeDecorator(this.m_Model.FindEntityType(name), this);

        // TODO
        public IEntityType FindEntityType(string name, string definingNavigationName, IEntityType definingEntityType)
            => this.m_Model.FindEntityType(name, definingNavigationName, definingEntityType);

        // TODO
        public IEnumerable<IAnnotation> GetAnnotations()
            => this.m_Model.GetAnnotations().Select(a => new AnnotationDecorator(a));

        // TODO
        public IEnumerable<IEntityType> GetEntityTypes()
            => this.m_Model.GetEntityTypes().Select(e => new EntityTypeDecorator(e, this));

        #endregion Methods

        #region - - - - - - Operators - - - - - -

        // TODO
        public object this[string name] => this.m_Model[name];

        public static implicit operator Microsoft.EntityFrameworkCore.Metadata.Internal.Model(ModelDecorator decorator)
            => null;

        #endregion Operators

    }

}
