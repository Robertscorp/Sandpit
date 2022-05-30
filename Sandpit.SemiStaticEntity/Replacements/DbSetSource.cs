using Microsoft.EntityFrameworkCore;
using Sandpit.SemiStaticEntity.Extensions;
using Sandpit.SemiStaticEntity.Internal;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sandpit.SemiStaticEntity.Replacements
{

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class DbSetSource : Microsoft.EntityFrameworkCore.Internal.DbSetSource
    {

        #region - - - - - - Methods - - - - - -

        public override object Create(DbContext context, Type type)
            => context.Model.FindEntityType(type).IsStaticEntity()
                ? Activator.CreateInstance(typeof(StaticEntitySet<>).MakeGenericType(type), context)
                : base.Create(context, type);

        #endregion Methods

    }

}
