using PresupuestoC.Command.Client;
using PresupuestoC.Command.Location;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Location;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Location.Region
{   
    public class LocationCreateRegionViewModel : ViewModelBase, INotifyDataErrorInfo
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


        public ICommand CreateRegion { get; }
        public ICommand Cancel { get; }

        // CONSTRUCTOR
        public LocationCreateRegionViewModel(            
            LocationListStore store,
            SuperCloseNavigationService close)
        {
            CreateRegion = new RegionCreateCommand(this, close, store);
            Cancel = new SuperCloseNavigateCommand(close);

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
