using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Currency;
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
    public class RegionCreateCommand : AsyncCommand
    {
        private readonly LocationCreateRegionViewModel _viewModel;
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;

        public RegionCreateCommand(LocationCreateRegionViewModel viewModel, SuperCloseNavigationService navigation, LocationListStore store)
        {

            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
        }


        public async override Task ExecuteAsync(object parameter)
        {
          
            try
            {
                _viewModel.Name = _viewModel.Name;

                if (_viewModel.HasErrors)
                {
                    return;
                }

                RegionModel region = new RegionModel();
                region.Name = _viewModel.Name;

                await _store.CreateRegion(region);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la region", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
