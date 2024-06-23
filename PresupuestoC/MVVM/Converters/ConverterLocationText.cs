using PresupuestoC.Models;
using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PresupuestoC.MVVM.Converters
{
    public class ConverterLocationText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DistrictModel district)
            {
                return $"{district.Province.Region.Name} - {district.Province.Name} - {district.Name}";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
