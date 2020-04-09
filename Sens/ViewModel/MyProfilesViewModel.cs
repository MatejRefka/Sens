using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sens.DataModels;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Sens.Utility;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using Sens.Services;

namespace Sens.ViewModel {

    /// <summary>
    /// A ViewModel controlling MyProfilesPage View. Handles Profiles Speed Dial like control (visibility) based on updates from 
    /// the database and one call from the CustomProfile View ('Save' button click) using a message/s to trigger events. 
    /// 
    /// Events upon 'Save':
    /// Generate a new Profile (Visibility adjustnment, dynamically set the Text (TextBlock) and Background (Border Background) 
    /// for this generated Profile from user input @CustomProfile View.
    /// 
    /// Events upon 'Email':
    /// Sends an auto generated email to the user, with a config text file attached. This config is appended in a way that a user 
    /// can then load it back through the CustomProfile form.
    /// TODO: 
    /// APPLY
    /// </summary>

    public class MyProfilesViewModel : BaseViewModel {

        /*FIELDS*/

        #region Interface variables (IoC)
        private ICalculator calculator;
        private ICalculator2D calculator2D;
        private IWriteToCFG cfgWriter;
        #endregion Interface variables (IoC)

        #region Speed Dial and New Speed Dial visibility
        private bool sD1Visibility;
        private bool sD2Visibility;
        private bool sD3Visibility;
        private bool sD4Visibility;
        private bool sD5Visibility;
        private bool sD6Visibility;
        private bool sD7Visibility;
        private bool sD8Visibility;
        private bool sD9Visibility;

        private bool nSD1Visibility;
        private bool nSD2Visibility;
        private bool nSD3Visibility;
        private bool nSD4Visibility;
        private bool nSD5Visibility;
        private bool nSD6Visibility;
        private bool nSD7Visibility;
        private bool nSD8Visibility;
        private bool nSD9Visibility;

        #endregion Speed Dial and New Speed Dial visibility

        #region Profile Names, one for each Profile

        private string profile1 = "";
        private string profile2 = "";
        private string profile3 = "";
        private string profile4 = "";
        private string profile5 = "";
        private string profile6 = "";
        private string profile7 = "";
        private string profile8 = "";
        private string profile9 = "";

        #endregion Profile Names, one for each Profile

        #region Profile Background path, one for each Profile

        private string background1;
        private string background2;
        private string background3;
        private string background4;
        private string background5;
        private string background6;
        private string background7;
        private string background8;
        private string background9;

        #endregion Profile Background URL, one for each Profile

        /*PROPERTIES*/

        #region ProfileName Properties for the 9 default Speed Dials
        public string ProfileName1 {
            get {
                return profile1;
            }
            set {
                if (value == null) {
                    profile1 = "";
                } else
                    profile1 = value;
                OnPropertyChanged("ProfileName1");
            }
        }
        public string ProfileName2 {
            get {
                return profile2;
            }
            set {
                if (value == null) {
                    profile2 = "";
                } else
                    profile2 = value;
                OnPropertyChanged("ProfileName2");
            }
        }
        public string ProfileName3 {
            get {
                return profile3;
            }
            set {
                if (value == null) {
                    profile3 = "";
                } else
                    profile3 = value;
                OnPropertyChanged("ProfileName3");
            }
        }
        public string ProfileName4 {
            get {
                return profile4;
            }
            set {
                if (value == null) {
                    profile4 = "";
                }
                else
                    profile4 = value;
                OnPropertyChanged("ProfileName4");
            }
        }
        public string ProfileName5 {
            get {
                return profile5;
            }
            set {
                if (value == null) {
                    profile5 = "";
                }
                else
                    profile5 = value;
                OnPropertyChanged("ProfileName5");
            }
        }
        public string ProfileName6 {
            get {
                return profile6;
            }
            set {
                if (value == null) {
                    profile6 = "";
                }
                else
                    profile6 = value;
                OnPropertyChanged("ProfileName6");
            }
        }
        public string ProfileName7 {
            get {
                return profile7;
            }
            set {
                if (value == null) {
                    profile7 = "";
                }
                else
                    profile7 = value;
                OnPropertyChanged("ProfileName7");
            }
        }
        public string ProfileName8 {
            get {
                return profile8;
            }
            set {
                if (value == null) {
                    profile8 = "";
                }
                else
                    profile8 = value;
                OnPropertyChanged("ProfileName8");
            }
        }
        public string ProfileName9 {
            get {
                return profile9;
            }
            set {
                if (value == null) {
                    profile9 = "";
                }
                else
                    profile9 = value;
                OnPropertyChanged("ProfileName9");
            }
        }
        #endregion ProfileName Properties for the 9 default Speed Dials

