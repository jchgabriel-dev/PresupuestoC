using PresupuestoC.MVVM;
using PresupuestoC.Stores.Archive;
using PresupuestoC.ViewModels.Home2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Archive
{
  
    public class ArchiveCurrentCommand : AsyncCommand
    {
        private readonly ArchiveSelectedStore _selected;

        public ArchiveCurrentCommand(ArchiveSelectedStore selected)
        {
            _selected = selected;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _selected.Load();          

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el archivo de inicio", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
