using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Location
{
    public class LocationNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly LocationNavigationService<TViewModel> _locationNavigationService;

        public LocationNavigateCommand(LocationNavigationService<TViewModel> navigationService)
        {
            _locationNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _locationNavigationService.Navigate();
        }
    }
}
