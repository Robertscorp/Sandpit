//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Internal;
//using System;
//using System.Diagnostics.CodeAnalysis;

//namespace Sandpit.SemiStaticEntity
//{

//    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
//    public class DbContextServicesReplacement : DbContextServices
//    {

//        #region - - - - - - Methods - - - - - -

//        public override IDbContextServices Initialize(
//            IServiceProvider scopedProvider,
//            IDbContextOptions contextOptions,
//            DbContext context)
//            => base.Initialize(new ServiceProviderDecorator(context, scopedProvider), contextOptions, context);

//        #endregion Methods

//    }

//}
