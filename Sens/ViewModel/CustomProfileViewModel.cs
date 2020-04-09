using System;
using Sens.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using Sens.Utility;
using System.Text.RegularExpressions;
using System.IO;

namespace Sens.ViewModel {

    /// <summary>
    /// ViewModel for CustomProfile View. Handles user input. Loads the current Profile into CustomProfile
    /// UI form. Upon 'Save', overwrites any user input in the DB Profile table. Also halndles validation
    /// for user input. There is only one empty instance of CustomProfile initiated. The Profile is determined on
    /// the current variable in the DB
    ///
    /// /current int variable (0=false, 1=true) which controls which Profile is currently being in context. Based on 
    /// user input/subsequent functionality, it flags one Profile(record in the DB) which is read into
    /// the Custom Profile form or then updated based on user input in the CustomProfile form.
    /// At any given point there is only one record where current =1 or zero if a new entry is being added.
    ///
    /// Upon 'Save' sends a Message to MyProfilesViewModel to:
    /// Generate a new Profile (Visibility adjustnment, dynamically set the Text (TextBlock) and Background (Border Background) 
    /// for this generated Profile from user input @CustomProfile View.
    /// 
    /// TODO:
    /// Button macro
    /// </summary>

    public class CustomProfileViewModel : BaseViewModel {

        /*FIELDS*/

        #region Construction functionality fields
        private readonly Window window;

        private RelayCommand closeCommand;
        private RelayCommand saveCommand;

        private List<ProfileModel> profiles = new List<ProfileModel>();

        private bool popupVisibility = true;
        #endregion Construction functionality fields

        #region Storing user input data
        private String profileName;
        private String profileBackground;
        private String buttonMacro;
        private String sensitivityFrom = "Apex Legends"; //default ComboBox value (don't have to validate)
        private String sensitivity;
        private String dpi;

        private bool apex;
        private bool csgo;
        private bool cs16;
        private bool css;
        private bool gmod;
        private bool hl2;
        private bool l4d2;
        private bool minecraft;
        private bool paladins;
        private bool quake;
        private bool rust;
        private bool smite;
        private bool tf2;
        private bool portal2;

        private string boundary; //differnt sensitivity boundaries for different games

        #endregion Storing user input data

        private string filepath; //filepath for loading a custom Profile from a txt file

        /*CONSTRUCTOR*/

        public CustomProfileViewModel(Window window) {
            this.window = window;
            CloseCommand = new RelayCommand(() => CloseWindow());
            ClosePopupCommand = new RelayCommand(() => ClosePopup());
            ImageFileCommand = new RelayCommand(() => ChooseImageFilePath());
            ConfigFileCommand = new RelayCommand(() => PopulateForm(ChooseConfigFilePath()));
            LoadForm();
        }

        /*PROPERTIES*/

        #region Save command + message sending
        public RelayCommand SaveCommand {
            get {
                return saveCommand ?? (saveCommand = new RelayCommand(() =>
                {
                    //Check if Sensitivity and DPI fields are valid
                    if (!SensitivityValid() || !DPIValid()) {
                        //open validation popup
                        ValidationPopup vp = new ValidationPopup();
                        vp.ShowDialog();
                        return;
                    }

                    //else valid Update to DB
                    WriteProfileToDB();

                    // Send a message to MyProfilesVM to update Visibility, ProfileName and ProfileBackground, functionality handled in MyProfilesViewModel
                    Messenger.Default.Send<CustomProfileVMmsg>(new CustomProfileVMmsg
                    {
                        Message = "Update from CustomProfileVM",
                        ProfileName = ProfileName,
                        ProfileBackground = ProfileBackground,
                        ButtonMacro = ButtonMacro
                    }) ;
                }));
            }
        }
        #endregion Save command + message sending

