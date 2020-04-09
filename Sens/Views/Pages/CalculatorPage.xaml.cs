using Sens.ViewModel;
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
using Sens.Services;

namespace Sens.Pages {
    /// <summary>
    /// Interaction logic for CalculatorPage.xaml
    /// </summary>
    public partial class CalculatorPage : Page {

        public CalculatorPage() {

            InitializeComponent();
            ICalculator calculator = Factory.CreateCalculator();
            ICalculator2D calculator2D = Factory.CreateCalculator2D();
            DataContext = new CalculatorViewModel(calculator, calculator2D);
        }

    }
}
