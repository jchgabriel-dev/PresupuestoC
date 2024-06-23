using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;

using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Home2
{
    public class ClientViewModel : ViewModelBase
    {

        // SELECTED
        private readonly ClientSelectedStore _selectedStore;
        public ClientModel SelectedStore => _selectedStore.CurrentClient;
        public bool IsSelected => (_selectedStore.IsSelected);


        // NAVIGATION
        private readonly ClientNavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand CloseModal { get; }    
        public ICommand ClientListNavigation { get; }
        public ICommand ClientCreateNavigation { get; }
        public ICommand ClientEditNavigation { get; }
        public ICommand ClientDeleteNavigation { get; }

        public ClientViewModel(ClientNavigationStore navigationStore,
            ClientNavigationService<ClientListViewModel> navigateClientList,
            ClientNavigationService<ClientCreateViewModel> navigateClientCreate,
            ClientNavigationService<ClientEditViewModel> navigateClientEdit,
            ClientNavigationService<ClientDeleteViewModel> navigateClientDelete,
            CloseNavigationService close, 
            ClientSelectedStore selectedStore,
            ProjectTemporalStore project)
        {
            project.DeselectedClient();
            _navigationStore = navigationStore;
            _selectedStore = selectedStore;
            ClientListNavigation = new ClientNavigateCommand<ClientListViewModel>(navigateClientList);
            ClientCreateNavigation = new ClientNavigateCommand<ClientCreateViewModel>(navigateClientCreate);
            ClientEditNavigation = new ClientNavigateCommand<ClientEditViewModel>(navigateClientEdit);
            ClientDeleteNavigation = new ClientNavigateCommand<ClientDeleteViewModel>(navigateClientDelete);

            CloseModal = new CloseNavigateCommand(close);
            
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _selectedStore.CurrentCurrencyChanged += OnCurrentCurrencyChanged;

            ClientListNavigation.Execute(null);

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentCurrencyChanged()
        {
            OnPropertyChanged(nameof(SelectedStore));
            OnPropertyChanged(nameof(IsSelected));

        }

    }

}
