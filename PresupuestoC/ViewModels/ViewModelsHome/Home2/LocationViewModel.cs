using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.Main;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.ViewModels.Location.District;
using PresupuestoC.ViewModels.Location.Province;
using PresupuestoC.ViewModels.Location.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Home2
{
    public class LocationViewModel : ViewModelBase
    {
            
        // NAVIGATION

        private readonly LocationNavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand CloseModal { get; }
        public ICommand LocationListNavigation { get; }
        public ICommand LocationRegisterNavigation { get; }


        public LocationViewModel(LocationNavigationStore navigationStore,
            LocationNavigationService<LocationListViewModel> navigateLocationList,
            LocationNavigationService<LocationRegisterViewModel> navigateLocationRegister,                       
            CloseNavigationService close,
            ProjectTemporalStore project) 
        {

            _navigationStore = navigationStore;
            project.DeselectedLocation();

            LocationListNavigation = new LocationNavigateCommand<LocationListViewModel>(navigateLocationList);
            LocationRegisterNavigation = new LocationNavigateCommand<LocationRegisterViewModel>(navigateLocationRegister);
            
            CloseModal = new CloseNavigateCommand(close);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            LocationListNavigation.Execute(null);

        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
     
    }
}
