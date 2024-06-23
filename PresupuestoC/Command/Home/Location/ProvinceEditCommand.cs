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
using PresupuestoC.ViewModels.Location.Province;
using PresupuestoC.Navigation.SuperModal;

namespace PresupuestoC.Command.Location
{
    public class ProvinceEditCommand : AsyncCommand
    {
        private readonly LocationEditProvinceViewModel _viewModel;
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;
        private readonly ProvinceSelectedStore _selected;


        public ProvinceEditCommand(LocationEditProvinceViewModel viewModel, SuperCloseNavigationService navigation, LocationListStore store, ProvinceSelectedStore selected)
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
                _viewModel.Region = _viewModel.Region;
           
                if (_viewModel.HasErrors)
                {
                    return;
                }

                ProvinceModel province = new ProvinceModel();
                province.Name = _viewModel.Name;               
                province.RegionId = _viewModel.Region.Id;

                await _store.UpdateProvince(_selected.CurrentProvince.Id, province, _viewModel.Region);
                _navigation.Navigate();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al editar la provincia", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
