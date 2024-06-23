using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using PresupuestoC.Models.Archive;

namespace PresupuestoC.MVVM.Converters
{
    public class ConverterLineTree : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TreeViewItem item))
                return Visibility.Visible;

            ItemsControl parentItemsControl = ItemsControl.ItemsControlFromItemContainer(item);
            if (parentItemsControl == null)
                return Visibility.Visible;

            int index = parentItemsControl.ItemContainerGenerator.IndexFromContainer(item);
            int count = parentItemsControl.Items.Count;
            bool isLastItem = index == count - 1;

            if (isLastItem && item.Header is FolderModel folder)
            {
                string lastName = folder.Name;
                return Visibility.Hidden;
            }
          

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture)
        {
            return Visibility.Visible;
        }
    }

}
