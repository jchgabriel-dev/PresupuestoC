using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.ViewModels.Location.Province;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Location
{
    public class ProvinceCreateCommand : AsyncCommand
    {
        private readonly LocationCreateProvinceViewModel _viewModel;
        private readonly SuperCloseNavigationService _navigation;
        private readonly LocationListStore _store;

        public ProvinceCreateCommand(LocationCreateProvinceViewModel viewModel, SuperCloseNavigationService navigation, LocationListStore store)
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
                _viewModel.Region = _viewModel.Region;

                if (_viewModel.HasErrors)
                {
                    return;
                }

                ProvinceModel province = new ProvinceModel();
                province.Name = _viewModel.Name;              
                province.RegionId = _viewModel.Region.Id;

                await _store.CreateProvince(province, _viewModel.Region);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la provincia", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
