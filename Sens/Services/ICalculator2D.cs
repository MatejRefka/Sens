namespace Sens.Services {

    /// <summary>
    /// ICalculator2D interface with all the methods necessary for the second 3D to 2D converter.
    /// Interface that will be utilised in several ViewModels/classes, following the dependency
    /// inversion principle.
    /// </summary>
    public interface ICalculator2D {

        //(2) PREP- Given a game + sens, convert to and return a source sens (yaw + non-yaw)
        double ConvertToSourceSens(string gameFrom, string sensFrom);

        //(3) Given the source sens + dpi, return cm2D 
        string CalcCM2D(string gameFrom, string sensFrom, string dpiFrom);

        //(4) Given the source sens + dpi, return in2D
        string CalcIN2D(string gameFrom, string sensFrom, string dpiFrom);

        //(1) Given cm2D, return new DPI
        string NewDPI(string gameFrom, string sensFrom, string dpiFrom);

    }
}