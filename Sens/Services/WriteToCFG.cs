using Sens.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens.Services {

    /// <summary>
    /// 'Write to config' class implementing IWriteToCFG. This class provides functionality to
    /// overwrite a new sensitivity value to each games' config file
    /// </summary>
    public class WriteToCFG : IWriteToCFG {

        private ICalculator calculator;

        public WriteToCFG(ICalculator calculator) {
            this.calculator = calculator;
        }

        public double CalcCM3D(int profileNumber) {
            //grab the game and its current sensitivity from the DB
            List<ProfileModel> allProfiles = SQLiteDataAccess.LoadAllProfiles();
            ProfileModel profile = allProfiles[profileNumber - 1];
            string gameFrom = profile.SensitivityFrom;
            string currentSens = profile.Sensitivity;

            //for each game: if ticked, calculate the enw sensitivity and overwrite it in the corresponding cfg file
            if (profile.Apex == 1) {
                string newSens = calculator.NewSens("Apex Legends", gameFrom, currentSens);
                WriteToApex(newSens);
            }
            if (profile.CSGO == 1) {
                string newSens = calculator.NewSens("CS:GO", gameFrom, currentSens);
                WriteToCSGO(newSens);
            }
            if (profile.CS16 == 1) {
                string newSens = calculator.NewSens("CS 1.6", gameFrom, currentSens);
                WriteToCS16(newSens);
            }
            if (profile.CSS == 1) {
                string newSens = calculator.NewSens("CS Source", gameFrom, currentSens);
                WriteToCSS(newSens);
            }
            if (profile.GMod == 1) {
                string newSens = calculator.NewSens("Garry's Mod", gameFrom, currentSens);
                WriteToGMOD(newSens);
            }
            if (profile.HL2 == 1) {
                string newSens = calculator.NewSens("Half Life 2", gameFrom, currentSens);
                WriteToHL2(newSens);
            }
            if (profile.L4D2 == 1) {
                string newSens = calculator.NewSens("Left 4 Dead 2", gameFrom, currentSens);
                WriteToL4D2(newSens);
            }
            if (profile.Minecraft == 1) {
                string newSens = calculator.NewSens("Minecraft", gameFrom, currentSens);
                WriteToMinecraft(newSens);
            }
            if (profile.Paladins == 1) {
                string newSens = calculator.NewSens("Paladins", gameFrom, currentSens);
                WriteToPaladins(newSens);
            }
            if (profile.Quake == 1) {
                string newSens = calculator.NewSens("Quake Live", gameFrom, currentSens);
                WriteToQuake(newSens);
            }
            if (profile.Rust == 1) {
                string newSens = calculator.NewSens("Rust", gameFrom, currentSens);
                WriteToRust(newSens);
            }
            if (profile.Smite == 1) {
                string newSens = calculator.NewSens("Smite", gameFrom, currentSens);
                WriteToSmite(newSens);
            }
            if (profile.TF2 == 1) {
                string newSens = calculator.NewSens("Team Fortress 2", gameFrom, currentSens);
                WriteToTF2(newSens);
            }
            if (profile.Portal2 == 1) {
                string newSens = calculator.NewSens("Portal 2", gameFrom, currentSens);
                WriteToPortal2(newSens);
            }

            return 0;
        }

        public void WriteToApex(string sens) {

            //example full path C:\Users\matej\Saved Games\Respawn\Apex\local\settings.cfg  mouse_sensitivity "2.957857"

            string userPath = @"C:\Users";
            string cfgPath = @"\Saved Games\Respawn\Apex\local\settings.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            string[] users = { };

            try {
                //there can be multiple users with apex...
                users = Directory.GetDirectories(userPath);
            }
            catch (DirectoryNotFoundException) { //if there isn't a user then catch and return
                return;
            }
            //...loop through each account folder
            foreach (string user in users) {
                string fullPath = user + cfgPath;

                try {
                    //find the sensitivity line, eg. sensitivity "1.633133"
                    using (StreamReader sr = new StreamReader(fullPath)) {
                        string line;

                        while ((line = sr.ReadLine()) != null) {

                            lineToReplace++;

                            if (line.StartsWith("mouse_sensitivity ")) {

                                int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                                if (line.IndexOf('m') == index) {

                                    int iFrom = line.IndexOf("mouse_sensitivity \"") + "mouse_sensitivity \"".Length;
                                    int iTo = line.LastIndexOf("\"");
                                    string result = line.Substring(iFrom, iTo - iFrom);
                                    currentSens = result;
                                }
                                break;
                            }
                        }
                    }
                }
                catch (IOException) { //keep looping throught the users even if a user doesn't have apex installed
                }
                try {
                    if (currentSens != "") {
                        //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                        string[] cfg = File.ReadAllLines(fullPath);
                        string oldLine = "mouse_sensitivity \"" + currentSens + "\"";
                        string newLine = "mouse_sensitivity \"" + sens + "\"";
                        cfg[lineToReplace - 1] = newLine;
                        File.WriteAllLines(fullPath, cfg);
                        lineToReplace = 0; //reset line count before reading the next cfg
                    }
                }
                catch (IOException) { //ignore if apex not installed for other users
                }
            }
        }

        public void WriteToCSGO(string sens) {

            //example full path C:\Program Files (x86)\Steam\userdata\310967857\730\local\cfg\config.cfg  sensitivity "1.633133"

            string accountPath = @"C:\Program Files (x86)\Steam\userdata";
            string cfgPath = @"\730\local\cfg\config.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            string[] accounts = { };

            try {
                //the user can have multiple csgo accounts...
                accounts = Directory.GetDirectories(accountPath);
            }
            catch (DirectoryNotFoundException) { //if no accounts are found then catch and return
                return;
            }
            //...loop through each account folder
            foreach (string account in accounts) {
                string fullPath = account + cfgPath;
                try {
                    //find the sensitivity line, eg. sensitivity "1.633133"
                    using (StreamReader sr = new StreamReader(fullPath)) {
                        string line;

                        while ((line = sr.ReadLine()) != null) {

                            lineToReplace++;

                            if (line.StartsWith("sensitivity ")) {

                                int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                                if (line.IndexOf('s') == index) {

                                    int iFrom = line.IndexOf("sensitivity \"") + "sensitivity \"".Length;
                                    int iTo = line.LastIndexOf("\"");
                                    string result = line.Substring(iFrom, iTo - iFrom);
                                    currentSens = result;
                                }
                                break;
                            }
                        }
                    }
                }
                catch { //keep looping through the accounts even if the account doesn't have csgo installed
                }
                try {
                    if (currentSens != "") {
                        //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                        string[] cfg = File.ReadAllLines(fullPath);
                        string oldLine = "sensitivity \"" + currentSens + "\"";
                        string newLine = "sensitivity \"" + sens + "\"";
                        cfg[lineToReplace - 1] = newLine;
                        File.WriteAllLines(fullPath, cfg);
                        lineToReplace = 0; //reset line count before reading the next cfg
                    }
                }
                catch {
                } //user doesn't have csgo installed
            }
        }

        public void WriteToCS16(string sens) {

            //example full path C:\Program Files(x86)\Steam\steamapps\common\Half - Life\cstrike\config.cfg  sensitivity "1.633133"

            string fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life\cstrike\config.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            try {
                //find the sensitivity line, eg. sensitivity "1.633133"
                using (StreamReader sr = new StreamReader(fullPath)) {
                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        lineToReplace++;

                        if (line.StartsWith("sensitivity ")) {

                            int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                            if (line.IndexOf('s') == index) {

                                int iFrom = line.IndexOf("sensitivity \"") + "sensitivity \"".Length;
                                int iTo = line.LastIndexOf("\"");
                                string result = line.Substring(iFrom, iTo - iFrom);
                                currentSens = result;
                            }
                            break;
                        }
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            if (currentSens != "") {
                //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                string[] cfg = File.ReadAllLines(fullPath);
                string oldLine = "sensitivity \"" + currentSens + "\"";
                string newLine = "sensitivity \"" + sens + "\"";
                cfg[lineToReplace - 1] = newLine;
                File.WriteAllLines(fullPath, cfg);
            }
        }

        public void WriteToCSS(string sens) {

            //example full path C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Source\cstrike\cfg\config.cfg sensitivity "5.5555"

            string fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Source\cstrike\cfg\config.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            try {
                //find the sensitivity line, eg. sensitivity "1.633133"
                using (StreamReader sr = new StreamReader(fullPath)) {
                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        lineToReplace++;

                        if (line.StartsWith("sensitivity ")) {

                            int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                            if (line.IndexOf('s') == index) {

                                int iFrom = line.IndexOf("sensitivity \"") + "sensitivity \"".Length;
                                int iTo = line.LastIndexOf("\"");
                                string result = line.Substring(iFrom, iTo - iFrom);
                                currentSens = result;
                            }
                            break;
                        }
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            if (currentSens != "") {
                //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                string[] cfg = File.ReadAllLines(fullPath);
                string oldLine = "sensitivity \"" + currentSens + "\"";
                string newLine = "sensitivity \"" + sens + "\"";
                cfg[lineToReplace - 1] = newLine;
                File.WriteAllLines(fullPath, cfg);
            }
        }

        public void WriteToGMOD(string sens) {

            //example full path C:\Program Files (x86)\Steam\steamapps\common\GarrysMod\garrysmod\cfg\config.cfg sensitivity "5.5555"

            string fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\GarrysMod\garrysmod\cfg\config.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            try {
                //find the sensitivity line, eg. sensitivity "1.633133"
                using (StreamReader sr = new StreamReader(fullPath)) {
                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        lineToReplace++;

                        if (line.StartsWith("sensitivity ")) {

                            int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                            if (line.IndexOf('s') == index) {

                                int iFrom = line.IndexOf("sensitivity \"") + "sensitivity \"".Length;
                                int iTo = line.LastIndexOf("\"");
                                string result = line.Substring(iFrom, iTo - iFrom);
                                currentSens = result;
                            }
                            break;
                        }
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            if (currentSens != "") {
                //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                string[] cfg = File.ReadAllLines(fullPath);
                string oldLine = "sensitivity \"" + currentSens + "\"";
                string newLine = "sensitivity \"" + sens + "\"";
                cfg[lineToReplace - 1] = newLine;
                File.WriteAllLines(fullPath, cfg);
            }
        }

        public void WriteToHL2(string sens) {

            //example full path C:\Program Files (x86)\Steam\steamapps\common\Half-Life 2\hl2\cfg\config.cfg sensitivity "5.5555"

            string fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 2\hl2\cfg\config.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            try {
                //find the sensitivity line, eg. sensitivity "1.633133"
                using (StreamReader sr = new StreamReader(fullPath)) {
                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        lineToReplace++;

                        if (line.StartsWith("sensitivity ")) {

                            int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                            if (line.IndexOf('s') == index) {

                                int iFrom = line.IndexOf("sensitivity \"") + "sensitivity \"".Length;
                                int iTo = line.LastIndexOf("\"");
                                string result = line.Substring(iFrom, iTo - iFrom);
                                currentSens = result;
                            }
                            break;
                        }
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            if (currentSens != "") {
                //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                string[] cfg = File.ReadAllLines(fullPath);
                string oldLine = "sensitivity \"" + currentSens + "\"";
                string newLine = "sensitivity \"" + sens + "\"";
                cfg[lineToReplace - 1] = newLine;
                File.WriteAllLines(fullPath, cfg);
            }
        }

        public void WriteToL4D2(string sens) {

            //example full path C:\Program Files (x86)\Steam\steamapps\common\Left 4 Dead 2\left4dead2\cfg\config.cfg sensitivity "5.5555"

            string fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\Left 4 Dead 2\left4dead2\cfg\config.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")
            try {
                //find the sensitivity line, eg. sensitivity "1.633133"
                using (StreamReader sr = new StreamReader(fullPath)) {
                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        lineToReplace++;

                        if (line.StartsWith("sensitivity ")) {

                            int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                            if (line.IndexOf('s') == index) {

                                int iFrom = line.IndexOf("sensitivity \"") + "sensitivity \"".Length;
                                int iTo = line.LastIndexOf("\"");
                                string result = line.Substring(iFrom, iTo - iFrom);
                                currentSens = result;
                            }
                            break;
                        }
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            if (currentSens != "") {
                //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                string[] cfg = File.ReadAllLines(fullPath);
                string oldLine = "sensitivity \"" + currentSens + "\"";
                string newLine = "sensitivity \"" + sens + "\"";
                cfg[lineToReplace - 1] = newLine;
                File.WriteAllLines(fullPath, cfg);
            }
        }

        public void WriteToMinecraft(string sens) {

            //example full path C:\Users\matej\AppData\Roaming\.minecraft\options.txt  mouseSensitivity:0.38

            string userPath = @"C:\Users";
            string cfgPath = @"\AppData\Roaming\.minecraft\options.txt";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            //convert the sens (0%-200%) to (0-1) since options.txt uses (0-1)
            double tempSens = Convert.ToDouble(sens);
            string convertedSens = (tempSens / 200).ToString();

            string[] users = { };

            try {
                //there can be multiple users with a minceraft account...
                users = Directory.GetDirectories(userPath);
            }
            catch (DirectoryNotFoundException) { //if there aren't any users then catch and return
                return;
            }
            //...loop through each account folder
            foreach (string user in users) {
                string fullPath = user + cfgPath;

                try {
                    //find the sensitivity line, eg. sensitivity "1.633133"
                    using (StreamReader sr = new StreamReader(fullPath)) {
                        string line;

                        while ((line = sr.ReadLine()) != null) {

                            lineToReplace++;

                            if (line.StartsWith("mouseSensitivity:")) {

                                int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                                if (line.IndexOf('m') == index) {

                                    int iFrom = line.IndexOf("mouseSensitivity:") + "mouseSensitivity:".Length;
                                    int iTo = line.Length;
                                    string result = line.Substring(iFrom, iTo - iFrom);
                                    currentSens = result;
                                }
                                break;
                            }
                        }
                    }
                }
                catch { //keep looping throught the users even if a user doesn't have minecraft installed
                }
                try {
                    if (currentSens != "") {
                        //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                        string[] cfg = File.ReadAllLines(fullPath);
                        string oldLine = "mouseSensitivity:" + currentSens;
                        string newLine = "mouseSensitivity:" + sens;
                        cfg[lineToReplace - 1] = newLine;
                        File.WriteAllLines(fullPath, cfg);
                        lineToReplace = 0; //reset line count before reading the next cfg
                    }
                }
                catch { //ignore if minecraft not installed for other users
                }
            }
        }

        public void WriteToPaladins(string sens) {

            //example full path C:\Users\matej\Documents\My Games\Paladins\ChaosGame\Config\ChaosInput.ini  MouseSensitivity=2.9

            string userPath = @"C:\Users";
            string cfgPath = @"\Documents\My Games\Paladins\ChaosGame\Config\ChaosInput.ini";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            string[] users = { }; 

            try {
                //there can be multiple users with paladins...
                users = Directory.GetDirectories(userPath);
            }
            catch (DirectoryNotFoundException) { //if there aren't any users then catch and return)
                return;
            }

            //...loop through each account folder
            foreach (string user in users) {
                string fullPath = user + cfgPath;

                try {
                    //find the sensitivity line, eg. sensitivity "1.633133"
                    using (StreamReader sr = new StreamReader(fullPath)) {
                        string line;

                        while ((line = sr.ReadLine()) != null) {

                            lineToReplace++;

                            if (line.StartsWith("MouseSensitivity=")) {

                                int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                                if (line.IndexOf('M') == index) {

                                    int iFrom = line.IndexOf("MouseSensitivity=") + "MouseSensitivity=".Length;
                                    int iTo = line.Length;
                                    string result = line.Substring(iFrom, iTo - iFrom);
                                    currentSens = result;
                                }
                                break;
                            }
                        }
                    }
                }
                catch { //keep looping throught the users even if a user doesn't have paladins installed
                }
                try {
                    if (currentSens != "") {
                        //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                        string[] cfg = File.ReadAllLines(fullPath);
                        string oldLine = "MouseSensitivity=" + currentSens;
                        string newLine = "MouseSensitivity=" + sens;
                        cfg[lineToReplace - 1] = newLine;
                        File.WriteAllLines(fullPath, cfg);
                        lineToReplace = 0; //reset line count before reading the next cfg
                    }
                }
                catch { //ignore if paladins not installed for other users
                }
            }
        }

        public void WriteToPortal2(string sens) {

            //C:\Program Files (x86)\Steam\steamapps\common\Portal 2\update\cfg\config.cfg sensitivity "8.157534"

            string fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\Portal 2\update\cfg\config.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            try {
                //find the sensitivity line, eg. sensitivity "1.633133"
                using (StreamReader sr = new StreamReader(fullPath)) {
                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        lineToReplace++;

                        if (line.StartsWith("sensitivity ")) {

                            int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                            if (line.IndexOf('s') == index) {

                                int iFrom = line.IndexOf("sensitivity \"") + "sensitivity \"".Length;
                                int iTo = line.LastIndexOf("\"");
                                string result = line.Substring(iFrom, iTo - iFrom);
                                currentSens = result;
                            }
                        }
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            if (currentSens != "") {
                //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                string[] cfg = File.ReadAllLines(fullPath);
                string oldLine = "sensitivity \"" + currentSens + "\"";
                string newLine = "sensitivity \"" + sens + "\"";
                cfg[lineToReplace - 1] = newLine;
                File.WriteAllLines(fullPath, cfg);
            }
        }

        public void WriteToQuake(string sens) {
            // C:\Program Files (x86)\Steam\SteamApps\common\Quake Live\<SteamID64>\baseq3\qzconfig.cfg seta sensitivity "2.25"

            string accountPath = @"C:\Program Files (x86)\Steam\SteamApps\common\Quake Live";
            string cfgPath = @"\baseq3\qzconfig.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            string[] accounts = { };
            
            try {
                //the user can have multiple quake accounts...
                accounts = Directory.GetDirectories(accountPath);
            }
            catch (DirectoryNotFoundException) { //if there aren't any quake accoutns then catch and return
                return;
            }
            //...loop through each account folder
            foreach (string account in accounts) {

                string fullPath = account + cfgPath;

                try {
                    //find the sensitivity line, eg. seta sensitivity "2.25"
                    using (StreamReader sr = new StreamReader(fullPath)) {
                        string line;

                        while ((line = sr.ReadLine()) != null) {

                            lineToReplace++;

                            if (line.StartsWith("seta sensitivity ")) {

                                int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                                if (line.IndexOf('s') == index) {

                                    int iFrom = line.IndexOf("seta sensitivity \"") + "seta sensitivity \"".Length;
                                    int iTo = line.LastIndexOf("\"");
                                    string result = line.Substring(iFrom, iTo - iFrom);
                                    currentSens = result;
                                }
                                break;
                            }
                        }
                    }
                }
                catch (IOException e) {
                    Console.WriteLine("File could not be read.");
                    Console.WriteLine(e.Message);
                }

                if (currentSens != "") {
                    //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                    string[] cfg = File.ReadAllLines(fullPath);
                    string oldLine = "seta sensitivity \"" + currentSens + "\"";
                    string newLine = "seta sensitivity \"" + sens + "\"";
                    cfg[lineToReplace - 1] = newLine;
                    File.WriteAllLines(fullPath, cfg);
                    lineToReplace = 0; //reset line count before reading the next cfg
                }
            }
        }

        public void WriteToRust(string sens) {
            //example sens C:\Program Files (x86)\Steam\steamapps\common\Rust\cfg\client.cfg input.sensitivity "0.68"

            string fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\Rust\cfg\client.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            try {
                //find the sensitivity line, eg. input.sensitivity "0.68"
                using (StreamReader sr = new StreamReader(fullPath)) {
                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        lineToReplace++;

                        if (line.StartsWith("input.sensitivity ")) {

                            int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                            if (line.IndexOf('i') == index) {

                                int iFrom = line.IndexOf("input.sensitivity \"") + "input.sensitivity \"".Length;
                                int iTo = line.LastIndexOf("\"");
                                string result = line.Substring(iFrom, iTo - iFrom);
                                currentSens = result;
                            }
                            break;
                        }
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            if (currentSens != "") {
                //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                string[] cfg = File.ReadAllLines(fullPath);
                string oldLine = "input.sensitivity \"" + currentSens + "\"";
                string newLine = "input.sensitivity \"" + sens + "\"";
                cfg[lineToReplace - 1] = newLine;
                File.WriteAllLines(fullPath, cfg);
            }
        }

        public void WriteToSmite(string sens) {

            //example full path C:\Users\matej\Documents\My Games\Smite\BattleGame\Config\BattleInput.ini  mouse_sensitivity "2.957857"

            string userPath = @"C:\Users";
            string cfgPath = @"\Documents\My Games\Smite\BattleGame\Config\BattleInput.ini";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            string [] users = { };

            try {
                //there can be multiple users with smite...
                users = Directory.GetDirectories(userPath);
            }
            catch (DirectoryNotFoundException) { //if there aren't any users then catch and return
                return;
            }
            //...loop through each account folder
            foreach (string user in users) {
                string fullPath = user + cfgPath;

                try {
                    //find the sensitivity line, eg. sensitivity "1.633133"
                    using (StreamReader sr = new StreamReader(fullPath)) {
                        string line;

                        while ((line = sr.ReadLine()) != null) {

                            lineToReplace++;

                            if (line.StartsWith("MouseSensitivity=")) {

                                int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                                if (line.IndexOf('M') == index) {

                                    int iFrom = line.IndexOf("MouseSensitivity=") + "MouseSensitivity=".Length;
                                    int iTo = line.Length;
                                    string result = line.Substring(iFrom, iTo - iFrom);
                                    currentSens = result;
                                }
                                break;
                            }
                        }
                    }
                }
                catch { //keep looping throught the users even if a user doesn't have smite installed
                }
                try {
                    if (currentSens != "") {
                        //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                        string[] cfg = File.ReadAllLines(fullPath);
                        string oldLine = "MouseSensitivity=" + currentSens;
                        string newLine = "MouseSensitivity=" + sens;
                        cfg[lineToReplace - 1] = newLine;
                        File.WriteAllLines(fullPath, cfg);
                        lineToReplace = 0; //reset line count before reading the next cfg
                    }
                }
                catch { //ignore if smite not installed for other users
                }
            }
        }

        public void WriteToTF2(string sens) {

            //example full path C:\Program Files (x86)\Steam\steamapps\common\Team Fortress 2\tf\cfg\config.cfg sensitivity "5.5555"

            string fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\Team Fortress 2\tf\cfg\config.cfg";
            string currentSens = "";
            int lineToReplace = 0; //the line number to replace, eg. replace (sensitivity "1") not sv_holdrotation(sensitivity "1")

            try {
                //find the sensitivity line, eg. sensitivity "1.633133"
                using (StreamReader sr = new StreamReader(fullPath)) {
                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        lineToReplace++;

                        if (line.StartsWith("sensitivity ")) {

                            int index = 0; //avoid something like sv_holdrotationsensitivity "3.33"
                            if (line.IndexOf('s') == index) {

                                int iFrom = line.IndexOf("sensitivity \"") + "sensitivity \"".Length;
                                int iTo = line.LastIndexOf("\"");
                                string result = line.Substring(iFrom, iTo - iFrom);
                                currentSens = result;
                            }
                            break;
                        }
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            if (currentSens != "") {
                //naive way of replacing the line (otherwise StreamReader with line count => StreamWriter .WriteLine(count))
                string[] cfg = File.ReadAllLines(fullPath);
                string oldLine = "sensitivity \"" + currentSens + "\"";
                string newLine = "sensitivity \"" + sens + "\"";
                cfg[lineToReplace - 1] = newLine;
                File.WriteAllLines(fullPath, cfg);
            }
        }



    }
}
