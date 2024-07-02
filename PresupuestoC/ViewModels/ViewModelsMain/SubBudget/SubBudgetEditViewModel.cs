using PresupuestoC.Command.Folder;
using PresupuestoC.Command.Home.SubBudget;
using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
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
    public class SubBudgetEditViewModel : ViewModelBase, INotifyDataErrorInfo
    {

        // STORE

        private readonly SubBudgetSelectedStore _selected;

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



        public ICommand EditSubBudget { get; }
        public ICommand CloseModal { get; }


        // CONSTRUCTOR

        public SubBudgetEditViewModel(CloseNavigationService close, SubBudgetListStore store, SubBudgetSelectedStore selected)
        {

            CloseModal = new CloseNavigateCommand(close);
            EditSubBudget = new SubBudgetEditCommand(this, close, store, selected);
            _selected = selected;

            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            Description = _selected.CurrentSubBudget.Description;
            Active = _selected.CurrentSubBudget.Active;
            if(_selected.CurrentSubBudget.Work != 0 )
            {
                LetWork = true;
                Work = Convert.ToString(_selected.CurrentSubBudget.Work);
            }

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
