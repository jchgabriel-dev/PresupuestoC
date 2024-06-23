using Microsoft.Win32;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Stores.Archive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Archive
{

    public class ArchiveSearchCommand : AsyncCommand
    {
        private readonly ArchivesListStore _store;

        public ArchiveSearchCommand(ArchivesListStore store)
        {
            _store = store;
        }


        public async override Task ExecuteAsync(object parameter)
        {

            try
            {
                                                
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Title = "Buscar archivo";
                fileDialog.Filter = "Archivos PRS (*.prs)|*.prs|Todos los archivos (*.*)|*.*";

                bool? success = fileDialog.ShowDialog();
                if (success == true)
                {
                    string path = fileDialog.FileName;
                    string address = Path.GetDirectoryName(path);
                    string name = Path.GetFileName(path);
                    
                    ArchiveModel archive = new ArchiveModel();
                    archive.Name = name;                    
                    archive.Address = address;
                    archive.Creation = DateOnly.FromDateTime(DateTime.Now);


                    await _store.CreateArchiveVirtual(archive);
                }
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show("El archivo ya se encuentra en el registro", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el archivo", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
