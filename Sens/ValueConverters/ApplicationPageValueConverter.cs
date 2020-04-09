using Sens.DataModels;
using Sens.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens.ValueConverters {

    /// <summary>
    /// Converts ApplicationPage enum into an actual page
    /// </summary>

    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //switch the current value of type ApplicationPage
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Home:
                    return new HomePage();

                case ApplicationPage.Calculator:
                    return new CalculatorPage();

                case ApplicationPage.MyProfiles:
                    return new MyProfilesPage();

                case ApplicationPage.Tutorials:
                    return new TutorialsPage();

                case ApplicationPage.Settings:
                    return new SettingsPage();

                case ApplicationPage.Feedback:
                    return new FeedbackPage();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
