using Sens.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sens.DataModels;
using GalaSoft.MvvmLight.Messaging;
using Sens.Utility;
using Sens.Services;
using Microsoft.Win32;
using System.Diagnostics;

namespace Sens
{
    /// <summary>
    /// CS file for the main window of the application. Other than setting the datacontext, it also contains
    /// the implementation for gloabl Hotkeys (set for each profile) because a window needs to be passed as 
    /// a parameter. Also, SourceInitialized and OnClose are overriden.
    /// </summary>

    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window {
        public MainWindow() {

            FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
            InitializeComponent();

            //Linking the WindowViewModel with this main window.
            this.DataContext = new WindowViewModel(this);

            ICalculator calculator = Factory.CreateCalculator();
            IWriteToCFG cfgWriter = Factory.CreateCFGWriter(calculator);
            this.calculator = calculator;
            this.cfgWriter = cfgWriter;

            //On CustomProfile 'Save', catch the message from its VM, and then update the hotkey combo
            Messenger.Default.Register<CustomProfileVMmsg>(this, UpdateHotKey);
            //Populate virtual key dictionary
            PopulateDictionaries();
        }

        #region Global HotKeys Implementation (needs a window)

        private ICalculator calculator;
        private IWriteToCFG cfgWriter;

        //Native methods for registering hotkeys:
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        //ID for each profile
        private const int profile1HotKeyID = 1;
        private const int profile2HotKeyID = 2;
        private const int profile3HotKeyID = 3;
        private const int profile4HotKeyID = 4;
        private const int profile5HotKeyID = 5;
        private const int profile6HotKeyID = 6;
        private const int profile7HotKeyID = 7;
        private const int profile8HotKeyID = 8;
        private const int profile9HotKeyID = 9;

        //key character to virtual key mapping
        private Dictionary<string, UInt32> vkDefault = new Dictionary<string, UInt32>();

        #region Populate the dictionaries
        private void PopulateDictionaries() {
            vkDefault.Add("A", 0x41);
            vkDefault.Add("B", 0x42);
            vkDefault.Add("C", 0x43);
            vkDefault.Add("D", 0x44);
            vkDefault.Add("E", 0x45);
            vkDefault.Add("F", 0x46);
            vkDefault.Add("G", 0x47);
            vkDefault.Add("H", 0x48);
            vkDefault.Add("I", 0x49);
            vkDefault.Add("J", 0x4A);
            vkDefault.Add("K", 0x4B); 
            vkDefault.Add("L", 0x4C);
            vkDefault.Add("M", 0x4D);
            vkDefault.Add("N", 0x4E);
            vkDefault.Add("O", 0x4F);
            vkDefault.Add("P", 0x50);
            vkDefault.Add("Q", 0x51);
            vkDefault.Add("R", 0x52);
            vkDefault.Add("S", 0x53);
            vkDefault.Add("T", 0x54);
            vkDefault.Add("U", 0x55);
            vkDefault.Add("V", 0x56);
            vkDefault.Add("W", 0x57);
            vkDefault.Add("X", 0x58);
            vkDefault.Add("Y", 0x59);
            vkDefault.Add("Z", 0x5A);
            vkDefault.Add("BACKSPACE", 0x08);
            vkDefault.Add("ENTER", 0x0D);
            vkDefault.Add("TAB", 0x09);
            vkDefault.Add("CAPS", 0x14);
            vkDefault.Add("SPACE", 0x20);
            vkDefault.Add("PAGEUP", 0x21);
            vkDefault.Add("PAGEDOWN", 0x22);
            vkDefault.Add("END", 0x23);
            vkDefault.Add("HOME", 0x24);
            vkDefault.Add("INSERT", 0x2D);
            vkDefault.Add("DELETE", 0x2E);
            vkDefault.Add("LEFT", 0x25);
            vkDefault.Add("UP", 0x26);
            vkDefault.Add("RIGHT", 0x27);
            vkDefault.Add("DOWN", 0x28);
            vkDefault.Add("0", 0x30);
            vkDefault.Add("1", 0x31);
            vkDefault.Add("2", 0x32);
            vkDefault.Add("3", 0x33);
            vkDefault.Add("4", 0x34);
            vkDefault.Add("5", 0x35);
            vkDefault.Add("6", 0x36);
            vkDefault.Add("7", 0x37);
            vkDefault.Add("8", 0x38);
            vkDefault.Add("9", 0x39);
            vkDefault.Add("*", 0x6A);
            vkDefault.Add("+", 0x6B);
            vkDefault.Add("-", 0xBD);
            vkDefault.Add("/", 0xBF);
            vkDefault.Add("=", 0xBB);
            vkDefault.Add(",", 0xBC);
            vkDefault.Add(".", 0xBE);
            vkDefault.Add("\\", 0xDC);
            vkDefault.Add("[", 0xDB);
            vkDefault.Add("]", 0xDD);
            vkDefault.Add(";", 0xBA);
        }
        #endregion populating the dictionaries

        //Default CTRL modifier (first hotkey combination)
        private const uint modControl = 0x0002;

        //Window to handles messages from OS uppon key press
        private HwndSource source;
        //The window's handler
        private IntPtr handler;

        //on app start, register all the Hotkeys
        protected override void OnSourceInitialized(EventArgs e) {

            base.OnSourceInitialized(e);

            //Instance of WindowInteropHelper to access a Handle property
            handler = new WindowInteropHelper(this).Handle;
            source = HwndSource.FromHwnd(handler);
            source.AddHook(HwndHook);

            //Register a hotkey for each profile
            RegisterHotKey(handler, profile1HotKeyID, modControl, GetKey(1));
            RegisterHotKey(handler, profile2HotKeyID, modControl, GetKey(2));
            RegisterHotKey(handler, profile3HotKeyID, modControl, GetKey(3));
            RegisterHotKey(handler, profile4HotKeyID, modControl, GetKey(4));
            RegisterHotKey(handler, profile5HotKeyID, modControl, GetKey(5));
            RegisterHotKey(handler, profile6HotKeyID, modControl, GetKey(6));
            RegisterHotKey(handler, profile7HotKeyID, modControl, GetKey(7));
            RegisterHotKey(handler, profile8HotKeyID, modControl, GetKey(8));
            RegisterHotKey(handler, profile9HotKeyID, modControl, GetKey(9));
        }

        //on profile save, update each Hotkey
        private void UpdateHotKey(CustomProfileVMmsg msg) {
            
            //Instance of WindowInteropHelper to access a Handle property
            handler = new WindowInteropHelper(this).Handle;
            source = HwndSource.FromHwnd(handler);
            source.AddHook(HwndHook);

            //Register a hotkey for each profile
            RegisterHotKey(handler, profile1HotKeyID, modControl, GetKey(1));
            RegisterHotKey(handler, profile2HotKeyID, modControl, GetKey(2));
            RegisterHotKey(handler, profile3HotKeyID, modControl, GetKey(3));
            RegisterHotKey(handler, profile4HotKeyID, modControl, GetKey(4));
            RegisterHotKey(handler, profile5HotKeyID, modControl, GetKey(5));
            RegisterHotKey(handler, profile6HotKeyID, modControl, GetKey(6));
            RegisterHotKey(handler, profile7HotKeyID, modControl, GetKey(7));
            RegisterHotKey(handler, profile8HotKeyID, modControl, GetKey(8));
            RegisterHotKey(handler, profile9HotKeyID, modControl, GetKey(9));
        }

        //return a uint virtual key for a specific profile hotkey
        private uint GetKey(int profileNumber) {

            //grab the hotkey string from the appropriate profile
            List<ProfileModel> allProfiles = SQLiteDataAccess.LoadAllProfiles();

            //if a profile is non-existant
            if (allProfiles.Count() < profileNumber) {
                return 0;
            }

            string key = allProfiles[profileNumber - 1].ButtonMacro;
            
            //if a key is not set within some profile
            if (key == null) {
                key = "ERROR";
            }

            //if it is a valid key (if it's in the dictionary)
            if (vkDefault.ContainsKey(key)) {
                return vkDefault[key];
            }

            return 0;
        }

        //event handler that recieves all window messages
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {

            const int WM_HOTKEY = 0x0312;

            switch (msg) {
                case WM_HOTKEY:
                    //determines that a hotkey was pressed
                    switch (wParam.ToInt32()) {

                        case profile1HotKeyID:
                            cfgWriter.CalcCM3D(1);
                            handled = true;
                            break;
                        case profile2HotKeyID:
                            cfgWriter.CalcCM3D(2);
                            handled = true;
                            break;
                        case profile3HotKeyID:
                            cfgWriter.CalcCM3D(3);
                            handled = true;
                            break;
                        case profile4HotKeyID:
                            cfgWriter.CalcCM3D(4);
                            handled = true;
                            break;
                        case profile5HotKeyID:
                            cfgWriter.CalcCM3D(5);
                            handled = true;
                            break;
                        case profile6HotKeyID:
                            cfgWriter.CalcCM3D(6);
                            handled = true;
                            break;
                        case profile7HotKeyID:
                            cfgWriter.CalcCM3D(7);
                            handled = true;
                            break;
                        case profile8HotKeyID:
                            cfgWriter.CalcCM3D(8);
                            handled = true;
                            break;
                        case profile9HotKeyID:
                            cfgWriter.CalcCM3D(9);
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        protected override void OnClosed(EventArgs e) {
            //remove hook and unregister all hotkeys
            source.RemoveHook(HwndHook);
            UnregisterHotKey(handler, profile1HotKeyID);
            UnregisterHotKey(handler, profile2HotKeyID);
            UnregisterHotKey(handler, profile3HotKeyID);
            UnregisterHotKey(handler, profile4HotKeyID);
            UnregisterHotKey(handler, profile5HotKeyID);
            UnregisterHotKey(handler, profile6HotKeyID);
            UnregisterHotKey(handler, profile7HotKeyID);
            UnregisterHotKey(handler, profile8HotKeyID);
            UnregisterHotKey(handler, profile9HotKeyID);
            base.OnClosed(e);
        }

        #endregion Global HotKeys Implementation (needs a window)

    }
}