        #region Window design properties
        //shadow around the window to distinguish from the main window
        public int OuterWindowMargin { get; set; } = 5;
        public Thickness OuterWindowMarginThickness { get { return new Thickness(OuterWindowMargin); } }

        //round window edges
        public int WindowRadius { get; set; } = 3;
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        //height of the title bar; draggable area= draggable area - outerWindowMargin
        public int CaptionBarHeight { get; set; } = 31;
        public GridLength CaptionBarGridHeight { get { return new GridLength(CaptionBarHeight - OuterWindowMargin); } }

        //inner content padding
        public int InnerPadding { get; set; } = 6;
        public Thickness InnerContentPadding { get { return new Thickness(InnerPadding); } }

        #endregion window design properties

        #region Populating combobox
        //sensitivity from combobox
        public List<String> GamesCollection { get; } = new List<string>
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

        #endregion Populating combobox

        #region Reading data from user input (TextBoxes+ComboBox)
        public string ProfileName {
            get {
                return profileName;
            }
            set {
                profileName = value;
                OnPropertyChanged("ProfileName");
            }
        }
        public string ProfileBackground {
            get {
                return profileBackground;
            }
            set {
                profileBackground = value;
                OnPropertyChanged("ProfileBackground");
                OnPropertyChanged("ProfileName");
            }
        }
        public string ButtonMacro {
            get {
                return buttonMacro;
            }
            set {
                buttonMacro = value;
                OnPropertyChanged("ButtonMacro");
            }
        }
        public string SensitivityFrom {
            get {
                return sensitivityFrom;
            }
            set {
                sensitivityFrom = value;
                OnPropertyChanged("SensitivityFrom");
                OnPropertyChanged("SaveCommand");
                OnPropertyChanged("Boundary");
            }
        }
        public string Sensitivity {
            get {
                return sensitivity;
            }
            set {
                sensitivity = value;
                OnPropertyChanged("Sensitivity");
                OnPropertyChanged("Boundary");
                OnPropertyChanged("SensitivityFrom");
                OnPropertyChanged("SaveCommand");
            }
        }
        public string DPI {
            get {
                return dpi;
            }
            set {
                dpi = value;
                OnPropertyChanged("DPI");
            }
        }
        #endregion #region Reading from user input data (TextBoxes+ComboBox)

        #region Reading data from user input (CheckBoxes)

        public bool Apex {
            get {
                return apex;
            }
            set {
                apex = value;
                OnPropertyChanged("Apex");
            }
        }
        public bool Csgo {
            get {
                return csgo;
            }
            set {
                csgo = value;
                OnPropertyChanged("Csgo");
            }
        }
        public bool Cs16 {
            get {
                return cs16;
            }
            set {
                cs16 = value;
                OnPropertyChanged("Cs16");
            }
        }
        public bool Css {
            get {
                return css;
            }
            set {
                css = value;
                OnPropertyChanged("Css");
            }
        }
        public bool Gmod {
            get {
                return gmod;
            }
            set {
                gmod = value;
                OnPropertyChanged("Gmod");
            }
        }
        public bool Hl2 {
            get {
                return hl2;
            }
            set {
                hl2 = value;
                OnPropertyChanged("Hl2");
            }
        }
        public bool L4d2 {
            get {
                return l4d2;
            }
            set {
                l4d2 = value;
                OnPropertyChanged("L4d2");
            }
        }
        public bool Minecraft {
            get {
                return minecraft;
            }
            set {
                minecraft = value;
                OnPropertyChanged("Minecraft");
            }
        }
        public bool Paladins {
            get {
                return paladins;
            }
            set {
                paladins = value;
                OnPropertyChanged("Paladins");
            }
        }
        public bool Quake {
            get {
                return quake;
            }
            set {
                quake = value;
                OnPropertyChanged("Quake");
            }
        }
        public bool Rust {
            get {
                return rust;
            }
            set {
                rust = value;
                OnPropertyChanged("Rust");
            }
        }
        public bool Smite {
            get {
                return smite;
            }
            set {
                smite = value;
                OnPropertyChanged("Smite");
            }
        }
        public bool Tf2 {
            get {
                return tf2;
            }
            set {
                tf2 = value;
                OnPropertyChanged("Tf2");
            }
        }
        public bool Portal2 {
            get {
                return portal2;
            }
            set {
                portal2 = value;
                OnPropertyChanged("Portal2");
            }
        }
        #endregion Reading from user input data (CheckBoxes)

