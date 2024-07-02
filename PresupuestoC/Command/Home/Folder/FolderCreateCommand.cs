using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Folder;
using PresupuestoC.ViewModels.Currency;
using PresupuestoC.ViewModels.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Folder
{
    public class FolderCreateCommand : AsyncCommand
    {
        private readonly FolderCreateViewModel _viewModel;
        private readonly CloseNavigationService _navigation;
        private readonly FolderListStore _store;
        private readonly FolderSelectedStore _selected;

        public FolderCreateCommand(FolderCreateViewModel viewModel, CloseNavigationService navigation, FolderListStore store, FolderSelectedStore selected)
        {

            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {
            
            try
            {
                _viewModel.Name = _viewModel.Name;

                if (_viewModel.HasErrors)
                {
                    return;
                }

                FolderModel folder = new FolderModel();
                folder.Name = _viewModel.Name;
                folder.ParentId = _selected.CurrentFolder.Id;
                folder.Type = 3;

                await _store.CreateFolder(folder, _selected.CurrentFolder);                
                _navigation.Navigate();
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Ya existe una carpeta con dicho nombre", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la carpeta", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
