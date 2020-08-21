using System;
using Xamarin.Forms;

namespace JsonPerformanceComparer.Converters
{
    public class DeltaPercentConverter : IValueConverter
    {
        //// <summary>
        /// Converts DeltaPercent to clear text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var deltaPercent = (float)value;
            if (deltaPercent > 0)
                return $"System.Text.Json is {Math.Abs(deltaPercent):0.0%} slower.";
            return $"System.Text.Json is {Math.Abs(deltaPercent):0.0%} faster.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
