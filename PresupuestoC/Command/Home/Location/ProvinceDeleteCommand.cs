using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.ViewModels.Location.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Location
{
    public class ProvinceDeleteCommand : AsyncCommand
    {
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;
        private readonly ProvinceSelectedStore _selected;

        public ProvinceDeleteCommand(SuperCloseNavigationService navigation,
            LocationListStore store, ProvinceSelectedStore selected)
        {
            _navigation = navigation;
            _store = store;
            _selected = selected;

        }

        public async override Task ExecuteAsync(object parameter)
        {

            try
            {
                await _store.DeleteProvince(_selected.CurrentProvince.Id);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la provincia", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
