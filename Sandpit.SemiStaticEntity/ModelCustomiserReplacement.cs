//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Storage;
//using System.Linq;
//using System.Reflection;

//namespace Sandpit.SemiStaticEntity
//{

//    public class ModelCustomiserReplacement : IModelCustomizer
//    {

//        #region - - - - - - Methods - - - - - -

//        public void Customize(ModelBuilder modelBuilder, DbContext context)
//        {
//            _ = context.GetType()
//                           .GetMethod("OnModelCreating", BindingFlags.NonPublic | BindingFlags.Instance)
//                           .Invoke(context, new object[] { new ModelBuilderDecorator(modelBuilder) });

//            _ = modelBuilder.FinalizeModel();

//            //var _Metadata = modelBuilder.Entity<TEntity>().Metadata;

//            foreach (var _Metadata in modelBuilder.Model.GetEntityTypes())
//                foreach (var _Property in _Metadata.GetProperties().Where(p => p.FindAnnotation("TypeMapping2") != null).ToList())
//                {
//                    var _RelationalTypeMapping = (RelationalTypeMapping)_Property["TypeMapping"];
//                    _RelationalTypeMapping = new RelationalTypeMappingDecorator(_RelationalTypeMapping);

//                    _Property.SetAnnotation("TypeMapping", _RelationalTypeMapping);

//                    xxx // None of this decoration behaviour works, because Microsoft was pretty good at locking everything up.
//                        // However, the weakness is the shaper.
//                        // Create a visitor that can visit the shaper and decorate any IProperty constants / params / whatever it comes across.
//                        // This way it can be decorated in the 1 place that makes the difference - in the query execution.
//                        // The shaper is the weakness in EFCore's defence that can be exploited :)

//                    var _XX = 0;
//                    //_Metadata.RemoveProperty(_Property);
//                    //_Metadata.AddProperty()

//                }


//        }

//        #endregion Methods

//    }

//}
