using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.NavigationBudget.Heriarchy
{
  
    public class HeriarchyNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly HeriarchyNavigationStore _currencyNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public HeriarchyNavigationService(HeriarchyNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _currencyNavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _currencyNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
