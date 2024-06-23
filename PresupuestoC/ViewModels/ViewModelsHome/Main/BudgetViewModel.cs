using PresupuestoC.MVVM;
using PresupuestoC.Navigation;
using PresupuestoC.Navigation.Budget;
using PresupuestoC.Navigation.Home2;
using PresupuestoC.Navigation.Main;
using PresupuestoC.ViewModels.Budget;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Main
{

    public class BudgetViewModel : ViewModelBase
    {

        private readonly BudgetNavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public ICommand HomeNavigation { get; }

        public ICommand HeriarchyNavigation { get; }

        public ICommand PolyNavigation { get; }

        public ICommand ProgramNavigation { get; }



        public BudgetViewModel(BudgetNavigationStore navigationStore,
            NavigationService<HomeViewModel> navigateHome,
            BudgetNavigationService<HierarchyViewModel> navigateHierarchy,
            BudgetNavigationService<PolyViewModel> navigatePoly,
            BudgetNavigationService<ProgramViewModel> navigateProgram)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            HomeNavigation = new NavigateCommand<HomeViewModel>(navigateHome);
            HeriarchyNavigation = new BudgetNavigateCommand<HierarchyViewModel>(navigateHierarchy);
            PolyNavigation = new BudgetNavigateCommand<PolyViewModel>(navigatePoly);
            ProgramNavigation = new BudgetNavigateCommand<ProgramViewModel>(navigateProgram);

            HeriarchyNavigation.Execute(null);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}