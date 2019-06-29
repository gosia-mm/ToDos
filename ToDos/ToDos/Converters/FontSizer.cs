using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ToDos.Converters
{
    class FontSizer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fontSize = System.Convert.ToInt32(value);
            var relativeSize = System.Convert.ToDouble(parameter.ToString().Replace('.',','));
            return System.Convert.ToInt32(Math.Round(fontSize * relativeSize));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
