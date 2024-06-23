

using PresupuestoC.Models;
using PresupuestoC.MVVM;


using System.Windows;
using System.Windows.Input;
using PresupuestoC.Navigation.Home2;
using PresupuestoC.ViewModels.Home2;
using PresupuestoC.Stores.Archive;
using PresupuestoC.Command.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.Stores.Currency;


namespace PresupuestoC.ViewModels.Main
{
    public class HomeViewModel : ViewModelBase
    {
   
        private ArchiveSelectedStore _archiveStore;
        public bool IsSelected => _archiveStore.IsSelected;

        private readonly HomeNavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public ICommand ProjectNavigation { get; }

        public ICommand ArchivesNavigation { get; }

        public ICommand SettingsNavigation { get; }

        public ICommand ArchiveCurrent { get; }


        public HomeViewModel(HomeNavigationStore navigationStore,
            HomeNavigationService<ProjectViewModel> navigateProject,
            HomeNavigationService<ArchivesViewModel> navigateArchives,
            HomeNavigationService<SettingsViewModel> navigateSettings,
            ArchiveSelectedStore selected)
        {
            _navigationStore = navigationStore;
            _archiveStore = selected;

            ProjectNavigation = new HomeNavigateCommand<ProjectViewModel>(navigateProject);
            ArchivesNavigation = new HomeNavigateCommand<ArchivesViewModel>(navigateArchives);
            SettingsNavigation = new HomeNavigateCommand<SettingsViewModel>(navigateSettings);
            ArchiveCurrent = new ArchiveCurrentCommand(selected);
            ArchiveCurrent.Execute(null);


            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _archiveStore.CurrentArchiveChanged += OnArchiveChanged;
            
        
            if(selected.CurrentArchive != null)
            {
                ProjectNavigation.Execute(null);
            }
            else
            {
                ArchivesNavigation.Execute(null);
            }

        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnArchiveChanged()
        {
            OnPropertyChanged(nameof(IsSelected));
        }
    }

}
