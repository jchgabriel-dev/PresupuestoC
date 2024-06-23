using PresupuestoC.Command.Client;
using PresupuestoC.Command.Location;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Location;
using PresupuestoC.Stores.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Location
{
   
    public class LocationListViewModel : ViewModelBase
    {

        private readonly LocationSelectedStore _locationSelected;

        public bool IsSelected => _locationSelected.IsSelected;
        
        // FILTERS
        public ICollectionView LocationCollection { get; }
   
        private string _regionFilter = string.Empty;
        public string RegionFilter
        {
            get
            {
                return _regionFilter;
            }
            set
            {
                _regionFilter = value;
                OnPropertyChanged(nameof(RegionFilter));
                LocationCollection.Refresh();
            }
        }

        private string _provinceFilter = string.Empty;
        public string ProvinceFilter
        {
            get
            {
                return _provinceFilter;
            }
            set
            {
                _provinceFilter = value;
                OnPropertyChanged(nameof(ProvinceFilter));
                LocationCollection.Refresh();
            }
        }

        private string _districtFilter = string.Empty;
        public string DistrictFilter
        {
            get
            {
                return _districtFilter;
            }
            set
            {
                _districtFilter = value;
                OnPropertyChanged(nameof(DistrictFilter));
                LocationCollection.Refresh();
            }
        }

        // STORES
        private readonly ObservableCollection<DistrictModel> _locations;        
        public IEnumerable<DistrictModel> Locations => _locations;

   

        public DistrictModel Selected
        {
            get => _locationSelected.CurrentLocation;
            set => _locationSelected.CurrentLocation = value;
        }


        public ICommand LoadLocations { get; }
        public ICommand CloseModal { get; }

        public ICommand SelectedLocation { get; }

        // CONSTRUCTOR
        public LocationListViewModel(LocationListStore store, CloseNavigationService close, LocationSelectedStore selected, ProjectTemporalStore project)

        {
            _locations = new ObservableCollection<DistrictModel>();
            _locationSelected = selected;
            _locationSelected.CurrentLocationChanged += LocationChanged;
           

            LoadLocations = new LocationLoadCommand(this, store);
            CloseModal = new CloseNavigateCommand(close);
            SelectedLocation = new LocationSelectedCommand(selected, project, close);

            LocationCollection = CollectionViewSource.GetDefaultView(_locations);
            LocationCollection.Filter = FilterLocation;
            LocationCollection.GroupDescriptions.Add(new PropertyGroupDescription("Province.RegionId"));
            LocationCollection.GroupDescriptions.Add(new PropertyGroupDescription(nameof(DistrictModel.ProvinceId)));

            _locationSelected.Deselected();
        }

        private void LocationChanged()
        {
            OnPropertyChanged(nameof(IsSelected));
            OnPropertyChanged(nameof(Selected));

        }

        public static LocationListViewModel LoadViewModel(LocationListStore store, CloseNavigationService close, LocationSelectedStore selected, ProjectTemporalStore project)
        {

            LocationListViewModel viewModel = new LocationListViewModel(store, close, selected, project);
            viewModel.LoadLocations.Execute(null);
            return viewModel;

        }

        public void UpdateLocations(IEnumerable<DistrictModel> locations)
        {
            _locations.Clear();

            foreach (DistrictModel location in locations)
            {
                _locations.Add(location);
            }


        }

        private bool FilterLocation(object obj)
        {
            if (obj is DistrictModel districtModel)
            {
                return districtModel.Name.Contains(DistrictFilter, StringComparison.InvariantCultureIgnoreCase)
                    && districtModel.Province.Name.Contains(ProvinceFilter, StringComparison.InvariantCultureIgnoreCase)
                    && districtModel.Province.Region.Name.Contains(RegionFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;


        }

    }
}
