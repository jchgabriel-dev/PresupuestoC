using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.ViewModels.Location.District;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Location
{
    public class DistrictCreateCommand : AsyncCommand
    {
        private readonly LocationCreateDistrictViewModel _viewModel;
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;

        public DistrictCreateCommand(LocationCreateDistrictViewModel viewModel, SuperCloseNavigationService navigation, LocationListStore store)
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
                _viewModel.Province = _viewModel.Province;
                _viewModel.ProvinceFilterRegion = _viewModel.ProvinceFilterRegion;

                if (_viewModel.HasErrors)
                {
                    return;
                }

                DistrictModel district = new DistrictModel();
                district.Name = _viewModel.Name;
                district.ProvinceId = _viewModel.Province.Id;

                await _store.CreateDistrict(district, _viewModel.Province);
                _navigation.Navigate();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el distrito", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
