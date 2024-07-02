using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.NavigationBudget.Heriarchy
{
  
    public class HeriarchyNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly HeriarchyNavigationService<TViewModel> _currencyNavigationService;

        public HeriarchyNavigateCommand(HeriarchyNavigationService<TViewModel> navigationService)
        {
            _currencyNavigationService = navigationService;
        }



        public override void Execute(object parameter)
        {
            _currencyNavigationService.Navigate();
        }
    }
}
