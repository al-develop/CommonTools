using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Utils
{
    /// <summary>
    /// Class to load Neutral Cultures and to distinct them
    /// </summary>
    public class CultureHandler
    {
        /// <summary>
        /// Loads all NeutralCultures
        /// </summary>
        /// <returns>A List of CultureInfo</returns>
        public static IList<CultureInfo> GetCultures() => CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList();

        /// <summary>
        /// Distincts a List of CultureInfo
        /// </summary>
        /// <returns>A List of CultureInfo</returns>
        public static IList<CultureInfo> GetDistinctedCultures()
        {
            IList<CultureInfo> cultures = GetCultures();
            return cultures.GroupBy(d => d.NativeName)
                           .Select(g => g.First())
                           .ToList();
        }


        /// <summary>
        /// Loads a List of System.String, containing NativeNames of all CultureInfos in lower case
        /// </summary>
        /// <returns>A List of System.String, containing NativeNames of all CultureInfos in lower case</returns>
        public static IList<string> GetCulturesAsLowerCaseStringCollectionByFilter()
        {
            IList<CultureInfo> cultures = GetCultures();
            IList<string> result = new List<string>();
            foreach (var culture in cultures)
            {
                result.Add(culture.NativeName.ToLower());
            }

            return result;
        }
    }
}