        #region ProfileBackground Properties for the 9 deafult Speed Dials
        public string ProfileBackground1 {
            get {
                return background1;
            }
            set {
                background1 = value;
                OnPropertyChanged("ProfileBackground1");
            }
        }
        public string ProfileBackground2 {
            get {
                return background2;
            }
            set {
                background2 = value;
                OnPropertyChanged("ProfileBackground2");
            }
        }
        public string ProfileBackground3 {
            get {
                return background3;
            }
            set {
                background3 = value;
                OnPropertyChanged("ProfileBackground3");
            }
        }
        public string ProfileBackground4 {
            get {
                return background4;
            }
            set {
                background4 = value;
                OnPropertyChanged("ProfileBackground4");
            }
        }
        public string ProfileBackground5 {
            get {
                return background5;
            }
            set {
                background5 = value;
                OnPropertyChanged("ProfileBackground5");
            }
        }
        public string ProfileBackground6 {
            get {
                return background6;
            }
            set {
                background6 = value;
                OnPropertyChanged("ProfileBackground6");
            }
        }
        public string ProfileBackground7 {
            get {
                return background7;
            }
            set {
                background7 = value;
                OnPropertyChanged("ProfileBackground7");
            }
        }
        public string ProfileBackground8 {
            get {
                return background8;
            }
            set {
                background8 = value;
                OnPropertyChanged("ProfileBackground8");
            }
        }
        public string ProfileBackground9 {
            get {
                return background9;
            }
            set {
                background9 = value;
                OnPropertyChanged("ProfileBackground9");
            }
        }
        #endregion ProfileBackground Properties for the 9 deafult Speed Dials

        #region Visibility Properties for the 9 default Speed Dials
        public bool SD1Visibility {
            get {
                return sD1Visibility;
            }
            set {
                sD1Visibility = value;
                OnPropertyChanged("SD1Visibility");
            }
        }
        public bool SD2Visibility {
            get {
                return sD2Visibility;
            }
            set {
                sD2Visibility = value;
                OnPropertyChanged("SD2Visibility");
            }
        }
        public bool SD3Visibility {
            get {
                return sD3Visibility;
            }
            set {
                sD3Visibility = value;
                OnPropertyChanged("SD3Visibility");
            }
        }
        public bool SD4Visibility {
            get {
                return sD4Visibility;
            }
            set {
                sD4Visibility = value;
                OnPropertyChanged("SD4Visibility");
            }
        }
        public bool SD5Visibility {
            get {
                return sD5Visibility;
            }
            set {
                sD5Visibility = value;
                OnPropertyChanged("SD5Visibility");
            }
        }
        public bool SD6Visibility {
            get {
                return sD6Visibility;
            }
            set {
                sD6Visibility = value;
                OnPropertyChanged("SD6Visibility");
            }
        }
        public bool SD7Visibility {
            get {
                return sD7Visibility;
            }
            set {
                sD7Visibility = value;
                OnPropertyChanged("SD7Visibility");
            }
        }
        public bool SD8Visibility {
            get {
                return sD8Visibility;
            }
            set {
                sD8Visibility = value;
                OnPropertyChanged("SD8Visibility");
            }
        }
        public bool SD9Visibility {
            get {
                return sD9Visibility;
            }
            set {
                sD9Visibility = value;
                OnPropertyChanged("SD9Visibility");
            }
        }
        #endregion Visibility Properties for the 9 default Speed Dials

        #region Visibility Properties for the 9 New Speed Dial Buttons

        public bool NSD1Visibility {
            get {
                return nSD1Visibility;
            }
            set {
                nSD1Visibility = value;
                OnPropertyChanged("NSD1Visibility");
            }
        }
        public bool NSD2Visibility {
            get {
                return nSD2Visibility;
            }
            set {
                nSD2Visibility = value;
                OnPropertyChanged("NSD2Visibility");
            }
        }
        public bool NSD3Visibility {
            get {
                return nSD3Visibility;
            }
            set {
                nSD3Visibility = value;
                OnPropertyChanged("NSD3Visibility");
            }
        }
        public bool NSD4Visibility {
            get {
                return nSD4Visibility;
            }
            set {
                nSD4Visibility = value;
                OnPropertyChanged("NSD4Visibility");
            }
        }
        public bool NSD5Visibility {
            get {
                return nSD5Visibility;
            }
            set {
                nSD5Visibility = value;
                OnPropertyChanged("NSD5Visibility");
            }
        }
        public bool NSD6Visibility {
            get {
                return nSD6Visibility;
            }
            set {
                nSD6Visibility = value;
                OnPropertyChanged("NSD6Visibility");
            }
        }
        public bool NSD7Visibility {
            get {
                return nSD7Visibility;
            }
            set {
                nSD7Visibility = value;
                OnPropertyChanged("NSD7Visibility");
            }
        }
        public bool NSD8Visibility {
            get {
                return nSD8Visibility;
            }
            set {
                nSD8Visibility = value;
                OnPropertyChanged("NSD8Visibility");
            }
        }
        public bool NSD9Visibility {
            get {
                return nSD9Visibility;
            }
            set {
                nSD9Visibility = value;
                OnPropertyChanged("NSD9Visibility");
            }
        }


        #endregion Visibility Properties for the 9 New Speed Dial Buttons

        /*COMMANDS*/

