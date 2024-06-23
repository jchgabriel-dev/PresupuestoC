using PresupuestoC.Command.Client;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Client
{
    public class ClientListViewModel : ViewModelBase
    {        

        // FILTERS
        public ICollectionView ClientCollection { get; }
        private string _clientFilterName = string.Empty;
        public string ClientFilterName
        {
            get
            {
                return _clientFilterName;
            }
            set
            {
                _clientFilterName = value;
                OnPropertyChanged(nameof(ClientFilterName));
                ClientCollection.Refresh();
            }
        }
        private string _clientFilterAddress = string.Empty;
        public string ClientFilterAddress
        {
            get
            {
                return _clientFilterAddress;
            }
            set
            {
                _clientFilterAddress = value;
                OnPropertyChanged(nameof(_clientFilterAddress));
                ClientCollection.Refresh();
            }
        }

        // STORES
        private readonly ObservableCollection<ClientModel> _clients;
        private ClientSelectedStore _selectedStore;     

        public ClientModel Selected
        {
            get => _selectedStore.CurrentClient;
            set => _selectedStore.CurrentClient = value;           
        }

        public bool IsSelected => _selectedStore.IsSelected;

       
        public IEnumerable<ClientModel> Clients => _clients;

        public ICommand LoadClients { get; }
        public ICommand CloseModal { get; }

        public ICommand SelectedClient { get; }


        // CONSTRUCTOR
        public ClientListViewModel(ClientListStore store,
            ClientSelectedStore selectedStore,
            CloseNavigationService close,
            ProjectTemporalStore project)

        {
            _selectedStore = selectedStore;
            _selectedStore.CurrentCurrencyChanged += ClientChanged;
            _clients = new ObservableCollection<ClientModel>();

            LoadClients = new ClientLoadCommand(this, store);
            CloseModal = new CloseNavigateCommand(close);
            SelectedClient = new ClientSelectedCommand(selectedStore, project, close);

            ClientCollection = CollectionViewSource.GetDefaultView(_clients);
            ClientCollection.Filter = FilterClient;
            _selectedStore.Deselected();
        }


        private void ClientChanged()
        {
            OnPropertyChanged(nameof(Selected));
            OnPropertyChanged(nameof(IsSelected));

        }

        public static ClientListViewModel LoadViewModel(ClientListStore store, ClientSelectedStore selectedStore, CloseNavigationService close, ProjectTemporalStore project)
        {
            ClientListViewModel viewModel = new ClientListViewModel(store, selectedStore, close, project);
            viewModel.LoadClients.Execute(null);
            return viewModel;

        }

        public void UpdateClients(IEnumerable<ClientModel> clients)
        {
            _clients.Clear();

            foreach (ClientModel client in clients)
            {
                _clients.Add(client);
            }
        }

        private bool FilterClient(object obj)
        {
            if (obj is ClientModel clientModel)
            {
                return clientModel.Name.Contains(ClientFilterName, StringComparison.InvariantCultureIgnoreCase)
                    && clientModel.Address.Contains(ClientFilterAddress, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
