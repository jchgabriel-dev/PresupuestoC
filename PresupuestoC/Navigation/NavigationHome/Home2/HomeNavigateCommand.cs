using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Home2
{
    public class HomeNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly HomeNavigationService<TViewModel> _homeNavigationService;

        public HomeNavigateCommand(HomeNavigationService<TViewModel> navigationService)
        {
            _homeNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _homeNavigationService.Navigate();
        }
    }
}
