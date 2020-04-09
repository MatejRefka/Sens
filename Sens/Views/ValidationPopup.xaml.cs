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
using System.Windows.Shapes;
using Sens.ViewModel;

namespace Sens {
    /// <summary>
    /// Interaction logic for ValidationPopup.xaml
    /// </summary>
    public partial class ValidationPopup : Window {

        public ValidationPopup() {

            InitializeComponent();
            this.DataContext = new CustomProfileViewModel(this);
        }
    }
}
