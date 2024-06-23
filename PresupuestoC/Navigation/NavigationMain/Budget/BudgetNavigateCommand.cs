using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Budget
{
 
     public class BudgetNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly BudgetNavigationService<TViewModel> _clientNavigationService;

        public BudgetNavigateCommand(BudgetNavigationService<TViewModel> navigationService)
        {
            _clientNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _clientNavigationService.Navigate();
        }
    }
}
