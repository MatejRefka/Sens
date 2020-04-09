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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sens {
    /// <summary>
    /// Interaction logic for CustomProfile.xaml
    /// </summary>
    public partial class CustomProfile : Window {

            public CustomProfile() {

            InitializeComponent();
            this.DataContext = new CustomProfileViewModel(this);
            
        }

    }
}
