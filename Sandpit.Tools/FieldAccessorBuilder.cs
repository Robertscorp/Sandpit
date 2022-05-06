using System;
using System.Linq.Expressions;

namespace Sandpit.Tools
{

    public static class FieldAccessor
    {

        #region - - - - - - Methods - - - - - -

        public static Func<TInstance, TField> Get<TInstance, TField>(string fieldName)
        {
            var _Param = Expression.Parameter(typeof(TInstance));
            return Expression.Lambda<Func<TInstance, TField>>(Expression.Field(_Param, fieldName), _Param).Compile();
        }

        #endregion Methods

    }

}
