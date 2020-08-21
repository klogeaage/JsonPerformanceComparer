using System;
using Xamarin.Forms;

namespace JsonPerformanceComparer.Converters
{
    public class NotNullOrEmptyBoolConverter : IValueConverter
    {
        /// <summary>
        /// Returns false if object or string is null or empty and true otherwise.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string s)
            {
                return !string.IsNullOrWhiteSpace(s);
            }
            if (value is int @int)
                return @int != 0;
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
