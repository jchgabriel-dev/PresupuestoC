using PresupuestoC.Models.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Stores.Folder
{

    public class FolderSelectedStore
    {
        private FolderModel _currentFolder;

        public FolderModel CurrentFolder
        {
            get => _currentFolder;
            set
            {
                _currentFolder = value;
                CurrentFolderChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentFolder != null;


        public event Action CurrentFolderChanged;

        public void Deselected()
        {
            CurrentFolder = null;
        }
    }
}
