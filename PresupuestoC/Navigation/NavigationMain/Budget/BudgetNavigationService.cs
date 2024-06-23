using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Budget
{

    public class BudgetNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly BudgetNavigationStore _clientNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public BudgetNavigationService(BudgetNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _clientNavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _clientNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
