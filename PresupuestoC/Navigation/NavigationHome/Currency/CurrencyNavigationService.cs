using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Currency
{
    public class CurrencyNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly CurrencyNavigationStore _currencyNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public CurrencyNavigationService(CurrencyNavigationStore navigationStore, Func<TViewModel> createViewModel)
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
