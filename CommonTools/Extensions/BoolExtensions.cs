using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Extensions
{
    public static class BoolExtensions
    {
        public static int ToOneZeroInteger(this bool value) => value ? 1 : 0;

        public static string ToYesNoString(this bool value) => value ? "Yes" : "No";
    }
}
