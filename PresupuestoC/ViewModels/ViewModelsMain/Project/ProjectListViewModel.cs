using PresupuestoC.Command.Client;
using PresupuestoC.Command.Project;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Main;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Client;
using PresupuestoC.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PresupuestoC.ViewModels.Project
{
    public class ProjectListViewModel : ViewModelBase
    {
        // FILTERS
        public ICollectionView ProjectCollection { get; }

        private string _projectFilterName = string.Empty;
        public string ProjectFilterName
        {
            get
            {
                return _projectFilterName;
            }
            set
            {
                _projectFilterName = value;
                OnPropertyChanged(nameof(ProjectFilterName));
                ProjectCollection.Refresh();
            }
        }

        // STORES
        private readonly ObservableCollection<ProjectModel> _projects;
        private readonly ObservableCollection<StateModel> _states;


        private ProjectSelectedStore _selectedStore;
        public ProjectModel Selected
        {
            get => _selectedStore.CurrentProject;       
            set => _selectedStore.CurrentProject = value;
            
        }

        public IEnumerable<ProjectModel> Projects => _projects;
        public IEnumerable<StateModel> States => _states;

        public ICommand SelectProject { get; }
        public ICommand LoadProjects { get; }
       


        // CONSTRUCTOR
        public ProjectListViewModel(ProjectListStore store,
            ProjectSelectedStore selectedStore,
            NavigationService<BudgetViewModel> navigate)

        {
            _selectedStore = selectedStore;
            _projects = new ObservableCollection<ProjectModel>();
            _states = new ObservableCollection<StateModel>();

            LoadProjects = new ProjectLoadCommand(this, store);
            SelectProject = new ProjectSelectedCommand(selectedStore, navigate);


            ProjectCollection = CollectionViewSource.GetDefaultView(_projects);

            _selectedStore.Deselected();
        }

        public static ProjectListViewModel LoadViewModel(ProjectListStore store, ProjectSelectedStore selectedStore, NavigationService<BudgetViewModel> navigate)
        {          
            ProjectListViewModel viewModel = new ProjectListViewModel(store, selectedStore, navigate);
            viewModel.LoadProjects.Execute(null);
            return viewModel;

        }

        public void UpdateProjects(IEnumerable<ProjectModel> projects, IEnumerable<StateModel> states)
        {
            _projects.Clear();

            foreach (ProjectModel project in projects)
            {
                _projects.Add(project);
            }

            _states.Clear();

            foreach (StateModel state in states)
            {
                _states.Add(state);
            }
        }

        private bool FilterProject(object obj)
        {
            if (obj is ClientModel clientModel)
            {
                return clientModel.Name.Contains(ProjectFilterName, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;


        }


    }
}
