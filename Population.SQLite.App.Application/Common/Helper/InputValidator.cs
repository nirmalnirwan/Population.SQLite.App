using System.Linq;

namespace Population.SQLite.App.Application.Common.Helper
{
    public static  class InputValidator
    {
        public static int[] QueryParamValidForIntArray(this string querystring)
        {
            string[] queryStrSplit = !string.IsNullOrEmpty(querystring) ? querystring.Split(',') : null;
            if (queryStrSplit == null)
            {
                return null;
            }
            foreach (var item in queryStrSplit)
            {
                if (!int.TryParse(item, out int i))
                {
                    return null;
                }
            }
            return !string.IsNullOrEmpty(querystring) ? querystring.Split(',').Select(int.Parse).ToArray() : null ;
        }
    }
}
