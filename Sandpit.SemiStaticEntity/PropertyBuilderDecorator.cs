//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Sandpit.SemiStaticEntity
//{

//    public class PropertyBuilderDecorator<TProperty> : PropertyBuilder<TProperty>
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly IMutableProperty m_Metadata;
//        private readonly PropertyBuilder<TProperty> m_PropertyBuilder;

//        #endregion Fields

//        #region - - - - - - Constructors - - - - - -

//        public PropertyBuilderDecorator(PropertyBuilder<TProperty> propertyBuilder) : base(propertyBuilder.Metadata)
//        {
//            this.m_Metadata = new MutablePropertyDecorator(propertyBuilder.Metadata);
//            this.m_PropertyBuilder = propertyBuilder;
//        }

//        #endregion Constructors

//        #region - - - - - - Properties - - - - - -

//        public override IMutableProperty Metadata => this.m_Metadata;

//        #endregion Properties

//        #region - - - - - - Methods - - - - - -

//        #endregion Methods

//    }

//}
