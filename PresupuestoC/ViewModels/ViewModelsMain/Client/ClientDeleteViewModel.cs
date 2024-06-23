using PresupuestoC.Command.Client;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Stores.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Client
{
    public class ClientDeleteViewModel : ViewModelBase
    {

        private string _confirmation = string.Empty; 
        public string Confirmation
        {
            get
            {
                return _confirmation;
            }
            set
            {
                _confirmation = value;
                OnPropertyChanged(nameof(Confirmation));
                OnPropertyChanged(nameof(CanDelete));  
            }
        }

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
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool CanDelete => Confirmation.Equals("SI", StringComparison.OrdinalIgnoreCase);

        public ICommand DeleteCurrency { get; }
        public ICommand Cancel { get; }

        // CONSTRUCTOR
        public ClientDeleteViewModel(ClientSelectedStore selectedStore,
            ClientNavigationService<ClientListViewModel> navigate,
            ClientListStore store)
        {
            DeleteCurrency = new ClientDeleteCommand(navigate, store, selectedStore);
            Cancel = new ClientNavigateCommand<ClientListViewModel>(navigate);
            Name = selectedStore.CurrentClient.Name;

        }
    }
}
