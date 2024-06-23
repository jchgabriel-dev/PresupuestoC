using PresupuestoC.Command.Location;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Location;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Location.District
{
   
    public class LocationCreateDistrictViewModel : ViewModelBase, INotifyDataErrorInfo
    {

        // FILTERS
        public ICollectionView ProvinceCollection { get; }
        private RegionModel _provinceFilterRegion = new RegionModel();
        public RegionModel ProvinceFilterRegion
        {
            get
            {
                return _provinceFilterRegion;
            }
            set
            {
                _provinceFilterRegion = value;
                _errorsViewModel.ClearErrors(nameof(ProvinceFilterRegion));
                if (_provinceFilterRegion == null || _provinceFilterRegion.Id == 0)
                {
                    _errorsViewModel.AddError(nameof(ProvinceFilterRegion), "Este campo es obligatorio");
                }

                OnPropertyChanged(nameof(ProvinceFilterRegion));
                ProvinceCollection.Refresh();
            }
        }


        // STORES
        private readonly ObservableCollection<RegionModel> _regions;
        public IEnumerable<RegionModel> Regions => _regions;

        private readonly ObservableCollection<ProvinceModel> _provinces;
        public IEnumerable<ProvinceModel> Provinces => _provinces;


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

       

        private ProvinceModel _province;
        public ProvinceModel Province
        {
            get
            {
                return _province;
            }
            set
            {
                _province = value;
                _errorsViewModel.ClearErrors(nameof(Province));
                if (_province == null)
                {
                    _errorsViewModel.AddError(nameof(Province), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Province));

            }
        }

        public ICommand CreateDistrict { get; }
        public ICommand Cancel { get; }
      




        // CONSTRUCTOR
        public LocationCreateDistrictViewModel(SuperCloseNavigationService navigate,
            LocationListStore store)
        {
            _regions = new ObservableCollection<RegionModel>();
            _provinces = new ObservableCollection<ProvinceModel>();

            Cancel = new SuperCloseNavigateCommand(navigate);
            CreateDistrict = new DistrictCreateCommand(this, navigate, store);

            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            ProvinceCollection = CollectionViewSource.GetDefaultView(_provinces);
            ProvinceCollection.Filter = FilterProvince;


        }

        public static LocationCreateDistrictViewModel LoadViewModel(SuperCloseNavigationService navigate,
            LocationListStore store)
        {
            LocationCreateDistrictViewModel viewModel = new LocationCreateDistrictViewModel(navigate, store);
            viewModel.UpdateData(store.Regions, store.Provinces);

            return viewModel;

        }

        public void UpdateData(IEnumerable<RegionModel> regions, IEnumerable<ProvinceModel> provinces)
        {
            _regions.Clear();

            foreach (RegionModel region in regions)
            {
                _regions.Add(region);
            }
            _provinces.Clear();

            foreach (ProvinceModel province in provinces)
            {
                _provinces.Add(province);
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

        private bool FilterProvince(object obj)
        {
            if (obj is ProvinceModel provinceModel)
            {
                return provinceModel.RegionId.Equals(ProvinceFilterRegion.Id);
            }
            return false;


        }
    }
}
