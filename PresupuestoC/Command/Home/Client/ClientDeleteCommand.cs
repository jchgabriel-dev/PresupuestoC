using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PresupuestoC.Navigation.Client;
using PresupuestoC.ViewModels.Client;
using PresupuestoC.Stores.Client;

namespace PresupuestoC.Command.Client
{
    public class ClientDeleteCommand : AsyncCommand
    {
        private readonly ClientNavigationService<ClientListViewModel> _navigation;
        private readonly ClientListStore _store;
        private readonly ClientSelectedStore _selected;

        public ClientDeleteCommand(ClientNavigationService<ClientListViewModel> navigation,
            ClientListStore store, ClientSelectedStore selected)
        {
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {

            try
            {
                await _store.DeleteClient(_selected.CurrentClient.Id);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el cliente", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
