using PresupuestoC.Command.Location;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Location;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Location.Region
{
    public class LocationEditRegionViewModel : ViewModelBase, INotifyDataErrorInfo
    {              
        
        // CONFIRMATION

        private readonly ErrorsViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool CanEdit => !HasErrors;
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


        public ICommand EditRegion { get; }
        public ICommand Cancel { get; }

        // CONSTRUCTOR
        public LocationEditRegionViewModel(
            SuperCloseNavigationService navigate,
            LocationListStore store,
            RegionSelectedStore selected)
        {
            
            Cancel = new SuperCloseNavigateCommand(navigate);
            EditRegion = new RegionEditCommand(this, navigate, store, selected);
            _errorsViewModel = new ErrorsViewModel();

            Name = selected.CurrentRegion.Name;

            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;


        }

     

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanEdit));
        }

    }
}
