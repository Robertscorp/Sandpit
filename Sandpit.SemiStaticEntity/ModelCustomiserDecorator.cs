//namespace Sandpit.SemiStaticEntity
//{

//    public class ModelCustomiserDecorator : IModelCustomizer
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly IModelCustomizer m_ModelCustomiser;

//        #endregion Fields

//        #region - - - - - - Constructors - - - - - -

//        public ModelCustomiserDecorator(IModelCustomizer modelCustomiser)
//            => this.m_ModelCustomiser = modelCustomiser;

//        #endregion Constructors

//        #region - - - - - - Methods - - - - - -

//        public void Customize(ModelBuilder modelBuilder, DbContext context)
//            => this.m_ModelCustomiser.Customize(new ModelBuilderDecorator(modelBuilder), context);

//        #endregion Methods

//    }

//}
