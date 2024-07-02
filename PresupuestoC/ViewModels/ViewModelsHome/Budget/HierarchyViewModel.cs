using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Navigation.NavigationBudget.Heriarchy;
using PresupuestoC.ViewModels.Client;
using PresupuestoC.ViewModels.Home2;
using PresupuestoC.ViewModels.ViewModelsBudget.Heriarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Budget
{
    
    
    
    public class HierarchyViewModel : ViewModelBase
    {

        private readonly HeriarchyNavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public ICommand LevelNavigation { get; }
        public ICommand CertificateNavigation { get; }
        public ICommand TitleNavigation { get; }

        public HierarchyViewModel(HeriarchyNavigationStore navigationStore,
            HeriarchyNavigationService<LevelViewModel> navigateLevel,
            HeriarchyNavigationService<CertificateViewModel> navigateCertificate,
            HeriarchyNavigationService<TitleViewModel> navigateTitle
            )
        {
            _navigationStore = navigationStore;
            LevelNavigation = new HeriarchyNavigateCommand<LevelViewModel>(navigateLevel);
            CertificateNavigation = new HeriarchyNavigateCommand<CertificateViewModel>(navigateCertificate);
            TitleNavigation = new HeriarchyNavigateCommand<TitleViewModel>(navigateTitle);

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
