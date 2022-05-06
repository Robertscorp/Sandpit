using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sandpit.SemiStaticEntity.Extensions;
using Sandpit.SemiStaticEntity.Internal;
using Sandpit.SemiStaticEntity.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sandpit.SemiStaticEntity.Replacements
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class DbSetSource : Microsoft.EntityFrameworkCore.Internal.DbSetSource
    {

        #region - - - - - - Fields - - - - - -

        private static readonly Func<EntityType, List<object>> s_EntityTypeData
            = FieldAccessor.Get<EntityType, List<object>>("_data");

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override object Create(DbContext context, Type type)
        {
            var _EntityType = context.Model.FindEntityType(type);
            if (!_EntityType.IsStaticEntity())
                return base.Create(context, type);

            var _Data = s_EntityTypeData((EntityType)_EntityType);

            context.AttachRange(_Data);

            return Activator.CreateInstance(typeof(StaticEntitySet<>).MakeGenericType(type), _Data);
        }

        #endregion Methods

    }

}
