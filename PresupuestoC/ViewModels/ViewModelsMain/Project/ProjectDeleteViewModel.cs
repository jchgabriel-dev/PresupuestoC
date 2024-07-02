using PresupuestoC.Command.Currency;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores;
using PresupuestoC.ViewModels.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresupuestoC.Stores.Project;
using PresupuestoC.Navigation.Project;
using PresupuestoC.Command.Project;

namespace PresupuestoC.ViewModels.Project
{
 

    public class ProjectDeleteViewModel : ViewModelBase
    {

        private string _confirmation = string.Empty;
        public string Confirmation
        {
            get
            {
                return _confirmation;
            }
            set
            {
                _confirmation = value;
                OnPropertyChanged(nameof(Confirmation));
                OnPropertyChanged(nameof(CanDelete));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool CanDelete => Confirmation.Equals("SI", StringComparison.OrdinalIgnoreCase);

        public ICommand DeleteProject { get; }
        public ICommand Cancel { get; }

        public ProjectDeleteViewModel(ProjectSelectedStore selectedStore,
            ProjectNavigationService<ProjectListViewModel> navigate,
            ProjectListStore store)
        {
            DeleteProject = new ProjectDeleteCommand(navigate, store, selectedStore);
            Cancel = new ProjectNavigateCommand<ProjectListViewModel>(navigate);
            Name = selectedStore.CurrentProject.Name;

        }
    }

}
