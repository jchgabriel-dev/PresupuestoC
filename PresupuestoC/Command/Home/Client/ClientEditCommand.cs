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
using PresupuestoC.ViewModels.Client;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Stores.Client;
using PresupuestoC.Models.Main;

namespace PresupuestoC.Command.Client
{
    public class ClientEditCommand : AsyncCommand
    {
        private readonly ClientEditViewModel _viewModel;
        private readonly ClientNavigationService<ClientListViewModel> _navigation;
        private readonly ClientListStore _store;
        private readonly ClientSelectedStore _selected;

        public ClientEditCommand(ClientEditViewModel viewModel, ClientNavigationService<ClientListViewModel> navigation,
            ClientListStore store, ClientSelectedStore selected)
        {
            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            _viewModel.Name = _viewModel.Name;
            _viewModel.Address = _viewModel.Address;

            if (_viewModel.HasErrors)
            {
                return;
            }

            ClientModel client = new ClientModel();
            client.Name = _viewModel.Name;
            client.Address = _viewModel.Address;


            try
            {
                await _store.UpdateClient(_selected.CurrentClient.Id, client);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el cliente", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
