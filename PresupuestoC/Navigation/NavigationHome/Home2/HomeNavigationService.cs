using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Home2
{
    public class HomeNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly HomeNavigationStore _homeNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public HomeNavigationService(HomeNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _homeNavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _homeNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
