using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Currency
{
    public class CurrencyNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly CurrencyNavigationService<TViewModel> _currencyNavigationService;

        public CurrencyNavigateCommand(CurrencyNavigationService<TViewModel> navigationService)
        {
            _currencyNavigationService = navigationService;
        }



        public override void Execute(object parameter)
        {
            _currencyNavigationService.Navigate();
        }
    }
}
