using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Home2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Project
{
    public class ProjectNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly ProjectNavigationService<TViewModel> _locationNavigationService;

        public ProjectNavigateCommand(ProjectNavigationService<TViewModel> navigationService)
        {
            _locationNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _locationNavigationService.Navigate();
        }
    }
}
