using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens.DataModels
{
    /// <summary>
    /// Model data structure for pages and navigation. The enum is later converted into a 
    /// page in ApplicationPageValueConverter.
    /// </summary>

    public enum ApplicationPage
    {
        Home=0,
        Calculator=1,
        MyProfiles=2,
        Tutorials=3,
        Settings=4,
        Feedback=5,

    }
}
