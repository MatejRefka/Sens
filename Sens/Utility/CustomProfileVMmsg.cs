using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens.Utility {

    /// <summary>
    /// An object used to send a Message between two ViewModels (on 'Save' button click in CustomProfile,
    /// generate a new Profile in MyProfilesPage updating Visibility, ProfileName and ProfileBackground),
    /// handled by a Messenger -GalaSoft.MvvmLight.Messaging
    /// </summary>

    public class CustomProfileVMmsg {

        public string Message { get; set; }
        public string ProfileName { get; set; }
        public string ProfileBackground { get; set; }
        public string ButtonMacro { get; set; }

    }
}
