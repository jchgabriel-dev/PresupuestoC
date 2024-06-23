using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Folder;
using PresupuestoC.ViewModels.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Folder
{
    public class FolderDeleteCommand : AsyncCommand
    {
        private readonly CloseNavigationService _navigation;
        private readonly FolderListStore _store;
        private readonly FolderSelectedStore _selected;

        public FolderDeleteCommand(CloseNavigationService navigation, FolderListStore store, FolderSelectedStore selected)
        {            
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {
                
            try
            {
                await _store.DeleteFolder(_selected.CurrentFolder.Id);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la carpeta", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
