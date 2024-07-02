using PresupuestoC.Command.Project;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.Project;
using PresupuestoC.Stores.Folder;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Home2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresupuestoC.Command.Home.Project;

namespace PresupuestoC.ViewModels.Project
{
 
    public class ProjectEditViewModel : ViewModelBase, INotifyDataErrorInfo
    {

        private ProjectSelectedStore _selected;

        // ERRORS

        private readonly ProjectTemporalStore _temporalStore;

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

        private string _place;
        public string Place
        {
            get => _place;
            set => _place = value;
        }


        public ClientModel Client => _temporalStore.CurrentClient;
        public DistrictModel Location => _temporalStore.CurrentDistrict;



        private DateTime? _date;
        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                _errorsViewModel.ClearErrors(nameof(Date));
                if (_date == null)
                {
                    _errorsViewModel.AddError(nameof(Date), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Date));

            }
        }

        private string _work;
        public string Work
        {
            get
            {
                return _work;
            }
            set
            {
                _work = value;
                _errorsViewModel.ClearErrors(nameof(Work));
                if (string.IsNullOrWhiteSpace(_work))
                {
                    _errorsViewModel.AddError(nameof(Work), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Work));

            }
        }

        private string _group;
        public string Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
                _errorsViewModel.ClearErrors(nameof(Group));
                if (string.IsNullOrWhiteSpace(_group))
                {
                    _errorsViewModel.AddError(nameof(Group), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Group));

            }
        }


        public CurrencyModel Currency => _temporalStore.CurrentCurrency;


        private bool _IGV;

        public bool IGV
        {
            get => _IGV;
            set => _IGV = value;
        }

        private string _left;
        public string Left
        {
            get => _left;
            set => _left = value;
        }

        private string _right;
        public string Right
        {
            get => _right;
            set => _right = value;
        }



        private readonly ModalNavigationStore _modalNavigationStore;
        public bool IsOpen => _modalNavigationStore.IsOpen;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;


        public ICommand NameUpper { get; }

        public ICommand ClientModalNavigation { get; }
        public ICommand LocationModalNavigation { get; }
        public ICommand CurrencyModalNavigation { get; }

        public ICommand ProjectEdit { get; }

        public ICommand Cancel { get; }

        // CONSTRUCTOR
        public ProjectEditViewModel(ModalNavigationStore navigationStore,
            ModalNavigationService<ClientViewModel> navigateClient,
            ModalNavigationService<LocationViewModel> navigateLocation,
            ModalNavigationService<CurrencyViewModel> navigateCurrency,
            ProjectNavigationService<ProjectListViewModel> navigateProjectList,
            ProjectListStore store,
            ProjectSelectedStore selected,
            ProjectTemporalStore creation)
        {
            _modalNavigationStore = navigationStore;
            _temporalStore = creation;
            _selected = selected;

            _temporalStore.DeselectedClient();
            _temporalStore.DeselectedCurrency();
            _temporalStore.DeselectedLocation();

            ClientModalNavigation = new ModalNavigateCommand<ClientViewModel>(navigateClient);
            LocationModalNavigation = new ModalNavigateCommand<LocationViewModel>(navigateLocation);
            CurrencyModalNavigation = new ModalNavigateCommand<CurrencyViewModel>(navigateCurrency);
            Cancel = new ProjectNavigateCommand<ProjectListViewModel>(navigateProjectList);

            NameUpper = new ProjectNameUpperAlt(this);
            ProjectEdit = new ProjectEditCommand(this, navigateProjectList, store, selected);

            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            _temporalStore.CurrentLocationChanged += LocationChanged;
            _temporalStore.CurrentCurrencyChanged += CurrencyChanged;
            _temporalStore.CurrentClientChanged += ClientChanged;

            

            Name = _selected.CurrentProject.Name;
            Date = _selected.CurrentProject.Date.ToDateTime(TimeOnly.MinValue);
            Group = _selected.CurrentProject.Group;
            Work =  Convert.ToString(_selected.CurrentProject.Work);
            Place = _selected.CurrentProject.Place;
            IGV = _selected.CurrentProject.IGV;            
            Left = _selected.CurrentProject.LetterheadLeft;
            Right = _selected.CurrentProject.LetterheadRight;

            ClientModel? client = new ClientModel();
            client.Name = _selected.CurrentProject.ClientArchive?.Name;
            client.Address = _selected.CurrentProject.ClientArchive?.Address;
            _temporalStore.CurrentClient = client;

            CurrencyModel? currency = new CurrencyModel();
            currency.Symbol = _selected.CurrentProject.CurrencyArchive?.Symbol;
            currency.Description = _selected.CurrentProject.CurrencyArchive?.Description;
            _temporalStore.CurrentCurrency = currency;

            DistrictModel? district = new DistrictModel();
            ProvinceModel? province = new ProvinceModel();
            RegionModel? region = new RegionModel();

            district.Name = _selected.CurrentProject.LocationArchive?.District;
            province.Name = _selected.CurrentProject.LocationArchive?.Province;
            region.Name = _selected.CurrentProject.LocationArchive?.Region;

            district.Province = province;
            province.Region = region;           
            _temporalStore.CurrentDistrict = district;

        }

        public void CurrencyChanged()
        {
            CurrencyModel item = _temporalStore.CurrentCurrency;
            _errorsViewModel.ClearErrors(nameof(Currency));
            if (item == null)
            {
                _errorsViewModel.AddError(nameof(Currency), "Este campo es obligatorio");
            }
            OnPropertyChanged(nameof(Currency));
        }


        public void LocationChanged()
        {
            DistrictModel item = _temporalStore.CurrentDistrict;
            _errorsViewModel.ClearErrors(nameof(Location));
            if (item == null)
            {
                _errorsViewModel.AddError(nameof(Location), "Este campo es obligatorio");
            }
            OnPropertyChanged(nameof(Location));

        }
        public void ClientChanged()
        {

            ClientModel item = _temporalStore.CurrentClient;

            _errorsViewModel.ClearErrors(nameof(Client));
            if (item == null)
            {
                _errorsViewModel.AddError(nameof(Client), "Este campo es obligatorio");
            }
            OnPropertyChanged(nameof(Client));

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
