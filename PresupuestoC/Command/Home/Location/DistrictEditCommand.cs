using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location.District;
using PresupuestoC.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PresupuestoC.Stores.Client;
using PresupuestoC.Navigation.SuperModal;

namespace PresupuestoC.Command.Location
{
    public class DistrictEditCommand : AsyncCommand
    {
        private readonly LocationEditDistrictViewModel _viewModel;
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;
        private readonly DistrictSelectedStore _selected;


        public DistrictEditCommand(LocationEditDistrictViewModel viewModel, SuperCloseNavigationService navigation, 
            LocationListStore store, DistrictSelectedStore selected)
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
                _viewModel.Province = _viewModel.Province;

                if (_viewModel.HasErrors)
                {
                    return;
                }

                DistrictModel district = new DistrictModel();
                district.Name = _viewModel.Name;
                district.ProvinceId = _viewModel.Province.Id;

                await _store.UpdateDistrict(_selected.CurrentDistrict.Id ,district, _viewModel.Province);
                _navigation.Navigate();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el distrito", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
