namespace Sens.Services {

    /// <summary>
    /// Interface with all the methods necessary to overwrite the sensitivity property for
    /// different games' config files.
    /// Interface that will be utilised in several ViewModels/classes, following the dependency
    /// inversion principle.
    /// </summary>
    public interface IWriteToCFG {

        //Determines which games are flagged to be used for cfg
        double CalcCM3D(int profileNumber);

        //Individual game (different paths) 
        void WriteToApex(string sens);
        void WriteToCS16(string sens);
        void WriteToCSGO(string sens);
        void WriteToCSS(string sens);
        void WriteToGMOD(string sens);
        void WriteToHL2(string sens);
        void WriteToL4D2(string sens);
        void WriteToMinecraft(string sens);
        void WriteToPaladins(string sens);
        void WriteToPortal2(string sens);
        void WriteToQuake(string sens);
        void WriteToRust(string sens);
        void WriteToSmite(string sens);
        void WriteToTF2(string sens);
    }
}