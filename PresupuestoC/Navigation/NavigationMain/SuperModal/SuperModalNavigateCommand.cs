using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.SuperModal
{
   

    public class SuperModalNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly SuperModalNavigationService<TViewModel> _modalNavigationService;

        public SuperModalNavigateCommand(SuperModalNavigationService<TViewModel> navigationService)
        {
            _modalNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationService.Navigate();
        }
    }
}
