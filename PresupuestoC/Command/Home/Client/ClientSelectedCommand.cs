using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Client
{
    public class ClientSelectedCommand : AsyncCommand
    {
        private readonly ClientSelectedStore _selected;
        private readonly ProjectTemporalStore _project;
        private readonly CloseNavigationService _navigate;


        public ClientSelectedCommand(ClientSelectedStore selected, ProjectTemporalStore project, CloseNavigationService navigate)
        {
            _selected = selected;
            _project = project;
            _navigate = navigate;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                if (_selected.CurrentClient == null)
                    return;

                _project.CurrentClient = _selected.CurrentClient;
                _navigate.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar la moneda", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
