using PresupuestoC.MVVM;
using PresupuestoC.Stores.Location;
using PresupuestoC.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Location
{
    public class LocationLoadCommand : AsyncCommand
    {
        private readonly LocationListViewModel _viewModel;
        private readonly LocationListStore _store;

        public LocationLoadCommand(LocationListViewModel viewModel, LocationListStore store)
        {
            _viewModel = viewModel;
            _store = store;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.Load();
                _viewModel.UpdateLocations(_store.Districts);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos relacionados con ubicaciones", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
