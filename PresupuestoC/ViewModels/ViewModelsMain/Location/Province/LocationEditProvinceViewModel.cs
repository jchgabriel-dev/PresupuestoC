using PresupuestoC.Command.Location;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Client;
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
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Location.Province
{
    public class LocationEditProvinceViewModel : ViewModelBase, INotifyDataErrorInfo
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

        public ICommand EditProvince { get; }
        public ICommand Cancel { get; }



        // CONSTRUCTOR
        public LocationEditProvinceViewModel(SuperCloseNavigationService navigate,
            LocationListStore store, ProvinceSelectedStore selected)
        {
            _regions = new ObservableCollection<RegionModel>();
            _errorsViewModel = new ErrorsViewModel();


            Cancel = new SuperCloseNavigateCommand(navigate);
            EditProvince = new ProvinceEditCommand(this, navigate, store, selected);

            Name = selected.CurrentProvince.Name;
            Region = selected.CurrentProvince.Region;

            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
           
        }

        public static LocationEditProvinceViewModel LoadViewModel(SuperCloseNavigationService navigate, LocationListStore store, ProvinceSelectedStore selected)
        {
            LocationEditProvinceViewModel viewModel = new LocationEditProvinceViewModel(navigate, store, selected);
            viewModel.UpdateRegions(store.Regions, store.Provinces);
            return viewModel;
        }

        public void UpdateRegions(IEnumerable<RegionModel> regions, IEnumerable<ProvinceModel> provinces)
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
