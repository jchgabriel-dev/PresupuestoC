using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Client;
using PresupuestoC.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Client
{
    public class ClientCreateCommand : AsyncCommand
    {
        private readonly ClientCreateViewModel _viewModel;
        private readonly ClientNavigationService<ClientListViewModel> _navigation;
        private readonly ClientListStore _store;

        public ClientCreateCommand(ClientCreateViewModel viewModel, ClientNavigationService<ClientListViewModel> navigation, ClientListStore store)
        {

            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
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
                await _store.CreateClient(client);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el cliente", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
