using PresupuestoC.Command.Location;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location.District;
using PresupuestoC.ViewModels.Location.Province;
using PresupuestoC.ViewModels.Location.Region;
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

namespace PresupuestoC.ViewModels.Location
{


    public class LocationRegisterViewModel : ViewModelBase
    {
        private LocationListStore _locationStore;
        // FILTERS

        private RegionSelectedStore _selectedRegion;
        public bool IsRegionSelected => _selectedRegion.IsSelected;

        private ProvinceSelectedStore _selectedProvince;
        public bool IsProvinceSelected => _selectedProvince.IsSelected;

        private DistrictSelectedStore _selectedDistrict;
        public bool IsDistrictSelected => _selectedDistrict.IsSelected;


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
                _selectedRegion.CurrentRegion = _provinceFilterRegion;
                OnPropertyChanged(nameof(ProvinceFilterRegion));
                ProvinceCollection.Refresh();
            }
        }

        public ICollectionView DistrictCollection { get; }
        private ProvinceModel _districtFilterProvince = new ProvinceModel();
        public ProvinceModel DistrictFilterProvince
        {
            get
            {
                return _districtFilterProvince;
            }
            set
            {
                _districtFilterProvince = value;
                _selectedProvince.CurrentProvince = _districtFilterProvince;
                OnPropertyChanged(nameof(DistrictFilterProvince));
                DistrictCollection.Refresh();
            }
        }

        public DistrictModel District
        {
            get => _selectedDistrict.CurrentDistrict;
            set => _selectedDistrict.CurrentDistrict = value;
        }





        // STORES
        private readonly ObservableCollection<RegionModel> _regions;
        public IEnumerable<RegionModel> Regions => _regions;

        private readonly ObservableCollection<ProvinceModel> _provinces;
        public IEnumerable<ProvinceModel> Provinces => _provinces;

        private readonly ObservableCollection<DistrictModel> _districts;
        public IEnumerable<DistrictModel> Districts => _districts;



        public ICommand Cancel { get; }

        public ICommand RegionCreateNavigation { get; }
        public ICommand RegionEditNavigation { get; }
        public ICommand RegionDeleteNavigation { get; }
        public ICommand ProvinceCreateNavigation { get; }
        public ICommand ProvinceEditNavigation { get; }
        public ICommand ProvinceDeleteNavigation { get; }
        public ICommand DistrictCreateNavigation { get; }
        public ICommand DistrictEditNavigation { get; }
        public ICommand DistrictDeleteNavigation { get; }



        // CONSTRUCTOR
        public LocationRegisterViewModel(
            LocationNavigationService<LocationListViewModel> navigate,
            LocationListStore store,   
            RegionSelectedStore regionSelected,
            ProvinceSelectedStore provinceSelected,
            DistrictSelectedStore districtSelected,

            SuperModalNavigationService<LocationCreateRegionViewModel> navigateRegionCreate,
            SuperModalNavigationService<LocationEditRegionViewModel> navigateRegionEdit,
            SuperModalNavigationService<LocationDeleteRegionViewModel> navigateRegionDelete,

            SuperModalNavigationService<LocationCreateProvinceViewModel> navigateProvinceCreate,
            SuperModalNavigationService<LocationEditProvinceViewModel> navigateProvinceEdit,
            SuperModalNavigationService<LocationDeleteProvinceViewModel> navigateProvinceDelete,

            SuperModalNavigationService<LocationCreateDistrictViewModel> navigateDistrictCreate,
            SuperModalNavigationService<LocationEditDistrictViewModel> navigateDistrictEdit,
            SuperModalNavigationService<LocationDeleteDistrictViewModel> navigateDistrictDelete)
        {
            RegionCreateNavigation = new SuperModalNavigateCommand<LocationCreateRegionViewModel>(navigateRegionCreate);
            RegionEditNavigation = new SuperModalNavigateCommand<LocationEditRegionViewModel>(navigateRegionEdit);
            RegionDeleteNavigation = new SuperModalNavigateCommand<LocationDeleteRegionViewModel>(navigateRegionDelete);

            ProvinceCreateNavigation = new SuperModalNavigateCommand<LocationCreateProvinceViewModel>(navigateProvinceCreate);
            ProvinceEditNavigation = new SuperModalNavigateCommand<LocationEditProvinceViewModel>(navigateProvinceEdit);
            ProvinceDeleteNavigation = new SuperModalNavigateCommand<LocationDeleteProvinceViewModel>(navigateProvinceDelete);

            DistrictCreateNavigation = new SuperModalNavigateCommand<LocationCreateDistrictViewModel>(navigateDistrictCreate);
            DistrictEditNavigation = new SuperModalNavigateCommand<LocationEditDistrictViewModel>(navigateDistrictEdit);
            DistrictDeleteNavigation = new SuperModalNavigateCommand<LocationDeleteDistrictViewModel>(navigateDistrictDelete);


            _provinceFilterRegion = null;

            _locationStore = store;
            _locationStore.Changes += LocationChanges;

            _regions = new ObservableCollection<RegionModel>();            
            _provinces = new ObservableCollection<ProvinceModel>();
            _districts = new ObservableCollection<DistrictModel>();

            _selectedRegion = regionSelected;
            _selectedRegion.Deselected();
            _selectedRegion.CurrentRegionChanged += RegionChanged;

            _selectedProvince = provinceSelected;
            _selectedProvince.Deselected();
            _selectedProvince.CurrentProvinceChanged += ProvinceChanged;

            _selectedDistrict = districtSelected;
            _selectedDistrict.Deselected();
            _selectedDistrict.CurrentDistrictChanged += DistrictChanged;

            Cancel = new LocationNavigateCommand<LocationListViewModel>(navigate);

     

            ProvinceCollection = CollectionViewSource.GetDefaultView(_provinces);
            ProvinceCollection.Filter = FilterProvince;

            DistrictCollection = CollectionViewSource.GetDefaultView(_districts);
            DistrictCollection.Filter = FilterDistrict;
        }

        private void LocationChanges()
        {
            UpdateData(_locationStore.Regions, _locationStore.Provinces, _locationStore.Districts);
        }


        private void RegionChanged()
        {
            OnPropertyChanged(nameof(IsRegionSelected));
            OnPropertyChanged(nameof(ProvinceFilterRegion));

        }

        private void ProvinceChanged()
        {
            OnPropertyChanged(nameof(IsProvinceSelected));
            OnPropertyChanged(nameof(DistrictFilterProvince));

        }

        private void DistrictChanged()
        {
            OnPropertyChanged(nameof(IsDistrictSelected));
            OnPropertyChanged(nameof(District));

        }

        public static LocationRegisterViewModel LoadViewModel(
            LocationNavigationService<LocationListViewModel> navigate,
            LocationListStore store,
            RegionSelectedStore regionSelected,
            ProvinceSelectedStore provinceSelected,
            DistrictSelectedStore districtSelected,

            SuperModalNavigationService<LocationCreateRegionViewModel> navigateRegionCreate,
            SuperModalNavigationService<LocationEditRegionViewModel> navigateRegionEdit,
            SuperModalNavigationService<LocationDeleteRegionViewModel> navigateRegionDelete,

            SuperModalNavigationService<LocationCreateProvinceViewModel> navigateProvinceCreate,
            SuperModalNavigationService<LocationEditProvinceViewModel> navigateProvinceEdit,
            SuperModalNavigationService<LocationDeleteProvinceViewModel> navigateProvinceDelete,

            SuperModalNavigationService<LocationCreateDistrictViewModel> navigateDistrictCreate,
            SuperModalNavigationService<LocationEditDistrictViewModel> navigateDistrictEdit,
            SuperModalNavigationService<LocationDeleteDistrictViewModel> navigateDistrictDelete)
        {
            LocationRegisterViewModel viewModel = new LocationRegisterViewModel(
                navigate, 
                store,
                regionSelected,
                provinceSelected,
                districtSelected,
                navigateRegionCreate,
                navigateRegionEdit,
                navigateRegionDelete,
                navigateProvinceCreate,
                navigateProvinceEdit,
                navigateProvinceDelete,
                navigateDistrictCreate,
                navigateDistrictEdit,
                navigateDistrictDelete);

            viewModel.UpdateData(store.Regions, store.Provinces, store.Districts);
            return viewModel;

        }


        public void UpdateData(IEnumerable<RegionModel> regions, IEnumerable<ProvinceModel> provinces, IEnumerable<DistrictModel> districts)
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

            _districts.Clear();

            foreach (DistrictModel district in districts)
            {
                _districts.Add(district);
            }
        }

            
        private bool FilterProvince(object obj)
        {
            if (obj is ProvinceModel provinceModel)
            {
                return provinceModel.RegionId.Equals(ProvinceFilterRegion?.Id);
            }
            return false;


        }

        private bool FilterDistrict(object obj)
        {
            if (obj is DistrictModel districtModel)
            {
                return districtModel.ProvinceId.Equals(DistrictFilterProvince?.Id);
            }
            return false;
        }
    }
}
