using PresupuestoC.Command.Client;
using PresupuestoC.Command.Location;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Location;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Location.Province
{
    public class LocationCreateProvinceViewModel : ViewModelBase, INotifyDataErrorInfo
    {

        // STORES
        private readonly ObservableCollection<RegionModel> _regions;
        public IEnumerable<RegionModel> Regions => _regions;



        // ERRORS
        private readonly ErrorsViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool CanCreate => !HasErrors;
        public bool HasErrors => _errorsViewModel.HasErrors;


        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                _errorsViewModel.ClearErrors(nameof(Name));
                if (string.IsNullOrWhiteSpace(_name))
                {
                    _errorsViewModel.AddError(nameof(Name), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Name));

            }
        }

        private RegionModel _region;
        public RegionModel Region
        {
            get
            {
                return _region;
            }
            set
            {
                _region = value;
                _errorsViewModel.ClearErrors(nameof(Region));
                if (_region == null)
                {
                    _errorsViewModel.AddError(nameof(Region), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Region));

            }
        }

        public ICommand CreateProvince{ get; }
        public ICommand Cancel { get; }



        // CONSTRUCTOR
        public LocationCreateProvinceViewModel(
            SuperCloseNavigationService navigate,
            LocationListStore store)
        {
            _regions = new ObservableCollection<RegionModel>();

            CreateProvince = new ProvinceCreateCommand(this, navigate, store);
            Cancel = new SuperCloseNavigateCommand(navigate);

            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
        }

        public static LocationCreateProvinceViewModel LoadViewModel(SuperCloseNavigationService navigate, LocationListStore store)
        {
            LocationCreateProvinceViewModel viewModel = new LocationCreateProvinceViewModel(navigate, store);
            viewModel.UpdateRegions(store.Regions);
            return viewModel;

        }

        public void UpdateRegions(IEnumerable<RegionModel> regions)
        {
            _regions.Clear();

            foreach (RegionModel region in regions)
            {
                _regions.Add(region);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanCreate));
        }
    }
}
