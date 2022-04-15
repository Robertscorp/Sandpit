//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using System;

//namespace Sandpit.SemiStaticEntity
//{

//    public class ServiceProviderDecorator : IServiceProvider
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly DbContext m_Context; // TODO: Is this used?
//        private readonly IServiceProvider m_ServiceProvider;

//        #endregion Fields

//        #region - - - - - - Constructors - - - - - -

//        public ServiceProviderDecorator(DbContext context, IServiceProvider serviceProvider)
//        {
//            this.m_Context = context ?? throw new ArgumentNullException(nameof(context));
//            this.m_ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
//        }

//        #endregion Constructors

//        #region - - - - - - Methods - - - - - -

//        object IServiceProvider.GetService(Type serviceType)
//            => serviceType == typeof(IModelCustomizer)
//                ? default //new ModelCustomiserDecorator((IModelCustomizer)this.m_ServiceProvider.GetService(serviceType))
//                : this.m_ServiceProvider.GetService(serviceType);

//        #endregion Methods

//    }

//}
