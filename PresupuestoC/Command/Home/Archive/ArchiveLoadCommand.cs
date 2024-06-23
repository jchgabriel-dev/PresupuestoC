using PresupuestoC.MVVM;
using PresupuestoC.Stores.Archive;
using PresupuestoC.Stores.Client;
using PresupuestoC.ViewModels.Client;
using PresupuestoC.ViewModels.Home2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Archive
{
    public class ArchiveLoadCommand : AsyncCommand
    {
        private readonly ArchivesViewModel _viewModel;
        private readonly ArchivesListStore _store;


        public ArchiveLoadCommand(ArchivesViewModel viewModel, ArchivesListStore store)
        {
            _viewModel = viewModel;
            _store = store;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.Load();
                _viewModel.UpdateArchives(_store.Archives);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos relacionados con los archivos", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
