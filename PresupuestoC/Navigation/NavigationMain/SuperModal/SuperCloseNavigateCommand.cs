using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.SuperModal
{
 
    public class SuperCloseNavigateCommand : CommandBase
    {
        private readonly SuperCloseNavigationService _closeNavigationService;

        public SuperCloseNavigateCommand(SuperCloseNavigationService navigationService)
        {
            _closeNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _closeNavigationService.Navigate();
        }
    }
}
