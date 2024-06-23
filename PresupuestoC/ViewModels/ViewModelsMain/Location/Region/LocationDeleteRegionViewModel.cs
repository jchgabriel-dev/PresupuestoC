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

namespace PresupuestoC.ViewModels.Location.Region
{
   
    public class LocationDeleteRegionViewModel : ViewModelBase
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

        public ICommand DeleteRegion { get; }
        public ICommand Cancel { get; }

        // CONSTRUCTOR
        public LocationDeleteRegionViewModel(
            SuperCloseNavigationService navigate,
            RegionSelectedStore selected, 
            LocationListStore store)
        {
            DeleteRegion = new RegionDeleteCommand(this, navigate, selected, store);
            Cancel = new SuperCloseNavigateCommand(navigate);

            Name = selected.CurrentRegion.Name;

        }
           
    }
}
