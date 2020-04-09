using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens.Services {

    /// <summary>
    /// Calculator class implementing ICalculator. The class provides functionality for the 
    /// first 3D to 3D sensitivity converter.
    /// </summary>
    public class Calculator : ICalculator {

        //fixed helper values
        private readonly double sourceYaw = 0.022;
        private readonly double quakeYaw = 0.022;
        private readonly double fortniteYaw = 0.5555;
        private readonly double overwatchYaw = 0.0066;
        private readonly double measuringDPI = 400;
        private readonly double inCmRatio = 2.54;


        /*CONVERT FROM SENS1 TO CM/360*/

        #region (1) LOW LEVEL- Given a sens, find cm/360 for game x (non-yaw, default 400 DPI)
        //f(sens)=cm/360
        public double SmiteFunc(string sensFrom) {

            double sens = Convert.ToDouble(sensFrom);

            //exponential curve for the seperate boundaries of possible sens values
            if (75 <= sens && sens <= 100) {
                return 10.43 * Math.Pow(0.9883, sens);
            }
            else if (50 <= sens && sens < 75) {
                return 12.28 * Math.Pow(0.9861, sens);
            }
            else if (25 <= sens && sens < 50) {
                return 20.20 * Math.Pow(0.9763, sens);
            }
            else if (15 <= sens && sens < 25) {
                return 52.71 * Math.Pow(0.9396, sens);
            }
            else if (10 <= sens && sens < 15) {
                return 68.19 * Math.Pow(0.9236, sens);
            }
            else if (5 <= sens && sens < 10) {
                return 126.42 * Math.Pow(0.8683, sens);
            }
            else if (1 <= sens && sens < 5) {
                return 508.60 * Math.Pow(0.6573, sens);
            }
            //if sens is outside of the range, -1 so that cm is negative and can be seperatly handled elsewhere
            return -1;
        }

        //f(sens)=cm/360
        public double PaladinsFunc(string sensFrom) {

            double sens = Convert.ToDouble(sensFrom);

            //exponential curve for the seperate boundaries of possible sens values
            if (75 <= sens && sens <= 100) {
                return 9.75 * Math.Pow(0.9857, sens);
            }
            else if (50 <= sens && sens < 75) {
                return 10.16 * Math.Pow(0.9851, sens);
            }
            else if (25 <= sens && sens < 50) {
                return 19.60 * Math.Pow(0.9723, sens);
            }
            else if (15 <= sens && sens < 25) {
                return 34.96 * Math.Pow(0.9500, sens);
            }
            else if (10 <= sens && sens < 15) {
                return 55.35 * Math.Pow(0.9216, sens);
            }
            else if (5 <= sens && sens < 10) {
                return 100.83 * Math.Pow(0.8677, sens);
            }
            else if (1 <= sens && sens < 5) {
                return 378.34 * Math.Pow(0.6661, sens);
            }

            return -1;
        }

        //f(sens)=cm/360
        public double WarframeFunc(string sensFrom) {

            double sens = Convert.ToDouble(sensFrom);

            //exponential curve for the seperate boundaries of possible sens values
            if (75 <= sens && sens <= 100) {
                return 8.55 * Math.Pow(0.9892, sens);
            }
            else if (50 <= sens && sens < 75) {
                return 11.52 * Math.Pow(0.9853, sens);
            }
            else if (25 <= sens && sens < 50) {
                return 17.82 * Math.Pow(0.9768, sens);
            }
            else if (15 <= sens && sens < 25) {
                return 27.51 * Math.Pow(0.9599, sens);
            }
            else if (10 <= sens && sens < 15) {
                return 34.96 * Math.Pow(0.9447, sens);
            }
            else if (5 <= sens && sens < 10) {
                return 46.06 * Math.Pow(0.9190, sens);
            }
            else if (1 <= sens && sens < 5) {
                return 63.3 * Math.Pow(0.8624, sens);
            }

            return -1;
        }

        //f(sens)=cm/360
        public double MinecraftFunc(string sensFrom) {

            double sens = Convert.ToDouble(sensFrom);

            //exponential curve for the seperate boundaries of possible sens values
            if (175 <= sens && sens <= 200) {
                return 29.68 * Math.Pow(0.9896, sens);
            }
            else if (150 <= sens && sens < 175) {
                return 49.55 * Math.Pow(0.9867, sens);
            }
            else if (125 <= sens && sens < 150) {
                return 57.98 * Math.Pow(0.9857, sens);
            }
            else if (100 <= sens && sens < 125) {
                return 75.47 * Math.Pow(0.9836, sens);
            }
            else if (75 <= sens && sens < 100) {
                return 99.75 * Math.Pow(0.9809, sens);
            }
            else if (50 <= sens && sens < 75) {
                return 139.01 * Math.Pow(0.9766, sens);
            }
            else if (25 <= sens && sens < 50) {
                return 177.28 * Math.Pow(0.9718, sens);
            }
            else if (15 <= sens && sens < 25) {
                return 195.47 * Math.Pow(0.9680, sens);
            }
            else if (10 <= sens && sens < 15) {
                return 209.61 * Math.Pow(0.9636, sens);
            }
            else if (5 <= sens && sens < 10) {
                return 222.33 * Math.Pow(0.9579, sens);
            }
            else if (1 <= sens && sens < 5) {
                return 220.4 * Math.Pow(0.9596, sens);
            }

            return -1;
        }

        //f(sens)=cm/360
        public double OuterWorldsFunc(string sensFrom) {

            double sens = Convert.ToDouble(sensFrom);

            //exponential curve for the seperate boundaries of possible sens values
            if (75 <= sens && sens <= 100) {
                return 13.30 * Math.Pow(0.9878, sens);
            }
            else if (50 <= sens && sens < 75) {
                return 16.89 * Math.Pow(0.9847, sens);
            }
            else if (25 <= sens && sens < 50) {
                return 14.13 * Math.Pow(0.9882, sens);
            }
            else if (15 <= sens && sens < 25) {
                return 130.82 * Math.Pow(0.9040, sens);
            }
            else if (10 <= sens && sens < 15) {
                return 72.62 * Math.Pow(0.9402, sens);
            }
            else if (5.5 <= sens && sens < 10) {
                return 146.57 * Math.Pow(0.8764, sens);
            }
            //tolerance 0.5 so 4.5-5.5 inclusive (else if deals with overlap) 
            else if (Math.Abs(sens - 5) <= 0.5) {
                return 75.8;
            }
            //tolerance 0.5 so 3.5-4.5 inclusive (else if deals with overlap)
            else if (Math.Abs(sens - 4) <= 0.5) {
                return 89.8;
            }
            //tolerance 0.5 so 2.5-3.5 inclusive (else if deals with overlap)
            else if (Math.Abs(sens - 3) <= 0.5) {
                return 116.9;
            }
            //tolerance 0.5 so 1.5-2.5 inclusive (else if deals with overlap)
            else if (Math.Abs(sens - 2) <= 0.5) {
                return 160.7;
            }
            //tolerance 0.5 so 0.5-1.5 inclusive (else if deals with overlap)
            else if (Math.Abs(sens - 1) <= 0.5) {
                return 261.5;
            }
            //tolerance 0.5 so 0.5-1.5 inclusive (else if deals with overlap)
            else if (Math.Abs(sens - 1) <= 0.5 && sens >= 0) {
                return 552.6;
            }

            return -1;
        }

        //f(sens)=cm/360
        public double RustFunc(string sensFrom) {

            double sens = Convert.ToDouble(sensFrom);

            if (sens == 2) {
                return 9.8;
            }
            else if (sens == 1.9) {
                return 10.5;
            }
            else if (sens == 1.8) {
                return 11;
            }
            else if (sens == 1.7) {
                return 11.6;
            }
            else if (sens == 1.6) {
                return 12.2;
            }
            else if (sens == 1.5) {
                return 13.3;
            }
            else if (sens == 1.4) {
                return 14.1;
            }
            else if (sens == 1.3) {
                return 15.2;
            }
            else if (sens == 1.2) {
                return 16.8;
            }
            else if (sens == 1.1) {
                return 18.9;
            }
            else if (sens == 1) {
                return 19.8;
            }
            else if (sens == 0.9) {
                return 21.2;
            }
            else if (sens == 0.8) {
                return 25.2;
            }
            else if (sens == 0.7) {
                return 29.1;
            }
            else if (sens == 0.6) {
                return 33.6;
            }
            else if (sens == 0.5) {
                return 39.6;
            }
            else if (sens == 0.4) {
                return 54.1;
            }
            else if (sens == 0.3) {
                return 78.4;
            }
            else if (sens == 0.2) {
                return 108.9;
            }
            else if (sens == 0.1) {
                return 244.4;
            }

            return -1;
        }
        #endregion Low Level - Given a sens, find cm/360 for game x (non-yaw, default 400 DPI)

        #region (2) Given a game + sens + DPI, find cm/360 (yaw + non-yaw (1))
        public string CalcCM(string gameFrom, string sensFrom, string dpiFrom) {

            double sens;
            try {
                sens = Convert.ToDouble(sensFrom);
            }
            catch (FormatException) {
                return "n/a";
            }

            double dpi;
            try {
                dpi = Convert.ToDouble(dpiFrom);
            }
            catch (FormatException) {
                return "n/a";
            }

            if (gameFrom == "Apex Legends" || gameFrom == "CS:GO" || gameFrom == "CS Source" || gameFrom == "CS 1.6" || gameFrom == "Garry's Mod" || gameFrom == "Half Life 2" || gameFrom == "Left 4 Dead 2" || gameFrom == "Team Fortress 2" || gameFrom == "Portal 2") {
                if (dpiFrom == null || sensFrom == null) {
                    return "n/a";
                }
                return Math.Round(((360 / (sens * dpi * sourceYaw)) * inCmRatio), 2).ToString();
            }
            else if (gameFrom == "Quake Live") {
                if (dpiFrom == null || sensFrom == null) {
                    return "n/a";
                }
                return Math.Round(((360 / (sens * dpi * quakeYaw)) * inCmRatio), 2).ToString();
            }
            else if (gameFrom == "Fortnite") {
                if (dpiFrom == null || sensFrom == null) {
                    return "n/a";
                }
                return Math.Round(((360 / (sens * dpi * fortniteYaw)) * inCmRatio), 2).ToString();
            }
            else if (gameFrom == "Overwatch") {
                if (dpiFrom == null || sensFrom == null) {
                    return "n/a";
                }
                return Math.Round(((360 / (sens * dpi * overwatchYaw)) * inCmRatio), 2).ToString();
            }
            else if (gameFrom == "Smite") {
                if (SmiteFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((SmiteFunc(sens.ToString()) * (dpi / measuringDPI)), 2).ToString();
            }
            else if (gameFrom == "Paladins") {
                if (PaladinsFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((PaladinsFunc(sens.ToString()) * (dpi / measuringDPI)), 2).ToString();
            }
            else if (gameFrom == "Minecraft") {
                if (MinecraftFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((MinecraftFunc(sens.ToString()) * (dpi / measuringDPI)), 2).ToString();
            }
            else if (gameFrom == "Rust") {
                if (RustFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((RustFunc(sens.ToString()) * (dpi / measuringDPI)), 2).ToString();
            }
            else if (gameFrom == "The Outer Worlds") {
                if (OuterWorldsFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((OuterWorldsFunc(sens.ToString()) * (dpi / measuringDPI)), 2).ToString();
            }
            else if (gameFrom == "Warframe") {
                if (WarframeFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((WarframeFunc(sens.ToString()) * (dpi / measuringDPI)), 2).ToString();
            }

            return "n/a";
        }
        #endregion (1) Given a sens and DPI, find cm/360 for game x (yaw + non-yaw (4))

        #region (3) Convert cm/360(2) to in/360
        public string CalcIN(string gameFrom, string sensFrom, string dpiFrom) {

            double sens;
            try {
                sens = Convert.ToDouble(CalcCM(gameFrom, sensFrom, dpiFrom));
                return Math.Round((sens / inCmRatio), 2).ToString();

            }
            catch (FormatException) {
                return "n/a";
            }
        }
        #endregion Convert cm/360(1) to in/360

        /*CONVERT BACK FROM CM/360 TO SENS2*/

        #region (4) PREP- Given a game + sens, return cm/360 with default DPI (yaw and non-yaw) 
        public string CalcCMFixedDPI(string gameFrom, string sensFrom) {

            double sens;
            try {
                sens = Convert.ToDouble(sensFrom);
            }
            catch (FormatException) {
                return "n/a";
            }

            double dpi = measuringDPI;


            if (gameFrom == "Apex Legends" || gameFrom == "CS:GO" || gameFrom == "CS Source" || gameFrom == "CS 1.6" || gameFrom == "Garry's Mod" || gameFrom == "Half Life 2" || gameFrom == "Left 4 Dead 2" || gameFrom == "Team Fortress 2" || gameFrom == "Portal 2") {
                if (sensFrom == null) {
                    return "n/a";
                }
                return Math.Round(((360 / (sens * dpi * sourceYaw)) * inCmRatio), 8).ToString();
            }
            else if (gameFrom == "Quake Live") {
                if (sensFrom == null) {
                    return "n/a";
                }
                return Math.Round(((360 / (sens * dpi * quakeYaw)) * inCmRatio), 8).ToString();
            }
            else if (gameFrom == "Fortnite") {
                if (sensFrom == null) {
                    return "n/a";
                }
                return Math.Round(((360 / (sens * dpi * fortniteYaw)) * inCmRatio), 8).ToString();
            }
            else if (gameFrom == "Overwatch") {
                if (sensFrom == null) {
                    return "n/a";
                }
                return Math.Round(((360 / (sens * dpi * overwatchYaw)) * inCmRatio), 8).ToString();
            }
            else if (gameFrom == "Smite") {
                if (SmiteFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((SmiteFunc(sens.ToString())), 2).ToString();
            }
            else if (gameFrom == "Paladins") {
                if (PaladinsFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((PaladinsFunc(sens.ToString())), 2).ToString();
            }
            else if (gameFrom == "Minecraft") {
                if (MinecraftFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((MinecraftFunc(sens.ToString())), 2).ToString();
            }
            else if (gameFrom == "Rust") {
                if (RustFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((RustFunc(sens.ToString())), 2).ToString();
            }
            else if (gameFrom == "The Outer Worlds") {
                if (OuterWorldsFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((OuterWorldsFunc(sens.ToString())), 2).ToString();
            }
            else if (gameFrom == "Warframe") {
                if (WarframeFunc(sens.ToString()) == -1) {
                    return "n/a";
                }
                return Math.Round((WarframeFunc(sens.ToString())), 2).ToString();
            }

            return "n/a";
        }

        #endregion (5) PREP- Given a sens, return cm/360 with default DPI (yaw and non-yaw)

        #region (5) LOW LEVEL - Return new sens for game x (yaw and non-yaw, default 400 DPI)

        public double SourceCalc(string gameFrom, string sensFrom) {

            double inches;
            try {
                inches = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom)) / inCmRatio;
            }
            catch (FormatException) {
                return -1;
            }

            return 360 / (measuringDPI * inches * sourceYaw);
        }

        public double FortniteCalc(string gameFrom, string sensFrom) {

            double inches;
            try {
                inches = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom)) / inCmRatio;
            }
            catch (FormatException) {
                return -1;
            }

            return 360 / (measuringDPI * inches * fortniteYaw);
        }

        public double OverwatchCalc(string gameFrom, string sensFrom) {

            double inches;
            try {
                inches = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom)) / inCmRatio;
            }
            catch (FormatException) {
                return -1;
            }

            return 360 / (measuringDPI * inches * overwatchYaw);
        }

        public double QuakeCalc(string gameFrom, string sensFrom) {

            double inches;
            try {
                inches = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom)) / inCmRatio;
            }
            catch (FormatException) {
                return -1;
            }

            return 360 / (measuringDPI * inches * quakeYaw);
        }

        public double SmiteCalc(string gameFrom, string sensFrom) {

            double cm;
            try {
                cm = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom));
            }
            catch (FormatException) {
                return -1;
            }

            //exponential curve for the seperate boundaries of possible sens values
            if (3.2 <= cm && cm <= 4.3) {
                try {
                    return (Math.Log(cm / 10.43, 0.9883));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (4.3 <= cm && cm < 6.1) {
                try {
                    return (Math.Log(cm / 12.28, 0.9861));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (6.1 <= cm && cm < 11.1) {
                try {
                    return (Math.Log(cm / 20.20, 0.9763));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (11.1 <= cm && cm < 20.7) {
                try {
                    return (Math.Log(cm / 52.72, 0.9396));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (20.7 <= cm && cm < 30.8) {
                try {
                    return (Math.Log(cm / 68.19, 0.9236));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (30.8 <= cm && cm < 62.4) {
                try {
                    return (Math.Log(cm / 126.42, 0.8683));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (62.4 <= cm && cm <= 334.3) {
                try {
                    return (Math.Log(cm / 508.60, 0.6573));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            //if sens is outside of the range, -1 so that yaw is negative and can be seperatly handled elsewhere
            return -1;
        }

        //f(sens)=cm/360
        public double PaladinsCalc(string gameFrom, string sensFrom) {

            double cm;
            try {
                cm = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom));
            }
            catch (FormatException) {
                return -1;
            }

            //exponential curve for the seperate boundaries of possible sens values
            if (2.3 <= cm && cm <= 3.3) {
                try {
                    return (Math.Log(cm / 9.75, 0.9857));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (3.3 <= cm && cm < 4.8) {
                try {
                    return (Math.Log(cm / 10.16, 0.9851));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (4.8 <= cm && cm < 9.7) {
                try {
                    return (Math.Log(cm / 19.60, 0.9723));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (9.7 <= cm && cm < 16.2) {
                try {
                    return (Math.Log(cm / 34.96, 0.9500));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (16.2 <= cm && cm < 24.4) {
                try {
                    return (Math.Log(cm / 55.35, 0.9216));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (24.4 <= cm && cm < 49.6) {
                try {
                    return (Math.Log(cm / 100.83, 0.8677));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (49.6 <= cm && cm <= 252) {
                try {
                    return (Math.Log(cm / 378.34, 0.6661));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            //if sens is outside of the range, N/A
            return -1;
        }

        //f(sens)=cm/360
        public double WarframeCalc(string gameFrom, string sensFrom) {

            double cm;
            try {
                cm = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom));
            }
            catch (FormatException) {
                return -1;
            }

            //exponential curve for the seperate boundaries of possible sens values
            if (2.9 <= cm && cm <= 3.8) {
                try {
                    return (Math.Log(cm / 8.55, 0.9892));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (3.8 <= cm && cm < 5.5) {
                try {
                    return (Math.Log(cm / 11.52, 0.9853));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (5.5 <= cm && cm < 9.9) {
                try {
                    return (Math.Log(cm / 17.82, 0.9768));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (9.9 <= cm && cm < 14.9) {
                try {
                    return (Math.Log(cm / 27.51, 0.9599));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (14.9 <= cm && cm < 19.8) {
                try {
                    return (Math.Log(cm / 34.96, 0.9447));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (19.8 <= cm && cm < 30.2) {
                try {
                    return (Math.Log(cm / 46.06, 0.9190));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (30.2 <= cm && cm < 63.3) {
                try {
                    return (Math.Log(cm / 63.3, 0.8624));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            //if sens is outside of the range, -1 so that yaw is negative and can be seperatly handled elsewhere
            return -1;
        }

        //f(sens)=cm/360
        public double MinecraftCalc(string gameFrom, string sensFrom) {

            double cm;
            try {
                cm = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom));
            }
            catch (FormatException) {
                return -1;
            }

            //exponential curve for the seperate boundaries of possible sens values
            if (3.7 <= cm && cm <= 4.8) {
                try {
                    return (Math.Log(cm / 29.68, 0.9896));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (4.8 <= cm && cm < 6.7) {
                try {
                    return (Math.Log(cm / 49.55, 0.9867));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (6.7 <= cm && cm < 9.6) {
                try {
                    return (Math.Log(cm / 57.98, 0.9857));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (9.6 <= cm && cm < 14.5) {
                try {
                    return (Math.Log(cm / 75.37, 0.9836));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (14.5 <= cm && cm < 23.5) {
                try {
                    return (Math.Log(cm / 100.04, 0.9809));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (23.5 <= cm && cm < 42.5) {
                try {
                    return (Math.Log(cm / 139.01, 0.9766));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (42.5 <= cm && cm < 86.8) {
                try {
                    return (Math.Log(cm / 177.28, 0.9718));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (86.8 <= cm && cm < 120.1) {
                try {
                    return (Math.Log(cm / 195.47, 0.9680));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (120.1 <= cm && cm < 144.6) {
                try {
                    return (Math.Log(cm / 209.61, 0.9636));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (144.6 <= cm && cm < 179.3) {
                try {
                    return (Math.Log(cm / 222.33, 0.9579));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (179.3 <= cm && cm < 220.4) {
                try {
                    return (Math.Log(cm / 220.40, 0.9596));
                }
                catch (FormatException) {
                    return -1;
                }
            }

            //if sens is outside of the range, -1 so that yaw is negative and can be seperatly handled elsewhere
            return -1;
        }

        //f(sens)=cm/360
        public double OuterWorldsCalc(string gameFrom, string sensFrom) {

            double cm;
            try {
                cm = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom));
            }
            catch (FormatException) {
                return -1;
            }

            //exponential curve for the seperate boundaries of possible sens values
            if (3.9 <= cm && cm <= 5.3) {
                try {
                    return (Math.Log(cm / 13.30, 0.9878));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (5.3 <= cm && cm < 7.8) {
                try {
                    return (Math.Log(cm / 16.89, 0.9847));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (7.8 <= cm && cm < 10.5) {
                try {
                    return (Math.Log(cm / 14.13, 0.9882));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (10.5 <= cm && cm < 28.8) {
                try {
                    return (Math.Log(cm / 130.83, 0.9040));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (28.8 <= cm && cm < 39.2) {
                try {
                    return (Math.Log(cm / 72.62, 0.9402));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            else if (39.2 <= cm && cm < 75.8) {
                try {
                    return (Math.Log(cm / 146.57, 0.8764));
                }
                catch (FormatException) {
                    return -1;
                }
            }
            //realised, there's no need for precise lower bound because of else if. As long as it doesn't run below min.
            else if (3.9 <= cm && cm < 82.8) {
                return 5;
            }
            //tolerance 0.5 so 3.5-4.5 inclusive (else if deals with overlap)
            else if (3.9 <= cm && cm < 103.4) {
                return 4;
            }
            //tolerance 0.5 so 2.5-3.5 inclusive (else if deals with overlap)
            else if (3.9 <= cm && cm < 138.8) {
                return 3;
            }
            //tolerance 0.5 so 1.5-2.5 inclusive (else if deals with overlap)
            else if (3.9 <= cm && cm < 211.1) {
                return 2;
            }
            //tolerance 0.5 so 0.5-1.5 inclusive (else if deals with overlap)
            else if (3.9 <= cm && cm < 407.1) {
                return 1;
            }
            //tolerance 0.5 so 0.5-1.5 inclusive (else if deals with overlap)
            else if (3.9 <= cm && cm < 600) {
                return 0;
            }

            //if sens is outside of the range, -1 so that yaw is negative and can be seperatly handled elsewhere
            return -1;
        }

        //f(sens)=cm/360
        public double RustCalc(string gameFrom, string sensFrom) {

            double cm;
            try {
                cm = Convert.ToDouble(CalcCMFixedDPI(gameFrom, sensFrom));
            }
            catch (FormatException) {
                return -1;
            }

            if (9.8 <= cm && cm < 10.2) {
                return 2;
            }
            else if (9.8 <= cm && cm < 10.8) {
                return 1.9;
            }
            else if (9.8 <= cm && cm < 11.3) {
                return 1.8;
            }
            else if (9.8 <= cm && cm < 11.9) {
                return 1.7;
            }
            else if (9.8 <= cm && cm < 12.8) {
                return 1.6;
            }
            else if (9.8 <= cm && cm < 13.7) {
                return 1.5;
            }
            else if (9.8 <= cm && cm < 14.7) {
                return 1.4;
            }
            else if (9.8 <= cm && cm < 16.0) {
                return 1.3;
            }
            else if (9.8 <= cm && cm < 17.1) {
                return 1.2;
            }
            else if (9.8 <= cm && cm < 19.4) {
                return 1.1;
            }
            else if (9.8 <= cm && cm < 20.5) {
                return 1.0;
            }
            else if (9.8 <= cm && cm < 23.0) {
                return 0.9;
            }
            else if (9.8 <= cm && cm < 26.9) {
                return 0.8;
            }
            else if (9.8 <= cm && cm < 31.4) {
                return 0.7;
            }
            else if (9.8 <= cm && cm < 36.6) {
                return 0.6;
            }
            else if (9.8 <= cm && cm < 46.9) {
                return 0.5;
            }
            else if (9.8 <= cm && cm < 66.3) {
                return 0.4;
            }
            else if (9.8 <= cm && cm < 93.7) {
                return 0.3;
            }
            else if (9.8 <= cm && cm < 176.7) {
                return 0.2;
            }
            else if (9.8 <= cm && cm < 250) {
                return 0.1;
            }

            //if sens is outside of the range, -1 so that yaw is negative and can be seperatly handled elsewhere
            return -1;
        }

        #endregion (6) Low Level - Given DPI + in/360 + yaw, return sens (yaw, default 400 DPI). Given cm/360, return sens (non-yaw)

        #region (6) Given gameFrom + gameTo + sens, return new sensitivity (yaw and non-yaw)
        public string NewSens(string gameTo, string gameFrom, string sensFrom) {

            if (gameTo == "Apex Legends" || gameTo == "CS:GO" || gameTo == "CS Source" || gameTo == "CS 1.6" || gameTo == "Garry's Mod" || gameTo == "Half Life 2" || gameTo == "Left 4 Dead 2" || gameTo == "Team Fortress 2" || gameTo == "Portal 2") {
                if (SourceCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(SourceCalc(gameFrom, sensFrom), 2).ToString();
            }

            if (gameTo == "Fortnite") {
                if (FortniteCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(FortniteCalc(gameFrom, sensFrom), 3).ToString();
            }

            if (gameTo == "Overwatch") {
                if (OverwatchCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(OverwatchCalc(gameFrom, sensFrom), 2).ToString();
            }

            if (gameTo == "Quake Live") {
                if (QuakeCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(QuakeCalc(gameFrom, sensFrom), 2).ToString();
            }

            if (gameTo == "Smite") {
                if (SmiteCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(SmiteCalc(gameFrom, sensFrom), 0).ToString();
            }
            else if (gameTo == "Paladins") {
                if (PaladinsCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(PaladinsCalc(gameFrom, sensFrom), 1).ToString();
            }
            else if (gameTo == "Minecraft") {
                if (MinecraftCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(MinecraftCalc(gameFrom, sensFrom), 0).ToString();
            }
            else if (gameTo == "The Outer Worlds") {
                if (OuterWorldsCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(OuterWorldsCalc(gameFrom, sensFrom), 0).ToString();
            }
            else if (gameTo == "Rust") {
                if (RustCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(RustCalc(gameFrom, sensFrom), 1).ToString();
            }
            else if (gameTo == "Warframe") {
                if (WarframeCalc(gameFrom, sensFrom) == -1) {
                    return "n/a";
                }
                return Math.Round(WarframeCalc(gameFrom, sensFrom), 0).ToString();
            }

            return "n/a";
        }
        #endregion (3) Given gameFrom + gameTo + sens, return new sensitivity (yaw and non-yaw)

    }
}
