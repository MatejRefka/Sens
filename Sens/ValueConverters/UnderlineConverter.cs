using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Sens.ValueConverters {

    /// <summary>
    /// Converts bool to TextDecoration, used to control Underline as bool property in WindowViewModel.
    /// </summary>

    public class UnderlineConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return null;

            return System.Convert.ToBoolean(value) ? TextDecorations.Underline : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
