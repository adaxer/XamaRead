using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace XamaRead.Converters
{
    public class EllipsisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string theValue 
                && int.TryParse(parameter?.ToString(), out int theParameter) 
                && theValue.Length > theParameter)
            {
                return $"{theValue.Substring(0, theParameter)}(…)";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
