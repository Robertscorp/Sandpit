//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace Sandpit.SemiStaticEntity
//{

//    public class MutablePropertyDecorator : IMutableProperty
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly IMutableProperty m_MutableProperty;

//        #endregion Fields

//        #region - - - - - - Constructors - - - - - -

//        public MutablePropertyDecorator(IMutableProperty mutableProperty)
//            => this.m_MutableProperty = mutableProperty;

//        #endregion Constructors

//        #region - - - - - - Properties - - - - - -

//        public Type ClrType => this.m_MutableProperty.ClrType;

//        public IMutableEntityType DeclaringEntityType => this.m_MutableProperty.DeclaringEntityType;

//        public IMutableTypeBase DeclaringType => this.m_MutableProperty.DeclaringType;

//        public FieldInfo FieldInfo { get => this.m_MutableProperty.FieldInfo; set => this.m_MutableProperty.FieldInfo = value; }

//        public bool IsConcurrencyToken { get => this.m_MutableProperty.IsConcurrencyToken; set => this.m_MutableProperty.IsConcurrencyToken = value; }

//        bool IProperty.IsConcurrencyToken => ((IProperty)this.m_MutableProperty).IsConcurrencyToken;

//        IEntityType IProperty.DeclaringEntityType => ((IProperty)this.m_MutableProperty).DeclaringEntityType;

//        ValueGenerated IProperty.ValueGenerated => ((IProperty)this.m_MutableProperty).ValueGenerated;

//        bool IProperty.IsNullable => ((IProperty)this.m_MutableProperty).IsNullable;

//        ITypeBase IPropertyBase.DeclaringType => ((IPropertyBase)this.m_MutableProperty).DeclaringType;

//        FieldInfo IPropertyBase.FieldInfo => ((IPropertyBase)this.m_MutableProperty).FieldInfo;

//        public bool IsNullable { get => this.m_MutableProperty.IsNullable; set => this.m_MutableProperty.IsNullable = value; }

//        public string Name => this.m_MutableProperty.Name;

//        public PropertyInfo PropertyInfo => this.m_MutableProperty.PropertyInfo;

//        public ValueGenerated ValueGenerated { get => this.m_MutableProperty.ValueGenerated; set => this.m_MutableProperty.ValueGenerated = value; }

//        #endregion Properties

//        #region - - - - - - Methods - - - - - -

//        public IAnnotation AddAnnotation(string name, object value) => this.m_MutableProperty.AddAnnotation(name, value);

//        public IAnnotation FindAnnotation(string name) => this.m_MutableProperty.FindAnnotation(name);

//        public IEnumerable<IAnnotation> GetAnnotations() => this.m_MutableProperty.GetAnnotations();

//        public IAnnotation RemoveAnnotation(string name) => this.m_MutableProperty.RemoveAnnotation(name);

//        public void SetAnnotation(string name, object value) => this.m_MutableProperty.SetAnnotation(name, value);

//        #endregion Methods

//        #region - - - - - - Operators - - - - - -

//        object IAnnotatable.this[string name] => ((IAnnotatable)this.m_MutableProperty)[name];

//        public object this[string name] { get => this.m_MutableProperty[name]; set => this.m_MutableProperty[name] = value; }

//        #endregion Operators

//    }

//}
