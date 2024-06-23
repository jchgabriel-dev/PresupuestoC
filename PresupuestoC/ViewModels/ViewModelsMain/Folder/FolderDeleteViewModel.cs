using PresupuestoC.Command.Folder;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Folder
{

    public class FolderDeleteViewModel : ViewModelBase
    {

        private string _confirmation = string.Empty;
        public string Confirmation
        {
            get
            {
                return _confirmation;
            }
            set
            {
                _confirmation = value;
                OnPropertyChanged(nameof(Confirmation));
                OnPropertyChanged(nameof(CanDelete));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool CanDelete => Confirmation.Equals("SI", StringComparison.OrdinalIgnoreCase);
        

        public ICommand CloseModal { get; }
        public ICommand DeleteFolder {  get; }  


        public FolderDeleteViewModel(CloseNavigationService close, FolderListStore store, FolderSelectedStore selected)
        {
            CloseModal = new CloseNavigateCommand(close);
            DeleteFolder = new FolderDeleteCommand(close, store, selected);
            Name = selected.CurrentFolder.Name;
           
        }
    }
}
