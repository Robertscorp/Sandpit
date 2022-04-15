using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Sandpit.SemiStaticEntity
{

    public class PropertyDecorator : IProperty
    {

        #region - - - - - - Fields - - - - - -

        private readonly IProperty m_Property;
        private readonly Func<RelationalTypeMapping, RelationalTypeMapping> m_RelationalTypeMappingDecoratorFactory;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PropertyDecorator(IProperty property, Expression queryContextParameter)
        {
            if (queryContextParameter is null)
                throw new ArgumentNullException(nameof(queryContextParameter));

            this.m_Property = property ?? throw new ArgumentNullException(nameof(property));
            this.m_RelationalTypeMappingDecoratorFactory = mapping => new RelationalTypeMappingDecorator(mapping, this, queryContextParameter);
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Type ClrType => this.m_Property.ClrType;

        public IEntityType DeclaringEntityType => this.m_Property.DeclaringEntityType;

        public ITypeBase DeclaringType => this.m_Property.DeclaringType;

        public FieldInfo FieldInfo => this.m_Property.FieldInfo;

        public bool IsConcurrencyToken => this.m_Property.IsConcurrencyToken;

        public bool IsNullable => this.m_Property.IsNullable;

        public string Name => this.m_Property.Name;

        public PropertyInfo PropertyInfo => this.m_Property.PropertyInfo;

        public ValueGenerated ValueGenerated => this.m_Property.ValueGenerated;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public IAnnotation FindAnnotation(string name)
            => this.m_Property.FindAnnotation(name);

        public IEnumerable<IAnnotation> GetAnnotations()
            => this.m_Property.GetAnnotations();

        #endregion Methods

        #region - - - - - - Operators - - - - - -

        // TODO: Migrate the decorator behaviour so it applies to FindAnnotation and GetAnnotations
        public object this[string name]
        {
            get
            {
                var _Value = this.m_Property[name];
                if (_Value is RelationalTypeMapping _RelationalTypeMapping && this.FindAnnotation("StaticEntity.TypeMapping") != null) // TODO: Hard-coded string.
                    _Value = this.m_RelationalTypeMappingDecoratorFactory(_RelationalTypeMapping);

                return _Value;
            }
        }

        #endregion Operators

    }

}
