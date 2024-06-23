using PresupuestoC.Command.Currency;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresupuestoC.Command.Client;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Stores.Client;

namespace PresupuestoC.ViewModels.Client
{
    public class ClientEditViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly ErrorsViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool CanCreate => !HasErrors;
        public bool HasErrors => _errorsViewModel.HasErrors;


        private readonly ClientSelectedStore _clientStore;
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

        public ICommand EditClient { get; }
        public ICommand Cancel { get; }


        // CONSTRUCTOR
        public ClientEditViewModel(ClientSelectedStore selectedStore,
            ClientNavigationService<ClientListViewModel> navigate,
            ClientListStore store)
        {
            EditClient = new ClientEditCommand(this, navigate, store, selectedStore);
            Cancel = new ClientNavigateCommand<ClientListViewModel>(navigate);

            _clientStore = selectedStore;
            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            Name = _clientStore.CurrentClient.Name;
            Address = _clientStore.CurrentClient.Address;
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
