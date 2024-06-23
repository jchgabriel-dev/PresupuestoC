using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;

using PresupuestoC.Navigation.Main;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.Project;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Currency;
using PresupuestoC.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Home2
{
    public class CurrencyViewModel : ViewModelBase
    {
        
        // SELECTED
        private readonly CurrencySelectedStore _selectedStore;
        public CurrencyModel SelectedStore => _selectedStore.CurrentCurrency;
        public bool IsSelected => (_selectedStore.IsSelected);


        // NAVIGATION

        private readonly CurrencyNavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand CloseModal { get; }
        public ICommand CurrencyListNavigation { get; }
        public ICommand CurrencyCreateNavigation { get; }
        public ICommand CurrencyEditNavigation { get; }
        public ICommand CurrencyDeleteNavigation { get; }


        public CurrencyViewModel(CurrencyNavigationStore navigationStore,
            CurrencyNavigationService<CurrencyListViewModel> navigateCurrencyList,
            CurrencyNavigationService<CurrencyCreateViewModel> navigateCurrencyCreate,
            CurrencyNavigationService<CurrencyEditViewModel> navigateCurrencyEdit,
            CurrencyNavigationService<CurrencyDeleteViewModel> navigateCurrencyDelete,
            CloseNavigationService close,
            CurrencySelectedStore selectedStore,
            ProjectTemporalStore project)
        {
            project.DeselectedCurrency();
            _navigationStore = navigationStore;
            _selectedStore = selectedStore;
            CurrencyListNavigation = new CurrencyNavigateCommand<CurrencyListViewModel>(navigateCurrencyList);
            CurrencyCreateNavigation = new CurrencyNavigateCommand<CurrencyCreateViewModel>(navigateCurrencyCreate);
            CurrencyEditNavigation = new CurrencyNavigateCommand<CurrencyEditViewModel>(navigateCurrencyEdit);
            CurrencyDeleteNavigation = new CurrencyNavigateCommand<CurrencyDeleteViewModel>(navigateCurrencyDelete);

            CloseModal = new CloseNavigateCommand(close);
            
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _selectedStore.CurrentCurrencyChanged += OnCurrentCurrencyChanged;
            CurrencyListNavigation.Execute(null);

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentCurrencyChanged()
        {
            OnPropertyChanged(nameof(SelectedStore));
            OnPropertyChanged(nameof(IsSelected));

        }
    }
}
