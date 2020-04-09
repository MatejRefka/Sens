using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens.Utility {

    /// <summary>
    /// wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
    /// </summary>

    public class NewOldIDWrapper {

        public int OldID { get; set; }
        public int NewID { get; set; }
    }
}
