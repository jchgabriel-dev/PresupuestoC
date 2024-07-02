using PresupuestoC.Command.Folder;
using PresupuestoC.Command.Home.SubBudget;
using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Folder;
using PresupuestoC.Stores.Project;
using PresupuestoC.Stores.SubBudget;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.SubBudget
{
    public class SubBudgetCreateViewModel : ViewModelBase, INotifyDataErrorInfo
    {

        // STORES
        private readonly ProjectSelectedStore selectedProject;
        public ProjectModel Project => selectedProject.CurrentProject;

        // ERRORS

        private readonly ErrorsViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool CanCreate => !HasErrors;
        public bool HasErrors => _errorsViewModel.HasErrors;


        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                _errorsViewModel.ClearErrors(nameof(Description));
                if (string.IsNullOrWhiteSpace(_description))
                {
                    _errorsViewModel.AddError(nameof(Description), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Description));

            }
        }

        private string _work;
        public string Work
        {
            get
            {
                return _work;
            }
            set
            {
                _work = value;
                _errorsViewModel.ClearErrors(nameof(Work));
                if (string.IsNullOrWhiteSpace(_work) && LetWork == true)
                {
                    _errorsViewModel.AddError(nameof(Work), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Work));

            }
        }

        private bool _active;
        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        private bool _letWork;
        public bool LetWork
        {
            get => _letWork;
            set => _letWork = value;
        }



        public ICommand CreateSubBudget {  get; }
        public ICommand CloseModal { get; }


        // CONSTRUCTOR

        public SubBudgetCreateViewModel(CloseNavigationService close, SubBudgetListStore store, ProjectSelectedStore selected) 
        {

            CloseModal = new CloseNavigateCommand(close);
            CreateSubBudget = new SubBudgetCreateCommand(this, close, store);

            selectedProject = selected;

            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            Active = true;
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
