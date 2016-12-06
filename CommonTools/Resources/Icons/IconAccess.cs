using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Resources.Icons
{
    public sealed class IconAccess
    {
        // TODO: Whenever a new Icon is added, add a public Uri which returns a string, containing a pack://application,,,/ path to the new Icon

        // Exammple for a Uri which returns a string with a pack://application:,,,/ for an Icon
        public Uri ExampleIcon => new Uri("pack://application:,,,/Resources/Icons/");
    }
}