        #region Profile - Close Buttons
        public ICommand CloseProfile1Command { get; set; }
        public ICommand CloseProfile2Command { get; set; }
        public ICommand CloseProfile3Command { get; set; }
        public ICommand CloseProfile4Command { get; set; }
        public ICommand CloseProfile5Command { get; set; }
        public ICommand CloseProfile6Command { get; set; }
        public ICommand CloseProfile7Command { get; set; }
        public ICommand CloseProfile8Command { get; set; }
        public ICommand CloseProfile9Command { get; set; }
        #endregion Profile - Close Buttons

        #region Profile - Edit Buttons
        public ICommand EditFirstRecordCommand { get; set; }
        public ICommand EditSecondRecordCommand { get; set; }
        public ICommand EditThirdRecordCommand { get; set; }
        public ICommand EditFourthRecordCommand { get; set; }
        public ICommand EditFifthRecordCommand { get; set; }
        public ICommand EditSixthRecordCommand { get; set; }
        public ICommand EditSeventhRecordCommand { get; set; }
        public ICommand EditEighthRecordCommand { get; set; }
        public ICommand EditNinethRecordCommand { get; set; }
        #endregion Profile - Edit Buttons

        #region Profile - Email Buttons
        public ICommand EmailCommand1 { get; set; }
        public ICommand EmailCommand2 { get; set; }
        public ICommand EmailCommand3 { get; set; }
        public ICommand EmailCommand4 { get; set; }
        public ICommand EmailCommand5 { get; set; }
        public ICommand EmailCommand6 { get; set; }
        public ICommand EmailCommand7 { get; set; }
        public ICommand EmailCommand8 { get; set; }
        public ICommand EmailCommand9 { get; set; }
        #endregion Profile - Email Buttons

        #region Profile - Apply Buttons
        public ICommand ApplyCommand1 { get; set; }
        public ICommand ApplyCommand2 { get; set; }
        public ICommand ApplyCommand3 { get; set; }
        public ICommand ApplyCommand4 { get; set; }
        public ICommand ApplyCommand5 { get; set; }
        public ICommand ApplyCommand6 { get; set; }
        public ICommand ApplyCommand7 { get; set; }
        public ICommand ApplyCommand8 { get; set; }
        public ICommand ApplyCommand9 { get; set; }
        #endregion Profile - Apply Buttons

        /*CONSTRUCTOR*/
        public MyProfilesViewModel(ICalculator calculator, ICalculator2D calculator2D, IWriteToCFG cfgWriter) {

            #region Setting Parameters
            this.calculator = calculator;
            this.calculator2D = calculator2D;
            this.cfgWriter = cfgWriter;
            #endregion Setting Parameters

            #region Handle any Message (Messenger) from other VMs
            //message from CustomProfileVM, on close form and on save form, refresh the dial visibility and profile name + background
            Messenger.Default.Register<CustomProfileVMmsg>(this, CustomProfileMessage);
            #endregion Handle any Message (Messenger) from other VMs

            #region Profile - Close Buttons RelayCommand
            CloseProfile1Command = new RelayCommand(() => CloseProfile1());
            CloseProfile2Command = new RelayCommand(() => CloseProfile2());
            CloseProfile3Command = new RelayCommand(() => CloseProfile3());
            CloseProfile4Command = new RelayCommand(() => CloseProfile4());
            CloseProfile5Command = new RelayCommand(() => CloseProfile5());
            CloseProfile6Command = new RelayCommand(() => CloseProfile6());
            CloseProfile7Command = new RelayCommand(() => CloseProfile7());
            CloseProfile8Command = new RelayCommand(() => CloseProfile8());
            CloseProfile9Command = new RelayCommand(() => CloseProfile9());
            #endregion Profile - Close Buttons RelayCommand

            #region Profile - Edit Buttons RelayCommand
            EditFirstRecordCommand = new RelayCommand(() => EditFirstRecord());
            EditSecondRecordCommand = new RelayCommand(() => EditSecondRecord());
            EditThirdRecordCommand = new RelayCommand(() => EditThirdRecord());
            EditFourthRecordCommand = new RelayCommand(() => EditFourthRecord());
            EditFifthRecordCommand = new RelayCommand(() => EditFifthRecord());
            EditSixthRecordCommand = new RelayCommand(() => EditSixthRecord());
            EditSeventhRecordCommand = new RelayCommand(() => EditSeventhRecord());
            EditEighthRecordCommand = new RelayCommand(() => EditEighthRecord());
            EditNinethRecordCommand = new RelayCommand(() => EditNinethRecord());
            #endregion Profile - Close Buttons RelayCommand

            #region Profile - Email Buttons RelayCommand
            EmailCommand1 = new RelayCommand(() => EmailConfig(1));
            EmailCommand2 = new RelayCommand(() => EmailConfig(2));
            EmailCommand3 = new RelayCommand(() => EmailConfig(3));
            EmailCommand4 = new RelayCommand(() => EmailConfig(4));
            EmailCommand5 = new RelayCommand(() => EmailConfig(5));
            EmailCommand6 = new RelayCommand(() => EmailConfig(6));
            EmailCommand7 = new RelayCommand(() => EmailConfig(7));
            EmailCommand8 = new RelayCommand(() => EmailConfig(8));
            EmailCommand9 = new RelayCommand(() => EmailConfig(9));
            #endregion Profile - Email Buttons RelayCommand

            #region Profile - Apply Buttons RelayCommand
            ApplyCommand1 = new RelayCommand(() => cfgWriter.CalcCM3D(1));
            ApplyCommand2 = new RelayCommand(() => cfgWriter.CalcCM3D(2));
            ApplyCommand3 = new RelayCommand(() => cfgWriter.CalcCM3D(3));
            ApplyCommand4 = new RelayCommand(() => cfgWriter.CalcCM3D(4));
            ApplyCommand5 = new RelayCommand(() => cfgWriter.CalcCM3D(5));
            ApplyCommand6 = new RelayCommand(() => cfgWriter.CalcCM3D(6));
            ApplyCommand7 = new RelayCommand(() => cfgWriter.CalcCM3D(7));
            ApplyCommand8 = new RelayCommand(() => cfgWriter.CalcCM3D(8));
            ApplyCommand9 = new RelayCommand(() => cfgWriter.CalcCM3D(9));
            #endregion Profile - Apply Buttons RelayCommand

            #region Load ProfileName and ProfileBackground for each profile

            LoadProfileName();
            LoadProfileBackground();

            #endregion Load ProfileName and ProfileBackground for each profile

            #region Setup the Profile Speed Dial visibility
            //default visibility for all Profiles Speed Dials = hidden
            SD1Visibility = false;
            SD2Visibility = false;
            SD3Visibility = false;
            SD4Visibility = false;
            SD5Visibility = false;
            SD6Visibility = false;
            SD7Visibility = false;
            SD8Visibility = false;
            SD9Visibility = false;

            //toggles the visibility of specific Profile Dials based on the records in DB
            SDVisibilityControl();

            #endregion Setup the Profile Speed Dial visibility

            #region Setup the New Profile Speed Dial visibility
            //default visibility for all New Profiles Speed Dials = hidden
            NSD1Visibility = false;
            NSD2Visibility = false;
            NSD3Visibility = false;
            NSD4Visibility = false;
            NSD5Visibility = false;
            NSD6Visibility = false;
            NSD7Visibility = false;
            NSD8Visibility = false;
            NSD9Visibility = false;

            //toggles the visibility of specific New Profile Dials based on the records in DB
            NSDVisibilityControl();

            #endregion Setup the New Profile Speed Dial visibility
        }

