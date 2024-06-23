using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Modal
{
    public class ModalNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly ModalNavigationService<TViewModel> _modalNavigationService;

        public ModalNavigateCommand(ModalNavigationService<TViewModel> navigationService)
        {
            _modalNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationService.Navigate();
        }
    }
}
