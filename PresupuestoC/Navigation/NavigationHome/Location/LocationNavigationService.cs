using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Location
{
    public class LocationNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly LocationNavigationStore _locationNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public LocationNavigationService(LocationNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _locationNavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _locationNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
