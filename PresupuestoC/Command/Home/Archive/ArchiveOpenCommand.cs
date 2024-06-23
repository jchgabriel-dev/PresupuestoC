using Microsoft.Win32;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.MVVM;
using PresupuestoC.Stores.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PresupuestoC.Models.Main;
using PresupuestoC.Stores.Folder;
using PresupuestoC.Stores.Project;

namespace PresupuestoC.Command.Archive
{
    public class ArchiveOpenCommand : AsyncCommand
    {
        private readonly ArchivesListStore _store;
        private readonly ArchiveTemporalStore _temporal;
        private readonly ArchiveSelectedStore _selected;
        private readonly FolderListStore _folderStore;
        private readonly ProjectListStore _projectStore;


        public ArchiveOpenCommand(ArchivesListStore store,  ArchiveTemporalStore temporal, ArchiveSelectedStore selected, 
            FolderListStore folderStore, ProjectListStore projectStore)
        {
            _store = store;
            _temporal = temporal;
            _selected = selected;
            _folderStore = folderStore;
            _projectStore = projectStore;

        }

        public async override Task ExecuteAsync(object parameter)
        {

            try
            {
                ArchiveModel test = _temporal.CurrentArchive;
                if (test == null || !System.IO.File.Exists(System.IO.Path.Combine(test?.Address, test?.Name)))
                {
                    throw new System.IO.FileNotFoundException("El archivo no se encuentra.", test?.Address);
                }
                else
                {
                    if (_selected.CurrentArchive != null) 
                    {
                        ArchiveModel current = await _store.GetArchive(_selected.CurrentArchive.Id);
                        current.Selected = null;
                        await _store.UpdateArchive(_selected.CurrentArchive.Id, current);                      
                    }

                    ArchiveModel opened = await _store.GetArchive(test.Id);
                    opened.Selected = true;
                    await _store.UpdateArchive(test.Id, opened);

                    _selected.CurrentArchive = test;
                    await _folderStore.Reload();
                    await _projectStore.Reload();

                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                try
                {
                    MessageBox.Show("No se encuentra al archivo en la ubicación guardada", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    await _store.DeleteArchive(_temporal.CurrentArchive.Id);
                    _temporal.Deselected();
                }

                
                catch (Exception nestedEx)
                {
                    MessageBox.Show("Error al eliminar el archivo", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el archivo", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }


}