        /*METHODS*/

        #region Load ProfileName for each profile upon startup
        private void LoadProfileName() {

            //populate each case independently, fixed names so can't automate this xdd
            List<ProfileModel> allProfiles = SQLiteDataAccess.LoadAllProfiles();
            if (allProfiles.Count == 9) {
                ProfileName1 = allProfiles[0].ProfileName;
                ProfileName2 = allProfiles[1].ProfileName;
                ProfileName3 = allProfiles[2].ProfileName;
                ProfileName4 = allProfiles[3].ProfileName;
                ProfileName5 = allProfiles[4].ProfileName;
                ProfileName6 = allProfiles[5].ProfileName;
                ProfileName7 = allProfiles[6].ProfileName;
                ProfileName8 = allProfiles[7].ProfileName;
                ProfileName9 = allProfiles[8].ProfileName;
            }
            else if (allProfiles.Count == 8) {
                ProfileName1 = allProfiles[0].ProfileName;
                ProfileName2 = allProfiles[1].ProfileName;
                ProfileName3 = allProfiles[2].ProfileName;
                ProfileName4 = allProfiles[3].ProfileName;
                ProfileName5 = allProfiles[4].ProfileName;
                ProfileName6 = allProfiles[5].ProfileName;
                ProfileName7 = allProfiles[6].ProfileName;
                ProfileName8 = allProfiles[7].ProfileName;
            }
            else if (allProfiles.Count == 7) {
                ProfileName1 = allProfiles[0].ProfileName;
                ProfileName2 = allProfiles[1].ProfileName;
                ProfileName3 = allProfiles[2].ProfileName;
                ProfileName4 = allProfiles[3].ProfileName;
                ProfileName5 = allProfiles[4].ProfileName;
                ProfileName6 = allProfiles[5].ProfileName;
                ProfileName7 = allProfiles[6].ProfileName;
            }
            else if (allProfiles.Count == 6) {
                ProfileName1 = allProfiles[0].ProfileName;
                ProfileName2 = allProfiles[1].ProfileName;
                ProfileName3 = allProfiles[2].ProfileName;
                ProfileName4 = allProfiles[3].ProfileName;
                ProfileName5 = allProfiles[4].ProfileName;
                ProfileName6 = allProfiles[5].ProfileName;
            }
            else if (allProfiles.Count == 5) {
                ProfileName1 = allProfiles[0].ProfileName;
                ProfileName2 = allProfiles[1].ProfileName;
                ProfileName3 = allProfiles[2].ProfileName;
                ProfileName4 = allProfiles[3].ProfileName;
                ProfileName5 = allProfiles[4].ProfileName;
            }
            else if (allProfiles.Count == 4) {
                ProfileName1 = allProfiles[0].ProfileName;
                ProfileName2 = allProfiles[1].ProfileName;
                ProfileName3 = allProfiles[2].ProfileName;
                ProfileName4 = allProfiles[3].ProfileName;
            }
            else if (allProfiles.Count == 3) {
                ProfileName1 = allProfiles[0].ProfileName;
                ProfileName2 = allProfiles[1].ProfileName;
                ProfileName3 = allProfiles[2].ProfileName;
            }
            else if (allProfiles.Count == 2) {
                ProfileName1 = allProfiles[0].ProfileName;
                ProfileName2 = allProfiles[1].ProfileName;
            }
            else if (allProfiles.Count == 1) {
                ProfileName1 = allProfiles[0].ProfileName;
            }
        }

