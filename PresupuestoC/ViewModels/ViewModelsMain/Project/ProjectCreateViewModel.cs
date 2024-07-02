using PresupuestoC.Command.Project;
using PresupuestoC.Models;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Home2;
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
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Project
{
    public class ProjectCreateViewModel : ViewModelBase, INotifyDataErrorInfo
    {

        // ERRORS

        private readonly ProjectTemporalStore _temporalStore;


        private readonly ErrorsViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool CanCreate => !HasErrors;
        public bool HasErrors => _errorsViewModel.HasErrors;
      
        public FolderModel Folder => _folderSelected.CurrentFolder;                    


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



        private readonly FolderSelectedStore _folderSelected;

        private readonly ModalNavigationStore _modalNavigationStore;
        public bool IsOpen => _modalNavigationStore.IsOpen;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;


        public ICommand NameUpper {  get; }

        public ICommand ClientModalNavigation { get; }
        public ICommand LocationModalNavigation { get; }
        public ICommand CurrencyModalNavigation { get; }

        public ICommand ProjectCreate {  get; }
        public ICommand Cancel { get; }


        // CONSTRUCTOR
        public ProjectCreateViewModel(ModalNavigationStore navigationStore,
            ModalNavigationService<ClientViewModel> navigateClient,
            ModalNavigationService<LocationViewModel> navigateLocation,
            ModalNavigationService<CurrencyViewModel> navigateCurrency,
            ProjectNavigationService<ProjectListViewModel> navigateProjectList,
            ProjectListStore store,
            ProjectTemporalStore creation,
            FolderSelectedStore folder,
            ProjectSelectedStore selected)
        {
            _modalNavigationStore = navigationStore;
            _temporalStore = creation;
            _folderSelected = folder;

            _temporalStore.DeselectedClient();
            _temporalStore.DeselectedCurrency();
            _temporalStore.DeselectedLocation();

            ClientModalNavigation = new ModalNavigateCommand<ClientViewModel>(navigateClient);
            LocationModalNavigation = new ModalNavigateCommand<LocationViewModel>(navigateLocation);
            CurrencyModalNavigation = new ModalNavigateCommand<CurrencyViewModel>(navigateCurrency);
            Cancel = new ProjectNavigateCommand<ProjectListViewModel>(navigateProjectList);

            NameUpper = new ProjectNameUpper(this);
            ProjectCreate = new ProjectCreateCommand(this, navigateProjectList, store);

            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
            
            _temporalStore.CurrentLocationChanged += LocationChanged;
            _temporalStore.CurrentCurrencyChanged += CurrencyChanged;
            _temporalStore.CurrentClientChanged += ClientChanged;
            _folderSelected.CurrentFolderChanged += FolderChanged;

            selected.Deselected();
            FolderChanged();
        }


        public void FolderChanged()
        {
            FolderModel item = _folderSelected.CurrentFolder;

            _errorsViewModel.ClearErrors(nameof(Folder));
            if (item == null)
            {
                _errorsViewModel.AddError(nameof(Folder), "Este campo es obligatorio");
            }

            else if (item.Type == 1)
            {
                _errorsViewModel.AddError(nameof(Folder), "Este carpeta no esta permitida");
            }

            OnPropertyChanged(nameof(Folder));
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
