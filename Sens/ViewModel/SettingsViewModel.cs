using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using Sens.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sens.ViewModel {

    /// <summary>
    /// A ViewModel controlling MyProfilesPage View. All the user input is saved in AppSettings.Default. 
    /// This VM controls user email, launch on start options (implemented here) and auto-update (implemented
    /// in EntryPoint.cs as it runs an async Task every time the app is opened to check for updates and update.
    /// </summary>
    public class SettingsViewModel : BaseViewModel {

        //FIELDS

        #region Default UI fields
        private string userEmail;
        private bool launch1;
        private bool launch2;
        private bool launch3;
        private bool autoUpdate;
        private bool dataCollection;
        #endregion Default UI fields
        
        //PROPERTIES

        #region Default User Input Properties
        public string UserEmail {
            get {
                return userEmail;
            }
            set {
                userEmail = value;
                AppSettings.Default.Email = value;
                AppSettings.Default.Save();
                OnPropertyChanged("UserEmail");
            }
        }

        public bool Launch1 {
            get {
                return launch1;
            }
            set {
                launch1 = value;
                AppSettings.Default.LaunchOption1 = value;
                AppSettings.Default.Save();
                Radio1Control();
                OnPropertyChanged("Launch1");
            }
        }

        public bool Launch2 {
            get {
                return launch2;
            }
            set {
                launch2 = value;
                AppSettings.Default.LaunchOption2 = value;
                AppSettings.Default.Save();
                Radio2Control();
                OnPropertyChanged("Launch2");
            }
        }

        public bool Launch3 {
            get {
                return launch3;
            }
            set {
                launch3 = value;
                AppSettings.Default.LaunchOption3 = value;
                AppSettings.Default.Save();
                Radio3Control();
                OnPropertyChanged("Launch3");
            }
        }

        public bool AutoUpdate {
            get {
                return autoUpdate;
            }
            set {
                autoUpdate = value;
                AppSettings.Default.AutoUpdate = value;
                AppSettings.Default.Save();
                OnPropertyChanged("AutoUpdate");
            }
        }

        public bool DataCollection {
            get {
                return dataCollection;
            }
            set {
                dataCollection = value;
                AppSettings.Default.DataCollection = value;
                AppSettings.Default.Save();
                OnPropertyChanged("DataCollection");
            }
        }
        #endregion Default UI Properties


        //CONSTRUCTOR
        public SettingsViewModel() {

            #region Reading and populating UI from saved settings (AppSettings.settings)
            UserEmail = AppSettings.Default.Email;
            Launch1 = AppSettings.Default.LaunchOption1;
            Launch2 = AppSettings.Default.LaunchOption2;
            Launch3 = AppSettings.Default.LaunchOption3;
            AutoUpdate = AppSettings.Default.AutoUpdate;
            DataCollection = AppSettings.Default.DataCollection;
            #endregion Reading and populating UI from saved settings (AppSettings.settings)

            RadioStateOnLaunch();

            StartupControl = new RelayCommand(() => RegistryControl());
            
        }

        //COMMANDS

        public ICommand StartupControl { get; set; }
        
        //METHODS

        #region RadioButton Control
        //Same RadioButton Group
        private void Radio1Control() {
            if (Launch1 == true) {
                Launch2 = false;
                Launch3 = false;
            }
        }
        //Same RadioButton Group
        private void Radio2Control() {
            if (Launch2 == true) {
                Launch1 = false;
                Launch3 = false;
            }
        }
        //Same RadioButton Group
        private void Radio3Control() {
            if (Launch3 == true) {
                Launch1 = false;
                Launch2 = false;
            }
        }
        //on first launch set radio3 as default
        private void RadioStateOnLaunch() {
            if (Launch1 == false && Launch2 == false && Launch3 == false) {
                Launch3 = true;
            }
        }
        #endregion RadioButton Control

        #region Open on Startup Control

        //static path for the current user default location: C:\Users\userXYZ\AppData\Local\Sens\Sens.exe
        //because I could not find a WPF alternative to System.Windows.Forms.Application.ExecutablePath
        public static string exePath = @"C:\Users\userXYZ\AppData\Local\Sens\Sens.exe";
        string currentUser = Environment.UserName;

        //registry control (on startup/run)
        private void RegistryControl() {
            //new key in startup registry location
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (Launch1) {
                //launch on startup
                key.SetValue("Sens", @"C:\Users\" + currentUser + @"\AppData\Local\Sens\Sens.exe");
            }
            else if (Launch2) {
                //launch on startup, minimized
                key.SetValue("Sens", @"C:\Users\" + currentUser + @"\AppData\Local\Sens\Sens.exe" + @" --start-minimized");
            }
            else
                //delete registry key and don't launch on startup
                key.DeleteValue("Sens");
        }
        #endregion Open on Startup Control

    }
}
