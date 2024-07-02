using PresupuestoC.Command.Home.SubBudget;
using PresupuestoC.Models;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation;
using PresupuestoC.Navigation.Budget;
using PresupuestoC.Navigation.Home2;
using PresupuestoC.Navigation.Main;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Project;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.Budget;
using PresupuestoC.ViewModels.Client;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Main
{

    public class BudgetViewModel : ViewModelBase
    {
        // STORES

        public ICollectionView SubBudgetCollection { get; }

        private readonly ObservableCollection<SubBudgetModel> _subBudgets;
        public IEnumerable<SubBudgetModel> SubBudgets => _subBudgets;

        private SubBudgetSelectedStore _selectedStore;

        public SubBudgetModel Selected
        {
            get => _selectedStore.CurrentSubBudget;
            set => _selectedStore.CurrentSubBudget = value;
        }


        private readonly ProjectSelectedStore _selectedProject;

        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }        





        private readonly BudgetNavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public ICommand HomeNavigation { get; }
        public ICommand HeriarchyNavigation { get; }
        public ICommand PolyNavigation { get; }
        public ICommand ProgramNavigation { get; }

        public ICommand LoadSubBudgets { get; }

        public BudgetViewModel(BudgetNavigationStore navigationStore,
            NavigationService<HomeViewModel> navigateHome,
            BudgetNavigationService<HierarchyViewModel> navigateHierarchy,
            BudgetNavigationService<PolyViewModel> navigatePoly,
            BudgetNavigationService<ProgramViewModel> navigateProgram,
            ProjectSelectedStore selectedProject,
            SubBudgetListStore subStore)
        {
            _selectedProject = selectedProject;
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _subBudgets = new ObservableCollection<SubBudgetModel>();
            SubBudgetCollection = CollectionViewSource.GetDefaultView(_subBudgets);


            HomeNavigation = new NavigateCommand<HomeViewModel>(navigateHome);
            HeriarchyNavigation = new BudgetNavigateCommand<HierarchyViewModel>(navigateHierarchy);
            PolyNavigation = new BudgetNavigateCommand<PolyViewModel>(navigatePoly);
            ProgramNavigation = new BudgetNavigateCommand<ProgramViewModel>(navigateProgram);
            LoadSubBudgets = new SubBudgetSelectedCommand(this, subStore);

            HeriarchyNavigation.Execute(null);
            Name = _selectedProject.CurrentProject.Name;
        }


        public static BudgetViewModel LoadViewModel(BudgetNavigationStore navigationStore,
            NavigationService<HomeViewModel> navigateHome,
            BudgetNavigationService<HierarchyViewModel> navigateHierarchy,
            BudgetNavigationService<PolyViewModel> navigatePoly,
            BudgetNavigationService<ProgramViewModel> navigateProgram,
            ProjectSelectedStore selectedProject,
            SubBudgetListStore subStore)
        {
            BudgetViewModel viewModel = new BudgetViewModel(navigationStore, navigateHome, navigateHierarchy, navigatePoly, navigateProgram, selectedProject, subStore);
            viewModel.LoadSubBudgets.Execute(null);
            return viewModel;
        }

        public void UpdateSubBudgets(IEnumerable<SubBudgetModel> subs)
        {
            _subBudgets.Clear();

            foreach (SubBudgetModel sub in subs)
            {
                _subBudgets.Add(sub);
            }
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}