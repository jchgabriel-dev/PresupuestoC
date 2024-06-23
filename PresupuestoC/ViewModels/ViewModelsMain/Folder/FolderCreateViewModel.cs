using PresupuestoC.Command.Folder;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Folder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Folder
{


    public class FolderCreateViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly ErrorsViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool CanCreate => !HasErrors;
        public bool HasErrors => _errorsViewModel.HasErrors;


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
                _errorsViewModel.ClearErrors(nameof(Name));
                if (string.IsNullOrWhiteSpace(_name))
                {
                    _errorsViewModel.AddError(nameof(Name), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Name));

            }
        }

        public ICommand CloseModal { get; }
        public ICommand CreateFolder {  get; }  

        // CONSTRUCTOR
        public FolderCreateViewModel(CloseNavigationService close, FolderListStore store, FolderSelectedStore selected)
        {
            CloseModal = new CloseNavigateCommand(close);
            CreateFolder = new FolderCreateCommand(this, close, store, selected);

            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanCreate));
        }

    }
}