        #region String Boundaries for Popup Validation
        public string Boundary {
            get {
                return boundary = profileName;
            }
            set {
                boundary = value;
                OnPropertyChanged("Boundary");
                OnPropertyChanged("SensitivityFrom");
                OnPropertyChanged("SaveCommand");
                OnPropertyChanged("ProfileName");
            }
        }

        #endregion String Boundaries for Popup Validation

        #region File Path
        public string FilePath {
            get {
                return filepath;
            }
            set {
                filepath = value;
                OnPropertyChanged("FilePath");
            }
        }
        #endregion File Path

        /*COMMANDS*/

        #region General Commands

        public ICommand CloseCommand { get; set; }
        public ICommand ClosePopupCommand { get; set; }
        public ICommand ImageFileCommand { get; set; }
        public ICommand ConfigFileCommand { get; set; }

        #endregion General Commands

        /*METHODS*/

        #region Close Window
        //closes the form
        private void CloseWindow() {
            SQLiteDataAccess.ResetAllCurrentFields();
            window.Close();
        }

        //closes the Popup
        public void ClosePopup() {
            window.Close();
        }
        #endregion Close Window

        #region Load current Profile from DB
        private void LoadForm() {

            //pull a list from the DB where current=1 (at this point, validation ensures there's exactly 1 or 0 ProfileModel instances in the list) 
            List<ProfileModel> currentProfileList = SQLiteDataAccess.LoadCurrentProfile();

            //if empty, no current => new dial = dont populate
            if (currentProfileList.Count == 0) {
                return;
            }

            //else there is only 1 entry in the list
            ProfileModel currentProfile = currentProfileList[0];

            //Populate the form user 'currentProfile' data
            ProfileName = currentProfile.ProfileName;
            ProfileBackground = currentProfile.BackgroundURL;
            ButtonMacro = currentProfile.ButtonMacro;
            SensitivityFrom = currentProfile.SensitivityFrom;
            Sensitivity = currentProfile.Sensitivity;
            DPI = currentProfile.DPI;

            Apex = (currentProfile.Apex == 1) ? true : false;
            Csgo = (currentProfile.CSGO == 1) ? true : false;
            Cs16 = (currentProfile.CS16 == 1) ? true : false;
            Css = (currentProfile.CSS == 1) ? true : false;
            Gmod = (currentProfile.GMod == 1) ? true : false;
            Hl2 = (currentProfile.HL2 == 1) ? true : false;
            L4d2 = (currentProfile.L4D2 == 1) ? true : false;
            Minecraft = (currentProfile.Minecraft == 1) ? true : false;
            Paladins = (currentProfile.Paladins == 1) ? true : false;
            Quake = (currentProfile.Quake == 1) ? true : false;
            Rust = (currentProfile.Rust == 1) ? true : false;
            Smite = (currentProfile.Smite == 1) ? true : false;
            Tf2 = (currentProfile.TF2 == 1) ? true : false;
            Portal2 = (currentProfile.Portal2 == 1) ? true : false;
        }
        #endregion Load current Profile from DB

