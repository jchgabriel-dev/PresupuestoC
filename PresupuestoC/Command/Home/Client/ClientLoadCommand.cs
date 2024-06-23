using PresupuestoC.MVVM;
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
    public class ClientLoadCommand : AsyncCommand
    {
        private readonly ClientListViewModel _viewModel;
        private readonly ClientListStore _store;


        public ClientLoadCommand(ClientListViewModel viewModel, ClientListStore store)
        {
            _viewModel = viewModel;
            _store = store;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.Load();
                _viewModel.UpdateClients(_store.Clients);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos relacionados con los clientes", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
