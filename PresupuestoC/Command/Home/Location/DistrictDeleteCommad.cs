using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PresupuestoC.Navigation.Location;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location.District;
using PresupuestoC.Navigation.SuperModal;

namespace PresupuestoC.Command.Location
{
    public class DistrictDeleteCommand : AsyncCommand
    {
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;
        private readonly DistrictSelectedStore _selected;

        public DistrictDeleteCommand(SuperCloseNavigationService navigation,
            LocationListStore store, DistrictSelectedStore selected)
        {
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {

            try
            {
                await _store.DeleteDistrict(_selected.CurrentDistrict.Id);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el distrito", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}