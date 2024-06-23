using PresupuestoC.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Modal
{
    public class CloseNavigateCommand : CommandBase
    {
        private readonly CloseNavigationService _closeNavigationService;

        public CloseNavigateCommand(CloseNavigationService navigationService)
        {
            _closeNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _closeNavigationService.Navigate();
        }
    }
}
