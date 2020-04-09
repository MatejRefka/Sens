using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens.DataModels {

    /// <summary>
    /// Profile Model acting as an interface for the DB.
    /// </summary>

    public class ProfileModel {

        //first set of fields (textboxes) in Custom Profile form
        public int ID { get; set; }

        public String ProfileName { get; set; }

        public String BackgroundURL { get; set; }

        public String ButtonMacro { get; set; }

        public String SensitivityFrom { get; set; }

        public String Sensitivity { get; set; }

        public String DPI { get; set; }

        //second set of fields (Apply to these games:) int because bool is not supported (0=false,1=true)
        public int Apex { get; set; }
        public int CSGO { get; set; }
        public int CS16 { get; set; }
        public int CSS { get; set; }
        public int GMod { get; set; }
        public int HL2 { get; set; }
        public int L4D2 { get; set; }
        public int Minecraft { get; set; }
        public int Paladins { get; set; }
        public int Quake { get; set; }
        public int Rust { get; set; }
        public int Smite { get; set; }
        public int TF2 { get; set; }
        public int Portal2 { get; set; }

        /*int variable (0=false, 1=true) which controls which Profile is currently being in context. Based on 
        user input/subsequent fucntionality, it flags one Profile (record in the DB) which is read into 
        the Custom Profile form or then updated based on user input in the CustomProfile form.
        At any given point there is only one record where current =1 or zero if a new entry is being added.
        */
        public int current { get; set; }
    }
}
