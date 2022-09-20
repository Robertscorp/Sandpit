using System.Linq;

namespace Sandpit.ConcatenateQueryables
{

    public static class IQueryableExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static IQueryable<T> Concatenate<T>(this IQueryable<T> source, IQueryable<T> concatenate)
            => new ConcatenatedQueryable<T>(source, concatenate);

        #endregion Methods

    }

}
