using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Services
{
    public sealed class StringEqualsConverter : IValueConverter
    {
        //Value: The value from the ViewModel (e.g., "Automatically")
        //TargetType: What type the UI expects (e.g., typeof(bool))
        //Parameter: ConverterParameter from XAML (e.g., "Automatically")
        //Culture: Culture info for formatting
        public Object Convert(Object? value, Type targetType, Object? parameter, CultureInfo culture)
        {
            return String.Equals(value as String, parameter as String, StringComparison.Ordinal);
        }

        //Value: The value from the UI (e.g., true/false)
        //TargetType: What type the ViewModel expects (e.g., typeof(string))
        //Parameter: ConverterParameter from XAML (e.g., "Manually")
        //Culture: Culture info for formatting
        public Object ConvertBack(Object? value, Type targetType, Object? parameter, CultureInfo culture)
        {
            if (value is true)
            {
                return parameter as String ?? BindingOperations.DoNothing;
            }

            return BindingOperations.DoNothing;
        }
    }
}