        #region Validation for Sensitivity and DPI
        private bool SensitivityValid() {
            double output = 0;
            //if Sensitivity is a double
            if (Double.TryParse(Sensitivity, out output)) {
                //convert it to a double
                double sensDouble = Double.Parse(Sensitivity);

                //evaluate sens based on each game

                if (SensitivityFrom == "Apex Legends") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 1, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0.2 && roundedSens <= 20) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "CS:GO") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 2, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0 && roundedSens <= 8) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "CS 1.6") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 1, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0.2 && roundedSens <= 20) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "CS Source") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 2, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0.10 && roundedSens <= 6) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Fortnite") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 3, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0 && roundedSens <= 1) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Garry's Mod") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 2, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0.10 && roundedSens <= 6) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Half Life 2") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 2, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0.10 && roundedSens <= 6) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Left 4 Dead 2") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 3, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 1 && roundedSens <= 20) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Minecraft") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 0, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 1 && roundedSens <= 199) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Overwatch") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 2, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0.01 && roundedSens <= 100) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Paladins") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 1, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 1 && roundedSens <= 100) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Quake Live") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 2, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0.01 && roundedSens <= 30) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Rust") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 1, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0 && roundedSens <= 2) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Smite") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 0, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 1 && roundedSens <= 100) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Team Fortress 2") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 2, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0.10 && roundedSens <= 6) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "The Outer Worlds") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 1, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 0 && roundedSens <= 100) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Warframe") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 0, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 1 && roundedSens <= 100) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
                else if (SensitivityFrom == "Portal 2") {
                    //round value
                    double roundedSens = Math.Round(sensDouble, 6, MidpointRounding.AwayFromZero);
                    if (roundedSens >= 1 && roundedSens <= 20) {
                        //save the rounded value
                        Sensitivity = roundedSens.ToString();
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DPIValid() {
            double output = 0;
            //if DPI is a double
            if (Double.TryParse(DPI, out output)) {
                //convert DPI to double
                double doubleDPI = Double.Parse(DPI);
                //round double to an int (0dp)
                int intDPI = (int)Math.Round(doubleDPI, 0, MidpointRounding.AwayFromZero);
                //and if DPI is between 1 and 16000
                if (intDPI > 0 && intDPI <= 16000) {
                    //DPI is valid and overwrite with rounded value
                    DPI = intDPI.ToString();
                    return true;
                }
            }
            //otherwise everything else is invalid
            return false;
        }
        #endregion Validation for Sensitivity and DPI

        #region Write to DB upon 'Save'
        //on 'Save' load all fields into a 'ProfileModel'
        private void WriteProfileToDB() {

            //populate the ProfileModel
            ProfileModel profile = new ProfileModel
            {
                ProfileName = profileName,
                BackgroundURL = profileBackground,
                ButtonMacro = buttonMacro,
                SensitivityFrom = sensitivityFrom,
                Sensitivity = sensitivity,
                DPI = dpi,

                //convert bool to int (SQLite can't handle bool)
                Apex = (apex) ? 1 : 0,
                CSGO = (csgo) ? 1 : 0,
                CS16 = (cs16) ? 1 : 0,
                CSS = (css) ? 1 : 0,
                GMod = (gmod) ? 1 : 0,
                HL2 = (hl2) ? 1 : 0,
                L4D2 = (l4d2) ? 1 : 0,
                Minecraft = (minecraft) ? 1 : 0,
                Paladins = (paladins) ? 1 : 0,
                Quake = (quake) ? 1 : 0,
                Rust = (rust) ? 1 : 0,
                Smite = (smite) ? 1 : 0,
                TF2 = (tf2) ? 1 : 0,
                Portal2 = (portal2) ? 1 : 0
            };

            //load the current profile (WHERE current=1) 
            List<ProfileModel> currentProfileList = SQLiteDataAccess.LoadCurrentProfile();

            //if empty, it is 'New Speed Dial' rather than 'Edit'
            if (currentProfileList.Count == 0) {

                //adjust ID to incremental manually, even thought it's set to incremental in DB just doesn't work :)

                //grab the most recent ID +1
                int mostRecentID;
                List<int> allCurrentIDs = SQLiteDataAccess.AllIDs();
                if (allCurrentIDs.Count == 0) {
                    mostRecentID = 0;
                }
                else {
                    mostRecentID = allCurrentIDs.Last();
                }
                int newID = mostRecentID + 1;

                //update record with new ID
                profile.ID = newID;

                //set current = 1 as it will be the current instance opened
                profile.current = 1;

                //add this Profile as a new record
                SQLiteDataAccess.WriteNewProfile(profile);

                return;
            }

            //otherwise there is a current profile(Edit has been clicked rather than New Dial) therefore overwrite the current Profile
            SQLiteDataAccess.WriteEditedProfile(profile);
        }
        #endregion Write to DB upon 'Save'

        #region Retrieve a user defined image path for Profile Background
        private void ChooseImageFilePath() {

            //create create OpenFileDialog obj
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            //default directory on open
            dialog.InitialDirectory = "c:\\";
            //set the default file extension
            dialog.DefaultExt = ".png";
            //only show image files
            dialog.Filter = "Image files |*.jpg;*.jpeg;*.tif;*.tiff;*.png";
            //save the most recent directory 
            dialog.RestoreDirectory = true;

            //display dialog
            Nullable<bool> result = dialog.ShowDialog();

            //if a text file is chosen
            if (result.HasValue && result.Value) {
                //read the filepath
                string filepath = dialog.FileName;
                ProfileBackground = filepath;
            }
        }
        #endregion Retrieve a user defined image path for Profile Background

        #region Populating the form from a user config
        //Retrieve config file path
        private string ChooseConfigFilePath() {

            //create create OpenFileDialog obj
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            //default directory on open
            dialog.InitialDirectory = "c:\\";
            //set the default file extension
            dialog.DefaultExt = ".txt";
            //only show image files
            dialog.Filter = "Text file |*.txt";
            //save the most recent directory 
            dialog.RestoreDirectory = true;

            //display dialog
            Nullable<bool> result = dialog.ShowDialog();

            //if a text file is chosen
            if (result.HasValue && result.Value) {
                //read the filepath
                string filepath = dialog.FileName;

                return filepath;
            }
            return "noFileChosen";
        }

        //Populate form
        private void PopulateForm(String path) {
            if (path.Equals("noFileChosen")) {
                return;
            }
            else {
                try {
                    //open text file using streamer
                    using (StreamReader sr = new StreamReader(path)) {
                        //for each line
                        String line;
                        while ((line = sr.ReadLine())!=null) {
                            //check lines containing specific keys (Form Properties)
                            if (line.Contains("ProfileName #")) {

                                int iFrom = line.IndexOf("ProfileName #") + "ProfileName #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                ProfileName = result;

                            }
                            if (line.Contains("Background #")) {

                                int iFrom = line.IndexOf("Background #") + "Background #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                ProfileBackground = result;
                            }
                            if (line.Contains("KeyPressMacro #")) {

                                int iFrom = line.IndexOf("KeyPressMacro #") + "KeyPressMacro #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                ButtonMacro = result;
                            }
                            if (line.Contains("SensitivityFrom #")) {
                                //if none from the list then apex as default
                                int iFrom = line.IndexOf("SensitivityFrom #") + "SensitivityFrom #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("Apex Legends") || result.Equals("CS:GO") || result.Equals("CS 1.6") || result.Equals("CS Source") || result.Equals("Fortnite") || result.Equals("Garry's Mod") ||
                                    result.Equals("Half Life 2") || result.Equals("Left 4 Dead 2") || result.Equals("Minecraft") || result.Equals("Overwatch") || result.Equals("Paladins") || result.Equals("Quake Live") ||
                                    result.Equals("Rust") || result.Equals("Smite") || result.Equals("Team Fortress 2") || result.Equals("The Outer Worlds") || result.Equals("Warframe") || result.Equals("Portal 2")) {
                                    SensitivityFrom = result;
                                }
                                else {
                                    SensitivityFrom = "Apex Legends";
                                }
                            }
                            if (line.Contains("Sensitivity #")) {

                                int iFrom = line.IndexOf("Sensitivity #") + "Sensitivity #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                Sensitivity = result;
                            }
                            if (line.Contains("DPI #")) {

                                int iFrom = line.IndexOf("DPI #") + "DPI #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                DPI = result;
                            }
                            if (line.Contains("Apex Legends #")) {

                                int iFrom = line.IndexOf("Apex Legends #") + "Apex Legends #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Apex = true;
                                }
                                else {
                                    Apex = false;
                                }
                            }
                            if (line.Contains("CS:GO #")) {

                                int iFrom = line.IndexOf("CS:GO #") + "CS:GO #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Csgo = true;
                                }
                                else {
                                    Csgo = false;
                                }
                            }
                            if (line.Contains("CS 1.6 #")) {

                                int iFrom = line.IndexOf("CS 1.6 #") + "CS 1.6 #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Cs16 = true;
                                }
                                else {
                                    Cs16 = false;
                                }
                            }
                            if (line.Contains("CS Source #")) {

                                int iFrom = line.IndexOf("CS Source #") + "CS Source #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Css = true;
                                }
                                else {
                                    Css = false;
                                }
                            }
                            if (line.Contains("Garry's Mod #")) {

                                int iFrom = line.IndexOf("Garry's Mod #") + "Garry's Mod #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Gmod = true;
                                }
                                else {
                                    Gmod = false;
                                }
                            }
                            if (line.Contains("Half Life 2 #")) {

                                int iFrom = line.IndexOf("Half Life 2 #") + "Half Life 2 #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Hl2 = true;
                                }
                                else {
                                    Hl2 = false;
                                }
                            }
                            if (line.Contains("Left 4 Dead 2 #")) {

                                int iFrom = line.IndexOf("Left 4 Dead 2 #") + "Left 4 Dead 2 #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    L4d2 = true;
                                }
                                else {
                                    L4d2 = false;
                                }
                            }
                            if (line.Contains("Minecraft #")) {

                                int iFrom = line.IndexOf("Minecraft #") + "Minecraft #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Minecraft = true;
                                }
                                else {
                                    Minecraft = false;
                                }
                            }
                            if (line.Contains("Paladins #")) {

                                int iFrom = line.IndexOf("Paladins #") + "Paladins #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Paladins = true;
                                }
                                else {
                                    Paladins = false;
                                }
                            }
                            if (line.Contains("Quake Live #")) {

                                int iFrom = line.IndexOf("Quake Live #") + "Quake Live #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Quake = true;
                                }
                                else {
                                    Quake = false;
                                }
                            }
                            if (line.Contains("Rust #")) {

                                int iFrom = line.IndexOf("Rust #") + "Rust #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Rust = true;
                                }
                                else {
                                    Rust = false;
                                }
                            }
                            if (line.Contains("Smite #")) {

                                int iFrom = line.IndexOf("Smite #") + "Smite #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Smite = true;
                                }
                                else {
                                    Smite = false;
                                }
                            }
                            if (line.Contains("Team Fortress #")) {

                                int iFrom = line.IndexOf("Team Fortress #") + "Team Fortress #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Tf2 = true;
                                }
                                else {
                                    Tf2 = false;
                                }
                            }
                            if (line.Contains("Portal 2 #")) {

                                int iFrom = line.IndexOf("Portal 2 #") + "Portal 2 #".Length;
                                int iTo = line.LastIndexOf("#.");

                                string result = line.Substring(iFrom, iTo - iFrom);
                                if (result.Equals("1")) {
                                    Portal2 = true;
                                }
                                else {
                                    Portal2 = false;
                                }
                            }
                        }
                    }
                }
                catch (IOException e) {
                    Console.WriteLine("File could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion Populating the form from a user config

    }
}
