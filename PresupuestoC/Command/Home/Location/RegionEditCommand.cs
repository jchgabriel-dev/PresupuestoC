using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores;
using PresupuestoC.ViewModels.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PresupuestoC.ViewModels.Location.Region;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location.District;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.Navigation.SuperModal;

namespace PresupuestoC.Command.Location
{
    public class RegionEditCommand : AsyncCommand
    {
        private readonly LocationEditRegionViewModel _viewModel;
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;
        private readonly RegionSelectedStore _selected;



        public RegionEditCommand(LocationEditRegionViewModel viewModel, 
            SuperCloseNavigationService navigation, LocationListStore store, RegionSelectedStore selected)
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
                _viewModel.Name = _viewModel.Name;

                if (_viewModel.HasErrors)
                {
                    return;
                }

                RegionModel region = new RegionModel();
                region.Name = _viewModel.Name;

                await _store.UpdateRegion(_selected.CurrentRegion.Id, region);
                _navigation.Navigate();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al editar la region", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
