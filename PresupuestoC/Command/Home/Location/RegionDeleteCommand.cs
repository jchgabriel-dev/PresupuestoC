using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.ViewModels.Location.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Location
{
    public class RegionDeleteCommand : AsyncCommand
    {
        private readonly LocationDeleteRegionViewModel _viewModel;
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;
        private readonly RegionSelectedStore _selected;

        public RegionDeleteCommand(LocationDeleteRegionViewModel viewModel, SuperCloseNavigationService navigation,
            RegionSelectedStore selected, LocationListStore store)
        {
            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.DeleteRegion(_selected.CurrentRegion.Id);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la region", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
