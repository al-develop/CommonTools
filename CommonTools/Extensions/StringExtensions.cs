using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonTools.Extensions
{
    public static class StringExtensions
    {
        public static bool Like(this string input, string pattern)
        {
            if (input != null)
            {
                pattern = Regex.Escape("%" + pattern + "%");
                pattern = pattern.Replace("%", ".*?").Replace("_", ".");
                pattern = pattern.Replace(@"\[", "[").Replace(@"\]", "]").Replace(@"\^", "^");

                return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
            }
            else
            {
                return false;
            }
        }

        public static string Replace(this string value, string whatToReplace, string replaceItWith, StringComparison comparison)
        {
            var options = RegexOptions.None;

            switch (comparison)
            {
                case StringComparison.CurrentCultureIgnoreCase:
                    options = RegexOptions.IgnoreCase;
                    break;
                case StringComparison.Ordinal:
                case StringComparison.InvariantCulture:
                    options = RegexOptions.CultureInvariant;
                    break;
                case StringComparison.OrdinalIgnoreCase:
                case StringComparison.InvariantCultureIgnoreCase:
                    options = RegexOptions.CultureInvariant | RegexOptions.IgnoreCase;
                    break;
            }

            var regex = new Regex(whatToReplace, options);
            return regex.Replace(value, replaceItWith ?? string.Empty);
        }

        public static string Substitute(this string value, string substitute = "-")
        {
            if (string.IsNullOrWhiteSpace(value))
                return substitute;

            return value;
        }

        public static string Shorten(this string value, int length = 50)
        {
            if (value == null)
                return null;

            if (value.Length <= length)
                return value;

            value = value.Substring(0, length - 1);
            return value + "…";
        }

        public static string MakeOneLiner(this string value)
        {
            if (value == null)
                return value;

            string result = value;

            result = result.Replace(Environment.NewLine, " ");
            result = result.Replace("\r", " ");
            result = result.Replace("\n", " ");

            return result;
        }

        public static string RemoveLastNewLine(this string value)
        {
            if (value.EndsWith(Environment.NewLine))
                return value.Remove(value.Length - Environment.NewLine.Length);

            return value;
        }

        public static string AppendLine(this string value, string line)
        {
            if (value == null)
                value = string.Empty;

            if (string.IsNullOrWhiteSpace(value) == false)
                value += Environment.NewLine;

            return value += line;
        }
    }
}