        #endregion Load ProfileName for each profile upon startup

        #region Load ProfileBackground for each profile upon startup
        private void LoadProfileBackground() {

            //populate each case independently, fixed names so can't automate this xdd
            List<ProfileModel> allProfiles = SQLiteDataAccess.LoadAllProfiles();
            if (allProfiles.Count == 9) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
                ProfileBackground2 = allProfiles[1].BackgroundURL;
                ProfileBackground3 = allProfiles[2].BackgroundURL;
                ProfileBackground4 = allProfiles[3].BackgroundURL;
                ProfileBackground5 = allProfiles[4].BackgroundURL;
                ProfileBackground6 = allProfiles[5].BackgroundURL;
                ProfileBackground7 = allProfiles[6].BackgroundURL;
                ProfileBackground8 = allProfiles[7].BackgroundURL;
                ProfileBackground9 = allProfiles[8].BackgroundURL;
            }
            else if (allProfiles.Count == 8) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
                ProfileBackground2 = allProfiles[1].BackgroundURL;
                ProfileBackground3 = allProfiles[2].BackgroundURL;
                ProfileBackground4 = allProfiles[3].BackgroundURL;
                ProfileBackground5 = allProfiles[4].BackgroundURL;
                ProfileBackground6 = allProfiles[5].BackgroundURL;
                ProfileBackground7 = allProfiles[6].BackgroundURL;
                ProfileBackground8 = allProfiles[7].BackgroundURL;
            }
            else if (allProfiles.Count == 7) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
                ProfileBackground2 = allProfiles[1].BackgroundURL;
                ProfileBackground3 = allProfiles[2].BackgroundURL;
                ProfileBackground4 = allProfiles[3].BackgroundURL;
                ProfileBackground5 = allProfiles[4].BackgroundURL;
                ProfileBackground6 = allProfiles[5].BackgroundURL;
                ProfileBackground7 = allProfiles[6].BackgroundURL;
            }
            else if (allProfiles.Count == 6) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
                ProfileBackground2 = allProfiles[1].BackgroundURL;
                ProfileBackground3 = allProfiles[2].BackgroundURL;
                ProfileBackground4 = allProfiles[3].BackgroundURL;
                ProfileBackground5 = allProfiles[4].BackgroundURL;
                ProfileBackground6 = allProfiles[5].BackgroundURL;
            }
            else if (allProfiles.Count == 5) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
                ProfileBackground2 = allProfiles[1].BackgroundURL;
                ProfileBackground3 = allProfiles[2].BackgroundURL;
                ProfileBackground4 = allProfiles[3].BackgroundURL;
                ProfileBackground5 = allProfiles[4].BackgroundURL;
            }
            else if (allProfiles.Count == 4) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
                ProfileBackground2 = allProfiles[1].BackgroundURL;
                ProfileBackground3 = allProfiles[2].BackgroundURL;
                ProfileBackground4 = allProfiles[3].BackgroundURL;
            }
            else if (allProfiles.Count == 3) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
                ProfileBackground2 = allProfiles[1].BackgroundURL;
                ProfileBackground3 = allProfiles[2].BackgroundURL;
            }
            else if (allProfiles.Count == 2) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
                ProfileBackground2 = allProfiles[1].BackgroundURL;
            }
            else if (allProfiles.Count == 1) {
                ProfileBackground1 = allProfiles[0].BackgroundURL;
            }


        }
        
        #endregion Load ProfileBackground for each profile upon startup

        #region Profile, Background and Visibility update upon Message from CustomProfileVM
        //upon message register, refresh all dials, update ProfileName and ProfileBackground based on info from DB
        private void CustomProfileMessage(CustomProfileVMmsg msg) {

            //ProfileName1 = msg.ProfileName;
            SDVisibilityControl();
            NSDVisibilityControl();
            ProfileNameControlSave();
            ProfileBackgroundControlSave();

        }
        #endregion Visibility Control upon Messages from other VMs

        #region ProfileName TextBlock control upon save
        private void ProfileNameControlSave() {
            //handle dynamic ProfileName generation
            List<ProfileModel> currentProfileList = SQLiteDataAccess.LoadCurrentProfile();
            ProfileModel currentProfile = currentProfileList[0];

            if (currentProfile.ID == 1) {
                ProfileName1 = currentProfile.ProfileName;
            }
            else if (currentProfile.ID == 2) {
                ProfileName2 = currentProfile.ProfileName;
            }
            else if (currentProfile.ID == 3) {
                ProfileName3 = currentProfile.ProfileName;
            }
            else if (currentProfile.ID == 4) {
                ProfileName4 = currentProfile.ProfileName;
            }
            else if (currentProfile.ID == 5) {
                ProfileName5 = currentProfile.ProfileName;
            }
            else if (currentProfile.ID == 6) {
                ProfileName6 = currentProfile.ProfileName;
            }
            else if (currentProfile.ID == 7) {
                ProfileName7 = currentProfile.ProfileName;
            }
            else if (currentProfile.ID == 8) {
                ProfileName8 = currentProfile.ProfileName;
            }
            else if (currentProfile.ID == 9) {
                ProfileName9 = currentProfile.ProfileName;
            }
        }
        #endregion ProfileName TextBlock control

        #region ProfileBackground Border Background control upon save
        private void ProfileBackgroundControlSave() {
            List<ProfileModel> currentProfileList = SQLiteDataAccess.LoadCurrentProfile();
            ProfileModel currentProfile = currentProfileList[0];
            if (currentProfile.ID == 1) {
                ProfileBackground1 = currentProfile.BackgroundURL;
            }
            else if (currentProfile.ID == 2) {
                ProfileBackground2 = currentProfile.BackgroundURL;
            }
            else if (currentProfile.ID == 3) {
                ProfileBackground3 = currentProfile.BackgroundURL;
            }
            else if (currentProfile.ID == 4) {
                ProfileBackground4 = currentProfile.BackgroundURL;
            }
            else if (currentProfile.ID == 5) {
                ProfileBackground5 = currentProfile.BackgroundURL;
            }
            else if (currentProfile.ID == 6) {
                ProfileBackground6 = currentProfile.BackgroundURL;
            }
            else if (currentProfile.ID == 7) {
                ProfileBackground7 = currentProfile.BackgroundURL;
            }
            else if (currentProfile.ID == 8) {
                ProfileBackground8 = currentProfile.BackgroundURL;
            }
            else if (currentProfile.ID == 9) {
                ProfileBackground9 = currentProfile.BackgroundURL;
            }
        }
        #endregion

        #region Close Profile Button
        private void CloseProfile1() {
            //currentID = 1st Profile
            int currentID = 1;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        private void CloseProfile2() {
            //currentID = 2nd Profile
            int currentID = 2;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        private void CloseProfile3() {
            //currentID = 3rd Profile
            int currentID = 3;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        private void CloseProfile4() {
            //currentID = 4th Profile
            int currentID = 4;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        private void CloseProfile5() {
            //currentID = 5th Profile
            int currentID = 5;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        private void CloseProfile6() {
            //currentID = 6th Profile
            int currentID = 6;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        private void CloseProfile7() {
            //currentID = 7th Profile
            int currentID = 7;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        private void CloseProfile8() {
            //currentID = 8th Profile
            int currentID = 8;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        private void CloseProfile9() {
            //currentID = 9th Profile
            int currentID = 9;
            //wrapper needed to generelize the query (don't know how to do parametarized int queries, only objects with Properties) 
            IntWrapper iWrapper = new IntWrapper();
            iWrapper.Integer = currentID;
            //delete record at id=curentID
            SQLiteDataAccess.DeleteRecord(iWrapper);
            //ID-- for all subsequent records (speed dial functionality)
            UpdateIDsOnDeletion(currentID);
            //hide/show new relevant Profiles/New Speed Dial
            UpdateDialsUponDeletion();
            //Refresh ProfileName and ProfileBackground from the DB
            LoadProfileName();
            LoadProfileBackground();
        }
        #endregion

        #region Update Profile IDs upon deletion
        //upon deletion of a Profile, move all other profiles to the left by one (dial functionality), ID--
        private void UpdateIDsOnDeletion(int idToReplace) {
            //grab the id of a Profile that is being deleted
            List<int> allIDs = new List<int>();
            allIDs = SQLiteDataAccess.AllIDs();
            //update all the subsequent Profile ids to id=id-1 (dial functionality- move them one grid to the left)
            for (int i = idToReplace; i <= allIDs.Count; i++) {
                int newID = allIDs[i - 1] - 1;
                NewOldIDWrapper idWrapper = new NewOldIDWrapper();
                idWrapper.NewID = newID;
                idWrapper.OldID = newID + 1;
                SQLiteDataAccess.UpdateID(idWrapper);
            }
        }
        #endregion

        #region Update Profile Visibility upon deletion
        private void UpdateDialsUponDeletion() {
            List<int> allIDs = SQLiteDataAccess.AllIDs();
            int idAmount = allIDs.Count;
            if (idAmount == 0) {
                NSD2Visibility = false;
                NSD1Visibility = true;
                SD1Visibility = false;
                return;
            }
            int maxID = allIDs[idAmount - 1];
            if (maxID == 8) {
                SD9Visibility = false;
                NSD9Visibility = true;
            }
            else if (maxID == 7) {
                NSD9Visibility = false;
                NSD8Visibility = true;
                SD8Visibility = false;
            }
            else if (maxID == 6) {
                NSD8Visibility = false;
                NSD7Visibility = true;
                SD7Visibility = false;
            }
            else if (maxID == 5) {
                NSD7Visibility = false;
                NSD6Visibility = true;
                SD6Visibility = false;
            }
            else if (maxID == 4) {
                NSD6Visibility = false;
                NSD5Visibility = true;
                SD5Visibility = false;
            }
            else if (maxID == 3) {
                NSD5Visibility = false;
                NSD4Visibility = true;
                SD4Visibility = false;
            }
            else if (maxID == 2) {
                NSD4Visibility = false;
                NSD3Visibility = true;
                SD3Visibility = false;
            }
            else if (maxID == 1) {
                NSD3Visibility = false;
                NSD2Visibility = true;
                SD2Visibility = false;
            }
        }
        #endregion

        #region Visibility control for all 9 Speed Dials

        private void SDVisibilityControl() {

            //for each record in DB that exists, toggle the corresponding profile visibility on
            foreach (int i in SQLiteDataAccess.AllIDs()) {
                if (i == 1) { 
                    SD1Visibility = true; 
                }
                if (i == 2) { 
                    SD2Visibility = true; 
                }
                if (i == 3) { 
                    SD3Visibility = true; 
                }
                if (i == 4) { 
                    SD4Visibility = true; 
                }
                if (i == 5) { 
                    SD5Visibility = true; 
                }
                if (i == 6) { 
                    SD6Visibility = true; 
                }
                if (i == 7) { 
                    SD7Visibility = true; 
                }
                if (i == 8) { 
                    SD8Visibility = true; 
                }
                if (i == 9) { 
                    SD9Visibility = true; 
                }
            }
        }
        #endregion Visibility control for all 9 Speed Dials

        #region Visibility control for all 9 New Speed Dials

        private void NSDVisibilityControl() {
            //if there are no records (no profiles, toggle the first New Profile Dial on
            if (SQLiteDataAccess.AllIDs().Count == 0) {
                NSD1Visibility = true;
                NSD2Visibility = false;
                NSD3Visibility = false;
                NSD4Visibility = false;
                NSD5Visibility = false;
                NSD6Visibility = false;
                NSD7Visibility = false;
                NSD8Visibility = false;
                NSD9Visibility = false;
            }
            //otherwise only toggle id+1 New Profile Dial on, max: id=8 +1
            else if (SQLiteDataAccess.AllIDs().Count == 1) {
                NSD2Visibility = true;
                NSD1Visibility = false;
                NSD3Visibility = false;
                NSD4Visibility = false;
                NSD5Visibility = false;
                NSD6Visibility = false;
                NSD7Visibility = false;
                NSD8Visibility = false;
                NSD9Visibility = false;
            }
            else if (SQLiteDataAccess.AllIDs().Count == 2) {
                NSD3Visibility = true;
                NSD1Visibility = false;
                NSD2Visibility = false;
                NSD4Visibility = false;
                NSD5Visibility = false;
                NSD6Visibility = false;
                NSD7Visibility = false;
                NSD8Visibility = false;
                NSD9Visibility = false;
            }
            else if (SQLiteDataAccess.AllIDs().Count == 3) {
                NSD4Visibility = true;
                NSD1Visibility = false;
                NSD2Visibility = false;
                NSD3Visibility = false;
                NSD5Visibility = false;
                NSD6Visibility = false;
                NSD7Visibility = false;
                NSD8Visibility = false;
                NSD9Visibility = false;
            }
            else if (SQLiteDataAccess.AllIDs().Count == 4) {
                NSD5Visibility = true;
                NSD1Visibility = false;
                NSD2Visibility = false;
                NSD3Visibility = false;
                NSD4Visibility = false;
                NSD6Visibility = false;
                NSD7Visibility = false;
                NSD8Visibility = false;
                NSD9Visibility = false;
            }
            else if (SQLiteDataAccess.AllIDs().Count == 5) {
                NSD6Visibility = true;
                NSD1Visibility = false;
                NSD2Visibility = false;
                NSD3Visibility = false;
                NSD4Visibility = false;
                NSD5Visibility = false;
                NSD7Visibility = false;
                NSD8Visibility = false;
                NSD9Visibility = false;
            }
            else if (SQLiteDataAccess.AllIDs().Count == 6) {
                NSD7Visibility = true;
                NSD1Visibility = false;
                NSD2Visibility = false;
                NSD3Visibility = false;
                NSD4Visibility = false;
                NSD5Visibility = false;
                NSD6Visibility = false;
                NSD8Visibility = false;
                NSD9Visibility = false;
            }
            else if (SQLiteDataAccess.AllIDs().Count == 7) {
                NSD8Visibility = true;
                NSD1Visibility = false;
                NSD2Visibility = false;
                NSD3Visibility = false;
                NSD4Visibility = false;
                NSD5Visibility = false;
                NSD6Visibility = false;
                NSD7Visibility = false;
                NSD9Visibility = false;
            }
            else if (SQLiteDataAccess.AllIDs().Count == 8) {
                NSD9Visibility = true;
                NSD1Visibility = false;
                NSD2Visibility = false;
                NSD3Visibility = false;
                NSD4Visibility = false;
                NSD5Visibility = false;
                NSD6Visibility = false;
                NSD7Visibility = false;
                NSD8Visibility = false;
            }
            else if (SQLiteDataAccess.AllIDs().Count == 9) {
                NSD9Visibility = false;
                NSD1Visibility = false;
                NSD2Visibility = false;
                NSD3Visibility = false;
                NSD4Visibility = false;
                NSD5Visibility = false;
                NSD6Visibility = false;
                NSD7Visibility = false;
                NSD8Visibility = false;
            }
        }
        #endregion Visibility control for all 9 New Speed Dials

        #region Edit Profile Button
        private void EditFirstRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile1();
        }
        private void EditSecondRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile2();
        }
        private void EditThirdRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile3();
        }
        private void EditFourthRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile4();
        }
        private void EditFifthRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile5();
        }
        private void EditSixthRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile6();
        }
        private void EditSeventhRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile7();
        }
        private void EditEighthRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile8();
        }
        private void EditNinethRecord() {
            SQLiteDataAccess.ResetAllCurrentFields();
            SQLiteDataAccess.CurrentProfile9();
        }
        #endregion Edit Profile Button

        #region Email the user their config

        //Populate EmailConfig.txt with the 
        private void PopulateFile(int populateID) {
            //EmailConfig.txt debug path
            string outputDirectory = System.AppDomain.CurrentDomain.BaseDirectory+@"EmailConfig.txt";
            //clear content
            File.WriteAllText(outputDirectory, String.Empty);
            //grab the current profile, based on a passed ID
            List<ProfileModel> allProfiles = SQLiteDataAccess.LoadAllProfiles();
            //populate EmailConfig.txt using the relevant record
            using (var configFile = new StreamWriter(outputDirectory, true)) {
                configFile.WriteLine("ProfileName #" + allProfiles[populateID-1].ProfileName + "#.");
                configFile.WriteLine("Background #" + allProfiles[populateID - 1].BackgroundURL + "#.");
                configFile.WriteLine("KeyPressMacro #" + allProfiles[populateID - 1].ButtonMacro + "#.");
                configFile.WriteLine("SensitivityFrom #" + allProfiles[populateID - 1].SensitivityFrom + "#.");
                configFile.WriteLine("Sensitivity #" + allProfiles[populateID - 1].Sensitivity + "#.");
                configFile.WriteLine("DPI #" + allProfiles[populateID - 1].DPI + "#.");
                configFile.WriteLine("Apex Legends #" + allProfiles[populateID - 1].Apex + "#.");
                configFile.WriteLine("CS:GO #" + allProfiles[populateID - 1].CSGO + "#.");
                configFile.WriteLine("CS 1.6 #" + allProfiles[populateID - 1].CS16 + "#.");
                configFile.WriteLine("CS Source #" + allProfiles[populateID - 1].CSS + "#.");
                configFile.WriteLine("Garry's Mod #" + allProfiles[populateID - 1].GMod + "#.");
                configFile.WriteLine("Half Life 2 #" + allProfiles[populateID - 1].HL2 + "#.");
                configFile.WriteLine("Left 4 Dead 2 #" + allProfiles[populateID - 1].L4D2 + "#.");
                configFile.WriteLine("Minecraft #" + allProfiles[populateID - 1].Minecraft + "#.");
                configFile.WriteLine("Paladins #" + allProfiles[populateID - 1].Paladins + "#.");
                configFile.WriteLine("Portal 2 #" + allProfiles[populateID - 1].Portal2 + "#.");
                configFile.WriteLine("Quake Live #" + allProfiles[populateID - 1].Quake + "#.");
                configFile.WriteLine("Rust #" + allProfiles[populateID - 1].Rust + "#.");
                configFile.WriteLine("Smite #" + allProfiles[populateID - 1].Smite + "#.");
                configFile.WriteLine("Team Fortress #" + allProfiles[populateID - 1].TF2 + "#.");
                
            }
        }

        //Send email with the attached file
        private void EmailConfig(int populateID) {

            PopulateFile(populateID);

            //To and From addresses
            var fromAddress = new MailAddress("sensitivityConfig@gmail.com", "");
            //Check if the given email is in a valid format
            if (!IsValidEmail(AppSettings.Default.Email)) {
                return;
            }
            var toAddress = new MailAddress(AppSettings.Default.Email);
            const string fromPassword = "fTwwWq2DV2r2dUL";

            //populating Subject and Body
            List<ProfileModel> allProfiles = SQLiteDataAccess.LoadAllProfiles();
            string subject = allProfiles[populateID-1].ProfileName+" Config";
            string body = "Config text file for "+allProfiles[populateID-1].ProfileName+" Profile.";

            //create a file attachment
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"EmailConfig.txt";
            Attachment attachment = new Attachment(filePath, MediaTypeNames.Application.Octet);
            
            //client settings
            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            //populating the message and sending it
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
                
        }) {
                message.Attachments.Add(attachment);
                client.Send(message);
            }
        }
        #endregion

        #region Check if a given email is valid
        //Not perfect, invalid emails with a correct format = true, format: string@string
        private bool IsValidEmail(string emailAddress) {
            try {
                var address = new MailAddress(emailAddress);
                return address.Address == emailAddress;
            }
            catch {
                return false;
            }
        }
        #endregion Check if a given email is valid
    }
}
