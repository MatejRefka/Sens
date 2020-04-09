using System;
using System.Windows;
using System.Windows.Controls;
using Sens.ViewModel;

namespace Sens
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page{

        public HomePage() {

            InitializeComponent();
            DataContext = new HomeViewModel();

        }

    }
}

