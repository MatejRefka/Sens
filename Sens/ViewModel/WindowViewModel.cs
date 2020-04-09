using Sens.DataModels;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sens.ViewModel {
    /// <summary>
    /// ViewModel for MainWindow View. Provides proeprties to structure and build the main window, including the custom
    /// panel buttons. Also handles the UI and functionality for navigation buttons, handling the page frame.
    /// 
    /// TODO:
    /// UI and functionality for the right side panel
    /// 
    /// </summary>

    public class WindowViewModel : BaseViewModel {

        /*FIELDS*/

        #region Necessary fields for the window
        private Window window;
        private int windowEdgeRadius = 5;
        private int windowOuterMargin = 10;
        #endregion

        /*PROPERTIES*/

        #region Window Properties
        //resize capture threshold
        public int ResizeBorder { get { return Borderless ? 0 : 0; } }
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder); } }

        //central content window padding
        public int LeftPadding { get; set; } = 10;
        public int RightPadding { get; set; } = 10;
        public int BottomPadding { get; set; } = 5;

        //Borderless bool state 
        public bool Borderless { get { return window.WindowState == WindowState.Maximized; } }

        //curved window corners, disabled when fullscreen
        public int EdgeRadius {
            get {
                if (window.WindowState == WindowState.Maximized) {
                    return 0;
                }
                else {
                    return windowEdgeRadius;
                }
            }

            set { windowEdgeRadius = value; }
        }

        //conver EdgeRadius int to type CornerRadius (used in xaml)
        public CornerRadius EdgeCornerRadius { get { return new CornerRadius(EdgeRadius); } }


        //window drop shadow, disabled when fullscreen
        public int OuterMarginSize {
            get {
                if (window.WindowState == WindowState.Maximized) {
                    return 0;
                }
                else {
                    return windowOuterMargin;
                }
            }

            set { windowOuterMargin = value; }
        }

        //convert OuterMarginSize int to type Thickness (used in xaml)
        public Thickness OuterMarginThickness { get; set; } = new Thickness(0 );


        //Title/caption bar height
        public int CaptionHeight { get; set; } = 30;

        //convert int into GridLength
        public GridLength PanelHeightGridLength { get { return new GridLength(CaptionHeight + ResizeBorder); } }

        #endregion WindowProperties

        #region Pagehandler
        //page handler
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Home;
        #endregion PageHandler

        #region Navigation Button Properties
        //navigation button Hover Over and Foreground
        public SolidColorBrush NavButtonHover { get; set; } = new SolidColorBrush(Colors.White);
        public bool HomeButtonUnderline { get; set; } = false;
        public bool CalculatorButtonUnderline { get; set; } = false;
        public bool MyProfilesButtonUnderline { get; set; } = false;
        public bool TutorialsButtonUnderline { get; set; } = false;
        public bool SettingsButtonUnderline { get; set; } = false;
        public bool FeedbackButtonUnderline { get; set; } = false;

        public SolidColorBrush HomeButtonForeground { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        public SolidColorBrush CalculatorButtonForeground { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        public SolidColorBrush MyProfilesButtonForeground { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        public SolidColorBrush TutorialsButtonForeground { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        public SolidColorBrush SettingsButtonForeground { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        public SolidColorBrush FeedbackButtonForeground { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        #endregion navigation Button Properties

        /*COMMANDS*/

        #region Main Window panel buttons
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        #endregion Main Window panel buttons

        #region Navigation Button Functionality and Hover Over
        public ICommand HomeNavCommand { get; set; }
        public ICommand CalculatorNavCommand { get; set; }
        public ICommand MyProfilesNavCommand { get; set; }
        public ICommand TutorialsNavCommand { get; set; }
        public ICommand SettingsNavCommand { get; set; }
        public ICommand FeedbackNavCommand { get; set; }

        public ICommand MouseEnterHomeCommand { get; set; }
        public ICommand MouseEnterCalculatorCommand { get; set; }
        public ICommand MouseEnterMyProfilesCommand { get; set; }
        public ICommand MouseEnterTutorialsCommand { get; set; }
        public ICommand MouseEnterSettingsCommand { get; set; }
        public ICommand MouseEnterFeedbackCommand { get; set; }

        public ICommand MouseLeaveHomeCommand { get; set; }
        public ICommand MouseLeaveCalculatorCommand { get; set; }
        public ICommand MouseLeaveMyProfilesCommand { get; set; }
        public ICommand MouseLeaveTutorialsCommand { get; set; }
        public ICommand MouseLeaveSettingsCommand { get; set; }
        public ICommand MouseLeaveFeedbackCommand { get; set; }
        #endregion Navigation Button Functionality and Hover Over

        /*CONSTRUCTOR*/
        public WindowViewModel(Window window) {

            this.window = window;

            #region Window Resizing and Maximize fix
            //Listen for the window resizing
            window.StateChanged += (sender, e) => {

                //On property changed (window state change) fire off all the properties that handle this
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(EdgeRadius));
                OnPropertyChanged(nameof(EdgeCornerRadius));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginThickness));

            };

            //window maximize fix 
            var windowResizer = new WindowResizer(window);
            #endregion Window Resizing and Maximize fix

            #region Main Window Panel Button RelayCommand
            //panel button commands
            MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => AppShutDown());
            #endregion Main Window Panel Button RelayCommand

            #region Navigation Buttons RelayCommand (functionality and mouse over)

            //sets default Home button foreground and underline on load (before nav methods handle this)
            HomeButtonForeground = new SolidColorBrush(Colors.White);
            HomeButtonUnderline = true;

            //nav buttons (on click, enter hover, leave hover)
            HomeNavCommand = new RelayCommand(() => Home());
            CalculatorNavCommand = new RelayCommand(() => Calculator());
            MyProfilesNavCommand = new RelayCommand(() => MyProfiles());
            TutorialsNavCommand = new RelayCommand(() => Tutorials());
            SettingsNavCommand = new RelayCommand(() => Settings());
            FeedbackNavCommand = new RelayCommand(() => Feedback());

            MouseEnterHomeCommand = new RelayCommand(() => MouseEnterHome());
            MouseEnterCalculatorCommand = new RelayCommand(() => MouseEnterCalculator());
            MouseEnterMyProfilesCommand = new RelayCommand(() => MouseEnterMyProfiles());
            MouseEnterTutorialsCommand = new RelayCommand(() => MouseEnterTutorials());
            MouseEnterSettingsCommand = new RelayCommand(() => MouseEnterSettings());
            MouseEnterFeedbackCommand = new RelayCommand(() => MouseEnterFeedback());

            MouseLeaveHomeCommand = new RelayCommand(() => MouseLeaveHome());
            MouseLeaveCalculatorCommand = new RelayCommand(() => MouseLeaveCalculator());
            MouseLeaveMyProfilesCommand = new RelayCommand(() => MouseLeaveMyProfiles());
            MouseLeaveTutorialsCommand = new RelayCommand(() => MouseLeaveTutorials());
            MouseLeaveSettingsCommand = new RelayCommand(() => MouseLeaveSettings());
            MouseLeaveFeedbackCommand = new RelayCommand(() => MouseLeaveFeedback());
            #endregion Navigation Buttons RelayCommand (functionality and mouse over)

            #region Adjust the Profile DB on start
            //reset 'current' field in Profile table (in case of a crash)
            SQLiteDataAccess.ResetAllCurrentFields();
            #endregion Adjust the Profile DB on start

            if (AppSettings.Default.LaunchMinimized == true) {
                
                window.WindowState = WindowState.Minimized;
                AppSettings.Default.LaunchMinimized = false;
            }
        }

        /*METHODS*/

        #region On Close Command
        //Reset all 'current' fields (current = 0) in Profile table, DB before app.shutdown
        private void AppShutDown() {
            //reset all 'current' fields in Profile table (current = 0)
            SQLiteDataAccess.ResetAllCurrentFields();
            //shutdown (close all windows, possibly hidden Profile forms as well)
            Application.Current.Shutdown();
        }
        #endregion On Close Command

        #region FUNCTIONS TO HANDLE NAVIGATION + BUTTON FORMATTING ON PAGE CHANGE

        private void Home() {

            CurrentPage = ApplicationPage.Home;
            HomeButtonForeground = new SolidColorBrush(Colors.White);
            HomeButtonUnderline = true;
            CalculatorButtonUnderline = false;
            MyProfilesButtonUnderline = false;
            TutorialsButtonUnderline = false;
            SettingsButtonUnderline = false;
            FeedbackButtonUnderline = false;
            CalculatorButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            MyProfilesButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            TutorialsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            SettingsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            FeedbackButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        }

        private void Calculator() {

            CurrentPage = ApplicationPage.Calculator;
            CalculatorButtonForeground = new SolidColorBrush(Colors.White);
            HomeButtonUnderline = false;
            CalculatorButtonUnderline = true;
            MyProfilesButtonUnderline = false;
            TutorialsButtonUnderline = false;
            SettingsButtonUnderline = false;
            FeedbackButtonUnderline = false;
            HomeButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            MyProfilesButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            TutorialsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            SettingsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            FeedbackButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        }

        private void MyProfiles() {

            CurrentPage = ApplicationPage.MyProfiles;
            MyProfilesButtonForeground = new SolidColorBrush(Colors.White);
            HomeButtonUnderline = false;
            CalculatorButtonUnderline = false;
            MyProfilesButtonUnderline = true;
            TutorialsButtonUnderline = false;
            SettingsButtonUnderline = false;
            FeedbackButtonUnderline = false;
            HomeButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            CalculatorButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            TutorialsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            SettingsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            FeedbackButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        }

        private void Tutorials() {

            CurrentPage = ApplicationPage.Tutorials;
            TutorialsButtonForeground = new SolidColorBrush(Colors.White);
            HomeButtonUnderline = false;
            CalculatorButtonUnderline = false;
            MyProfilesButtonUnderline = false;
            TutorialsButtonUnderline = true;
            SettingsButtonUnderline = false;
            FeedbackButtonUnderline = false;
            HomeButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            CalculatorButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            MyProfilesButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            SettingsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            FeedbackButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        }

        private void Settings() {

            CurrentPage = ApplicationPage.Settings;
            SettingsButtonForeground = new SolidColorBrush(Colors.White);
            HomeButtonUnderline = false;
            CalculatorButtonUnderline = false;
            MyProfilesButtonUnderline = false;
            TutorialsButtonUnderline = false;
            SettingsButtonUnderline = true;
            FeedbackButtonUnderline = false;
            HomeButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            CalculatorButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            MyProfilesButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            TutorialsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            FeedbackButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        }

        private void Feedback() {

            CurrentPage = ApplicationPage.Feedback;
            FeedbackButtonForeground = new SolidColorBrush(Colors.White);
            HomeButtonUnderline = false;
            CalculatorButtonUnderline = false;
            MyProfilesButtonUnderline = false;
            TutorialsButtonUnderline = false;
            SettingsButtonUnderline = false;
            FeedbackButtonUnderline = true;
            HomeButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            CalculatorButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            MyProfilesButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            TutorialsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            SettingsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
        }
        #endregion FUNCTIONS TO HANDLE NAVIGATION + BUTTON FORMATTING ON PAGE CHANGE

        #region FUNCTIONS TO HANDLE MOUSE OVER EVENT TRIGGERS FOR NAV BUTTONS

        private void MouseEnterHome() {
            HomeButtonForeground = new SolidColorBrush(Colors.White);
        }
        private void MouseEnterCalculator() {
            CalculatorButtonForeground = new SolidColorBrush(Colors.White);
        }
        private void MouseEnterMyProfiles() {
            MyProfilesButtonForeground = new SolidColorBrush(Colors.White);
        }
        private void MouseEnterTutorials() {
            TutorialsButtonForeground = new SolidColorBrush(Colors.White);
        }
        private void MouseEnterSettings() {
            SettingsButtonForeground = new SolidColorBrush(Colors.White);
        }
        private void MouseEnterFeedback() {
            FeedbackButtonForeground = new SolidColorBrush(Colors.White);
        }
        private void MouseLeaveHome() {
            if (CurrentPage != ApplicationPage.Home) {
                HomeButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            }
        }
        private void MouseLeaveCalculator() {
            if (CurrentPage != ApplicationPage.Calculator) {
                CalculatorButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            }
        }
        private void MouseLeaveMyProfiles() {
            if (CurrentPage != ApplicationPage.MyProfiles) {
                MyProfilesButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            }
        }
        private void MouseLeaveTutorials() {
            if (CurrentPage != ApplicationPage.Tutorials) {
                TutorialsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            }
        }
        private void MouseLeaveSettings() {
            if (CurrentPage != ApplicationPage.Settings) {
                SettingsButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            }
        }
        private void MouseLeaveFeedback() {
            if (CurrentPage != ApplicationPage.Feedback) {
                FeedbackButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#777777"));
            }
        }
        #endregion FUNCTIONS TO HANDLE MOUSE OVER EVENT TRIGGERS FOR NAV BUTTONS
    }
}

