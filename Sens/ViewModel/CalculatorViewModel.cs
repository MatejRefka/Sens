using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sens.DataModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using Sens.Services;

namespace Sens.ViewModel {

    /// <summary>
    /// ViewModel for CalculatorPage View. Provides functionality for UI design, alongside user input 
    /// validation. Provides all the calculation for the conversions. The calculation functionality for
    /// both 3D and 2D calculators is provided by ICalculator and ICalculator2D interfaces.
    /// </summary>

    public class CalculatorViewModel : BaseViewModel {

        #region Fields
        /*FIELDS*/

        //calculator interfaces (dependency inversion)
        private ICalculator calculator;
        private ICalculator2D calculator2D;

        //fields corresponding to Calculator TextBoxes. double values as string because of a comparison error when deleting from TextBox, converts to double later
        private string gameFrom3D="CS:GO";
        private string sensFrom3D;
        private string dpiFrom3D;

        private string gameTo3D="Apex Legends";
        private string sensTo3D;
        private string in360;
        private string cm360;

        private string gameFrom3Dsecond="CS:GO";
        private string sensFrom3Dsecond;
        private string dpiFrom3Dsecond;

        private string gameTo2Dsecond="Desktop";
        private string dpiTo2Dsecond;
        private string in2D;
        private string cm2D;

        //Match digits or dot for sens. regex101.com
        private static readonly Regex regexSens = new Regex("[^0-9.{1}]");

        //Match digits or . for DPI. regex101.com
        private static readonly Regex regexDPI = new Regex("[^0-9]");

        #endregion Fields

        #region Properties
        /*PROPERTIES*/

        public List<String> GamesCollection { get; set; } = new List<string>
        {
            "Apex Legends",
            "CS:GO",
            "CS 1.6",
            "CS Source",
            "Fortnite",
            "Garry's Mod",
            "Half Life 2",
            "Left 4 Dead 2",
            "Minecraft",
            "Overwatch",
            "Paladins",
            "Portal 2",
            "Quake Live",
            "Rust",
            "Smite",
            "Team Fortress 2",
            "The Outer Worlds",
            "Warframe"
        };

        public List<String> Games2DCollection { get; set; } = new List<String>
        {
            "Desktop",
            "osu!"
        };


        /*FROM 3D -properties binding text within TextBoxes*/
        public string GameFrom3D {
            get {
                return gameFrom3D;
            }
            set {
                gameFrom3D = value;
                OnPropertyChanged("GameFrom3D");
            }
        }
        public string SensitivityFrom3D {
            get {
                return sensFrom3D;
            }
            set {
                sensFrom3D = value;
                OnPropertyChanged("SensitivityFrom3D");
            }
        }
        public string DPIFrom3D {
            get {
                return dpiFrom3D;
            }
            set {
                dpiFrom3D = value;
                OnPropertyChanged("DPIFrom3D");
            }
        }

        /*TO 3D -properties binding text within TextBoxes*/
        public string GameTo3D {
            get {
                return gameTo3D;
            }
            set {
                gameTo3D = value;
                OnPropertyChanged("GameTo3D");
            }
        }

        public string SensitivityTo3D {
            get {
                return sensTo3D = calculator.NewSens(GameTo3D, GameFrom3D, SensitivityFrom3D);
            }
            set {
                sensTo3D = value;
                OnPropertyChanged("SensitivityFrom3D");
                OnPropertyChanged("GameFrom3D");
                OnPropertyChanged("GameTo3D");
            }
        }

        public string CM360 {
            get {
                return cm360 = calculator.CalcCM(gameFrom3D, SensitivityFrom3D, dpiFrom3D);
            }
            set {
                cm360 = value;
                OnPropertyChanged("GameFrom3D");
                OnPropertyChanged("SensitivityFrom3D");
                OnPropertyChanged("DPIFrom3D");
            }
        }

        public string IN360 {
            get {
                return in360 = calculator.CalcIN(gameFrom3D, SensitivityFrom3D, dpiFrom3D);
            }
            set {
                in360 = value;
                OnPropertyChanged("GameFrom3D");
                OnPropertyChanged("SensitivityFrom3D");
                OnPropertyChanged("DPIFrom3D");
            }
        }

        /*FROM 3D SECOND -properties binding text within TextBoxes*/
        public string GameFrom3Dsecond {
            get {
                return gameFrom3Dsecond;
            }
            set {
                gameFrom3Dsecond = value;
                OnPropertyChanged("GameFrom3Dsecond");
            }
        }

        public string SensitivityFrom3Dsecond {
            get {
                return sensFrom3Dsecond;
            }
            set {
                sensFrom3Dsecond = value;
                OnPropertyChanged("SensitivityFrom3Dsecond");
            }
        }

        public string DPIFrom3Dsecond {
            get {
                return dpiFrom3Dsecond;
            }
            set {
                dpiFrom3Dsecond = value;
                OnPropertyChanged("DPIFrom3Dsecond");
            }
        }

        /*TO 2D SECOND -properties binding text within TextBoxes*/
        public string GameTo2Dsecond {
            get {
                return gameTo2Dsecond;
            }
            set {
                gameTo2Dsecond = value;
                OnPropertyChanged("GameTo2Dsecond");
            }
        }
        public string DPITo2Dsecond {
            get {
                return dpiTo2Dsecond = calculator2D.NewDPI(gameFrom3Dsecond, sensFrom3Dsecond, dpiFrom3Dsecond);
            }
            set {
                dpiTo2Dsecond = value;
                OnPropertyChanged("GameFrom3Dsecond");
                OnPropertyChanged("SensitivityFrom3Dsecond");
                OnPropertyChanged("DPIFrom3Dsecond");
            }
        }
        public string IN2D {
            get {
                return in2D = calculator2D.CalcIN2D(gameFrom3Dsecond, sensFrom3Dsecond, dpiFrom3Dsecond);
            }
            set {
                in2D = value;
                OnPropertyChanged("GameFrom3Dsecond");
                OnPropertyChanged("SensitivityFrom3Dsecond");
                OnPropertyChanged("DPIFrom3Dsecond");
            }
        }
        public string CM2D {
            get {
                return cm2D = calculator2D.CalcCM2D(gameFrom3Dsecond, sensFrom3Dsecond, dpiFrom3Dsecond);
            }
            set {
                cm2D = value;
                OnPropertyChanged("GameFrom3Dsecond");
                OnPropertyChanged("SensitivityFrom3Dsecond");
                OnPropertyChanged("DPIFrom3Dsecond");
            }
        }

        #endregion Properties

        #region Constructor

        /*CONSTRUCTOR*/
        public CalculatorViewModel(ICalculator calculator, ICalculator2D calculator2D) {

            this.calculator = calculator;
            this.calculator2D = calculator2D;

        }
        #endregion Constructor

        #region Regex Validation

        //only allow numbers and one decimal dot in the TextBox
        public void NumberValidationSens(object sender, TextCompositionEventArgs e) {
            e.Handled = regexSens.IsMatch(e.Text);
        }

        public void NumberValidationDPI(object sender, TextCompositionEventArgs e) {
            e.Handled = regexDPI.IsMatch(e.Text);
        }
        #endregion Validation

    }

}

