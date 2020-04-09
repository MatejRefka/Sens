﻿using System;
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


namespace Sens.Pages {
    /// <summary>
    /// Interaction logic for FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page {
        public FeedbackPage() {
            InitializeComponent();
            DataContext = new FeedbackViewModel();
        }
    }
}
