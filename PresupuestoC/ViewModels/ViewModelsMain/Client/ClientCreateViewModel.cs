using PresupuestoC.Command.Currency;
using PresupuestoC.MVVM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresupuestoC.Stores.Client;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Command.Client;

namespace PresupuestoC.ViewModels.Client
{
    public class ClientCreateViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private ClientSelectedStore _selectedStore;

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

        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                _errorsViewModel.ClearErrors(nameof(Address));
                if (string.IsNullOrWhiteSpace(_address))
                {
                    _errorsViewModel.AddError(nameof(Address), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Address));

            }
        }

        public ICommand CreateClient { get; }
        public ICommand Cancel { get; }


        // CONSTRUCTOR
        public ClientCreateViewModel(ClientSelectedStore selectedStore,
            ClientNavigationService<ClientListViewModel> navigate,
            ClientListStore store)
        {
            CreateClient = new ClientCreateCommand(this, navigate, store);
            Cancel = new ClientNavigateCommand<ClientListViewModel>(navigate);

            _selectedStore = selectedStore;
            _errorsViewModel = new ErrorsViewModel();

            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
            _selectedStore.Deselected();

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
