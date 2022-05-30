using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Sandpit.SemiStaticEntity.Extensions
{

    public static class IEntityTypeExtensions
    {

        #region - - - - - - Methods - - - - - -

        internal static Func<TEntity, object[]> GetPrimaryKeyValuesFunc<TEntity>(
            this IEntityType entityType)
        {
            var _Parameter = Expression.Parameter(typeof(TEntity));
            var _CreateArrayExpression
                = Expression.NewArrayInit(
                    typeof(object),
                    entityType.FindPrimaryKey()
                        .Properties.Select(p => Expression.Convert(Expression.Property(_Parameter, p.PropertyInfo), typeof(object))));

            return Expression.Lambda<Func<TEntity, object[]>>(_CreateArrayExpression, _Parameter).Compile();
        }

        public static bool IsStaticEntity(this IEntityType entityType)
            => entityType?.FindAnnotation("StaticEntity.IsStaticEntity") != null; // TODO: Hard-coded string

        #endregion Methods

    }

}
