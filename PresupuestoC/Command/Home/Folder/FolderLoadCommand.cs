using PresupuestoC.MVVM;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Folder;
using PresupuestoC.ViewModels.Currency;
using PresupuestoC.ViewModels.Home2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Folder
{
    public class FolderLoadCommand : AsyncCommand
    {
        private readonly ProjectViewModel _viewModel;
        private readonly FolderListStore _store;


        public FolderLoadCommand(ProjectViewModel viewModel, FolderListStore store)
        {
            _viewModel = viewModel;
            _store = store;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.Load();
                _viewModel.UpdateFolders(_store.Folders);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
