using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Client
{
    public class ClientNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly ClientNavigationService<TViewModel> _clientNavigationService;

        public ClientNavigateCommand(ClientNavigationService<TViewModel> navigationService)
        {
            _clientNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _clientNavigationService.Navigate();
        }
    }
}
