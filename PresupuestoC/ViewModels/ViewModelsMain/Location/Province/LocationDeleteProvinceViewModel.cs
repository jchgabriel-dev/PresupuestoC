using PresupuestoC.Command.Client;
using PresupuestoC.Command.Location;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Location;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Location.Province
{
  

    public class LocationDeleteProvinceViewModel : ViewModelBase
    {
            
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


        public ICommand DeleteProvince { get; }
        public ICommand Cancel { get; }

        // CONSTRUCTOR
        public LocationDeleteProvinceViewModel(SuperCloseNavigationService navigate,
            LocationListStore store, ProvinceSelectedStore selected)
        {
            Cancel = new SuperCloseNavigateCommand(navigate);
            DeleteProvince = new ProvinceDeleteCommand(navigate, store, selected);
            Name = selected.CurrentProvince.Name;

        }      

    }
}
