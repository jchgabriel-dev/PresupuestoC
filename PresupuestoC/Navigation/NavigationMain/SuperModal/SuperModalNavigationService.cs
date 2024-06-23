using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.SuperModal
{
  

    public class SuperModalNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly SuperModalNavigationStore _modalNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public SuperModalNavigationService(SuperModalNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _modalNavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _modalNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
