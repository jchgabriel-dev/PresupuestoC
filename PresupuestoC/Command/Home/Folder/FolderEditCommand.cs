using PresupuestoC.Models.Archive;
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
    public class FolderEditCommand : AsyncCommand
    {
        private readonly FolderEditViewModel _viewModel;
        private readonly CloseNavigationService _navigation;
        private readonly FolderListStore _store;
        private readonly FolderSelectedStore _selected;

        public FolderEditCommand(FolderEditViewModel viewModel, CloseNavigationService navigation, FolderListStore store, FolderSelectedStore selected)
        {

            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            _viewModel.Name = _viewModel.Name;

            if (_viewModel.HasErrors)
            {
                return;
            }

            FolderModel folder = new FolderModel();
            folder.Name = _viewModel.Name;
            folder.Type = 3;
            folder.ParentId = _selected.CurrentFolder.ParentId;


            try
            {
                await _store.UpdateFolder(_selected.CurrentFolder.Id, folder);
                _navigation.Navigate();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar la carpeta", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
