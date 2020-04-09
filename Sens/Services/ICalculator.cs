namespace Sens.Services {

    /// <summary>
    /// ICalculator interface with all the methods necessary for the first 3D to 3D converter.
    /// Interface that will be utilised in several ViewModels/classes, following the dependency
    /// inversion principle.
    /// </summary>
    public interface ICalculator {

        //(1) LOW LEVEL- Given a sens, find cm/360 for game x (non-yaw, default 400 DPI)
        double SmiteFunc(string sensFrom);
        double PaladinsFunc(string sensFrom);
        double WarframeFunc(string sensFrom);
        double MinecraftFunc(string sensFrom);
        double OuterWorldsFunc(string sensFrom);
        double RustFunc(string sensFrom);

        //(2) Given a game + sens + DPI, find cm/360 (yaw + non-yaw (1))
        string CalcCM(string gameFrom, string sensFrom, string dpiFrom);

        //(3) Convert cm/360(2) to in/360
        string CalcIN(string gameFrom, string sensFrom, string dpiFrom);

        //(4) PREP- Given a game + sens, return cm/360 with default DPI (yaw and non-yaw) 
        string CalcCMFixedDPI(string gameFrom, string sensFrom);

        //(5) LOW LEVEL - Return new sens for game x (yaw and non-yaw, default 400 DPI)
        double SourceCalc(string gameFrom, string sensFrom);
        double FortniteCalc(string gameFrom, string sensFrom);
        double OverwatchCalc(string gameFrom, string sensFrom);
        double QuakeCalc(string gameFrom, string sensFrom);
        double SmiteCalc(string gameFrom, string sensFrom);
        double PaladinsCalc(string gameFrom, string sensFrom);
        double WarframeCalc(string gameFrom, string sensFrom);
        double MinecraftCalc(string gameFrom, string sensFrom);
        double OuterWorldsCalc(string gameFrom, string sensFrom);
        double RustCalc(string gameFrom, string sensFrom);

        //(6) Given gameFrom + gameTo + sens, return new sensitivity (yaw and non-yaw)
        string NewSens(string gameTo, string gameFrom, string sensFrom);
        
    }
}