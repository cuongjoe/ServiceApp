using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Solution1.Models
{
    public class StringToIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = (string)value;
            string test = (string)parameter;
            return input == test;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is bool))
            {
                return string.Empty;
            }
            if (parameter == null || !(parameter is string))
            {
                return string.Empty;
            }
            if ((bool)value)
            {
                return parameter.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
