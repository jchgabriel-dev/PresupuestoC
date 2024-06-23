using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PresupuestoC.MVVM.Converters
{ 
    internal class ConverterHeightBudget : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return GridLength.Auto;

            Type targetTypeParameter = parameter as Type;

            if (targetTypeParameter.Equals(value.GetType()))
            {
                return new GridLength(38, GridUnitType.Star);
            }

            return GridLength.Auto;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
