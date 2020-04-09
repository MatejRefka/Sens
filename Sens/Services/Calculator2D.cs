using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens.Services {
   
    /// <summary>
    /// Calculator class implementing ICalculator2D. The class provides functionality for the 
    /// second 3D to 2D sensitivity converter.
    /// </summary>
    public class Calculator2D : ICalculator2D {

        //fixed helper fields
        private readonly double cmSource = 77.8; //cm @ 100dpi, 1.5 sens
        private readonly double inCmRatio = 2.54;

        private double[] minecraftSenses = {
            0.49, 0.51, 0.53, 0.56, 0.58, 0.6, 0.63, 0.66, 0.69, 0.72, 0.75, 0.77, 0.8, 0.83, 0.87, 0.89, 0.92, 0.95, 0.99, 1.02, 1.05, 1.09, 1.12, 1.16, 1.2,
            1.23, 1.27, 1.31, 1.34, 1.38, 1.42, 1.46, 1.51, 1.55, 1.6, 1.64, 1.69, 1.74, 1.79, 1.84, 1.89, 1.95, 2.01, 2.06, 2.12, 2.18, 2.25, 2.31, 2.38, 2.44,
            2.5, 2.56, 2.62, 2.68, 2.75, 2.82, 2.88, 2.95, 3.02, 3.09, 3.17, 3.25, 3.32, 3.4, 3.48, 3.57, 3.65, 3.74, 3.83, 3.92, 4.02, 4.11, 4.21, 4.31, 4.43,
            4.51, 4.6, 4.69, 4.78, 4.87, 4.97, 5.06, 5.16, 5.26, 5.37, 5.47, 5.58, 5.68, 5.8, 5.91, 6.02, 6.14, 6.26, 6.38, 6.51, 6.64, 6.76, 6.9, 7.03, 7.2,
            7.31, 7.44, 7.56, 7.69, 7.81, 7.94, 8.08, 8.21, 8.35, 8.49, 8.63, 8.78, 8.92, 9.07, 9.22, 9.38, 9.53, 9.69, 9.85, 10.01, 10.18, 10.35, 10.53, 10.7, 10.85,
            11.01, 11.16, 11.32, 11.49, 11.66, 11.82, 12, 12.17, 12.34, 12.52, 12.7, 12.89, 13.09, 13.27, 13.46, 13.65, 13.85, 14.06, 14.25, 14.47, 14.68, 14.89, 15.1, 15.33, 15.63,
            15.84, 16.06, 16.26, 16.49, 16.71, 16.92, 17.18, 17.41, 17.64, 17.85, 18.1, 18.36, 18.59, 18.86, 19.1, 19.35, 19.61, 19.87, 20.14, 20.41, 20.7, 20.99, 21.25, 21.56, 21.83,
            22.06, 22.3, 22.49, 22.74, 22.99, 23.25, 23.46, 23.72, 23.94, 24.22, 24.45, 24.74, 24.98, 25.28, 25.53, 25.78, 26.04, 26.31, 26.58, 26.92, 27.2, 27.49, 27.71, 28.01, 28.31
            }; //1-200

        private double[] paladinsSenses = {
            0.41, 0.62, 0.93, 1.4, 2.1, 2.41, 2.78, 3.21, 3.7, 4.25, 4.61, 5, 5.43, 5.89, 6.41, 6.75, 7.11, 7.48, 7.88, 8.29, 8.72, 9.19, 9.67, 10.18, 10.7,
            11.01, 11.32, 11.64, 11.97, 12.31, 12.67, 13.02, 13.39, 13.78, 14.18, 14.57, 14.99, 15.42, 15.86, 16.31, 16.76, 17.26, 17.73, 18.26, 18.76, 19.31, 19.87, 20.41, 20.99, 21.65,
            22.01, 22.35, 22.64, 22.99, 23.35, 23.72, 24.05, 24.45, 24.8, 25.16, 25.53, 25.91, 26.31, 26.71, 27.13, 27.56, 27.93, 28.39, 28.78, 29.27, 29.69, 30.12, 30.56, 31.02, 31.39,
            31.87, 32.27, 32.78, 33.3, 33.74, 34.18, 34.75, 35.22, 35.71, 36.21, 36.72, 37.38, 37.79, 38.34, 38.92, 39.51, 40.12, 40.75, 41.23, 41.9, 42.41, 43.12, 43.66, 44.41, 44.98
        }; //1-100

        private double[] smiteSenses = {
            0.31, 0.47, 0.72, 1.09, 1.67, 1.92, 2.21, 2.54, 2.93, 3.37, 3.65, 3.96, 4.28, 4.64, 5.02, 5.34, 5.68, 6.05, 6.44, 6.85, 7.29, 7.76, 8.26, 8.79, 9.37,
            9.59, 9.83, 10.07, 10.31, 10.56, 10.82, 11.08, 11.36, 11.62, 11.9, 12.2, 12.49, 12.8, 13.1, 13.42, 13.74, 14.08, 14.43, 14.78, 15.15, 15.51, 15.89, 16.26, 16.65, 17.03,
            17.29, 17.52, 17.76, 18.01, 18.26, 18.52, 18.79, 19.07, 19.31, 19.61, 19.87, 20.14, 20.45, 20.74, 21.03, 21.29, 21.6, 21.92, 22.25, 22.54, 22.84, 23.19, 23.51, 23.83, 24.11,
            24.39, 24.68, 24.98, 25.22, 25.53, 25.85, 26.17, 26.44, 26.78, 27.06, 27.42, 27.71, 28.08, 28.39, 28.7, 29.11, 29.44, 29.77, 30.12, 30.47, 30.83, 31.2, 31.58, 31.97, 32.37
        }; //1-100

        private double[] outerWorldsSenses = {
            0.4, 0.65, 0.89, 1.16, 1.37, 1.56, 1.79, 2.04, 2.32, 2.65, 2.82, 3, 3.19, 3.39, 3.61, 3.99, 4.42, 4.89, 5.4, 5.98, 6.61, 7.32, 8.09, 8.95, 9.9,
            10.01, 10.13, 10.26, 10.38, 10.5, 10.62, 10.76, 10.88, 11.01, 11.14, 11.27, 11.41, 11.55, 11.69, 11.82, 11.96, 12.11, 12.25, 12.4, 12.55, 12.7, 12.84, 13, 13.15, 13.3,
            13.51, 13.71, 13.93, 14.14, 14.37, 14.59, 14.82, 15.04, 15.28, 15.51, 15.77, 16.01, 16.26, 16.49, 16.76, 17.01, 17.29, 17.55, 17.82, 18.1, 18.39, 18.66, 18.96, 19.24, 19.61,
            19.87, 20.1, 20.33, 20.62, 20.87, 21.12, 21.38, 21.65, 21.92, 22.16, 22.44, 22.74, 22.99, 23.3, 23.56, 23.89, 24.16, 24.45, 24.74, 25.1, 25.41, 25.72, 26.04, 26.31, 26.64
        }; //1-100

        private double[] warframeSenses = {
            1.9, 2.21, 2.56, 2.97, 3.44, 3.74, 4.07, 4.43, 4.82, 5.25, 5.56, 5.88, 6.23, 6.59, 6.98, 7.27, 7.57, 7.89, 8.22, 8.57, 8.92, 9.29, 9.68, 10.09, 10.49,
            10.73, 11, 11.25, 11.52, 11.79, 12.07, 12.36, 12.66, 12.96, 13.25, 13.58, 13.89, 14.23, 14.57, 14.91, 15.26, 15.63, 16.01, 16.39, 16.76, 17.18, 17.58, 17.98, 18.42, 18.93,
            19.21, 19.5, 19.75, 20.06, 20.37, 20.66, 20.99, 21.29, 21.6, 21.92, 22.25, 22.59, 22.94, 23.25, 23.62, 24, 24.33, 24.68, 25.04, 25.41, 25.78, 26.17, 26.58, 26.99, 27.42,
            27.71, 28.01, 28.31, 28.63, 28.94, 29.27, 29.6, 29.94, 30.29, 30.56, 30.93, 31.3, 31.58, 31.97, 32.27, 32.68, 32.99, 33.41, 33.74, 34.07, 34.52, 34.87, 35.22, 35.59, 35.95
        }; //1-100

        #region (1) PREP- Given a game + sens, convert to and return a source sens (yaw + non-yaw)
        public double ConvertToSourceSens(string gameFrom, string sensFrom) {

            double sens;

            try {
                sens = Convert.ToDouble(sensFrom);
            }
            catch (FormatException) {
                return -1;
            }

            if (gameFrom == "Apex Legends" || gameFrom == "CS:GO" || gameFrom == "CS Source" || gameFrom == "CS 1.6" || gameFrom == "Garry's Mod" || gameFrom == "Half Life 2" || gameFrom == "Left 4 Dead 2" || gameFrom == "Team Fortress 2" || gameFrom == "Portal 2") {
                return sens;
            }

            else if (gameFrom == "Overwatch") {
                //fixed yaw => fixed multiplier
                return sens / (10 / 3);
            }

            else if (gameFrom == "Fortnite") {
                //fixed yaw => fixed multiplier
                return sens / (0.03956);
            }

            else if (gameFrom == "Rust") {
                if (sens == 2) {
                    return 10.6;
                }
                else if (sens == 1.9) {
                    return 9.9;
                }
                else if (sens == 1.8) {
                    return 9.45;
                }
                else if (sens == 1.7) {
                    return 8.96;
                }
                else if (sens == 1.6) {
                    return 8.52;
                }
                else if (sens == 1.5) {
                    return 7.81;
                }
                else if (sens == 1.4) {
                    return 7.37;
                }
                else if (sens == 1.3) {
                    return 6.84;
                }
                else if (sens == 1.2) {
                    return 6.19;
                }
                else if (sens == 1.1) {
                    return 5.5;
                }
                else if (sens == 1) {
                    return 5.25;
                }
                else if (sens == 0.9) {
                    return 4.9;
                }
                else if (sens == 0.8) {
                    return 4.12;
                }
                else if (sens == 0.7) {
                    return 3.57;
                }
                else if (sens == 0.6) {
                    return 3.09;
                }
                else if (sens == 0.5) {
                    return 2.62;
                }
                else if (sens == 0.4) {
                    return 1.92;
                }
                else if (sens == 0.3) {
                    return 1.33;
                }
                else if (sens == 0.2) {
                    return 0.95;
                }
                else if (sens == 0.1) {
                    return 0.43;
                }
                return -1;
            }

            else if (gameFrom == "Minecraft") {

                //check that sens is a whole number and in the sens limit
                if (sens % 1 == 0 && sens > 0 && sens <= 200) {
                    return minecraftSenses[(int)sens - 1];
                }
                return -1;
            }

            else if (gameFrom == "Smite") {

                //check that sens is a whole number and in the sens limit
                if (sens % 1 == 0 && sens > 0 && sens <= 100) {
                    return smiteSenses[(int)sens - 1];
                }
                return -1;
            }

            else if (gameFrom == "Paladins") {

                //check that sens is a whole number and in the sens limit
                if (sens % 1 == 0 && sens > 0 && sens <= 100) {
                    return paladinsSenses[(int)sens - 1];
                }
                return -1;
            }

            else if (gameFrom == "Warframe") {

                //check that sens is a whole number and in the sens limit
                if (sens % 1 == 0 && sens > 0 && sens <= 100) {
                    return warframeSenses[(int)sens - 1];
                }
                return -1;
            }

            else if (gameFrom == "The Outer Worlds") {

                //check that sens is a whole number and in the sens limit
                if (sens % 1 == 0 && sens > 0 && sens <= 100) {
                    return outerWorldsSenses[(int)sens - 1];
                }
                return -1;
            }

            return -1;
        }
        #endregion (2) Convert input sensitivity to source sensitivity

        #region (2) Given the source sens + dpi, return cm2D 
        public string CalcCM2D(string gameFrom, string sensFrom, string dpiFrom) {

            double sens = ConvertToSourceSens(gameFrom, sensFrom);
            if (sens == -1 || Double.IsInfinity(sens)) {
                return "n/a";
            }

            double dpi;

            try {
                dpi = Convert.ToDouble(dpiFrom);
                if (dpi == 0) {
                    return "n/a";
                }
            }
            catch (FormatException) {
                return "n/a";
            }

            double cm2D = cmSource;
            cm2D = cm2D * (100 / dpi);
            cm2D = cm2D * (1.5 / sens);

            return Convert.ToString(Math.Round(cm2D, 2));
        }
        #endregion

        #region (3) Given the source sens + dpi, return in2D
        public string CalcIN2D(string gameFrom, string sensFrom, string dpiFrom) {

            double cm2D;

            try {
                cm2D = Convert.ToDouble(CalcCM2D(gameFrom, sensFrom, dpiFrom));
                return Convert.ToString(Math.Round(cm2D / inCmRatio, 2));
            }
            catch (FormatException) {
                return "n/a";
            }
        }
        #endregion (4) Given the source sens + dpi, return in2D

        #region (4) Given cm2D, return new DPI
        public string NewDPI(string gameFrom, string sensFrom, string dpiFrom) {

            double cm;

            try {
                cm = Convert.ToDouble(CalcCM2D(gameFrom, sensFrom, dpiFrom));
                if (cm == 0) {
                    return "n/a";
                }
                else
                    return Convert.ToString(Math.Round(4700 / cm, 0));
            }
            catch (FormatException) {
                return "n/a";
            }
        }
        #endregion

    }
}
