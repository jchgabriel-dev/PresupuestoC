using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PresupuestoC.Database;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Stores.Archive;
using PresupuestoC.Stores.Client;
using PresupuestoC.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Archive
{
    public class ArchiveCreateCommand : AsyncCommand
    {       
        private readonly ArchivesListStore _store;


        public ArchiveCreateCommand(ArchivesListStore store)
        {           
            _store = store;
        }


        public async override Task ExecuteAsync(object parameter)
        {
                              
            try
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "Crear archivo";
                fileDialog.Filter = "Archivos PRS (*.prs)|*.prs";
                fileDialog.OverwritePrompt = false;

                bool? success = fileDialog.ShowDialog();
                if(success == true)
                {
                    string path = fileDialog.FileName;

                    if (File.Exists(path))
                    {
                        throw new InvalidOperationException();
                    }


                    string address = Path.GetDirectoryName(path);
                    string name = Path.GetFileName(path);

                    ArchiveModel archive = new ArchiveModel();
                    archive.Name = name;
                    archive.Address = address;
                    archive.Creation = DateOnly.FromDateTime(DateTime.Now);

                    await _store.CreateArchiveReal(path, archive);

                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Ya existe un archivo con dicho nombre", "Error",
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
