using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Home2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Project
{
    public class ProjectNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly ProjectNavigationStore _projectNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public ProjectNavigationService(ProjectNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _projectNavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _projectNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
