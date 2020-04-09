using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sens.ViewModel;
using Sens.Services;

namespace Sens.Pages {
    /// <summary>
    /// Interaction logic for MyProfilesPage.xaml
    /// </summary>
    public partial class MyProfilesPage : Page {
        public MyProfilesPage() {
            ICalculator calculator = Factory.CreateCalculator();
            ICalculator2D calculator2D = Factory.CreateCalculator2D();
            IWriteToCFG cfgWriter = Factory.CreateCFGWriter(calculator);
            InitializeComponent();
            DataContext = new MyProfilesViewModel(calculator, calculator2D, cfgWriter);
        }

        //open a new Profile form on + button click
        private void Edit_Custom_Profile_Window(object sender, RoutedEventArgs e) {
            //execute command before click (command sets current=1, before new window uses it)
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
            //new CustomProfile window
            CustomProfile win = new CustomProfile();
            win.ShowDialog();
        }
        private void New_Custom_Profile_Window(object sender, RoutedEventArgs e) {
            //reset all current fields and then new CustomProfile window
            SQLiteDataAccess.ResetAllCurrentFields();
            CustomProfile win = new CustomProfile();
            win.ShowDialog();
        }
    }
}
