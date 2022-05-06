using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Sandpit.SemiStaticEntity.Modelx
{

    public class AnnotationDecorator : IAnnotation
    {

        #region - - - - - - Fields - - - - - -

        private readonly IAnnotation m_Annotation;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public AnnotationDecorator(IAnnotation annotation)
            => this.m_Annotation = annotation ?? throw new ArgumentNullException(nameof(annotation));

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public string Name => this.m_Annotation.Name;

        public object Value => this.m_Annotation.Value;

        #endregion Properties

    }

}
