using PresupuestoC.Command.Folder;
using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Home2;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.Project;
using PresupuestoC.Services;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Folder;
using PresupuestoC.ViewModels.Client;
using PresupuestoC.ViewModels.Folder;
using PresupuestoC.ViewModels.Project;
using PresupuestoC.ViewModels.SubBudget;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Home2
{
    public class ProjectViewModel : ViewModelBase
    {        
        // STORES
        public IEnumerable<FolderModel> Folders => _folders;
        private readonly ObservableCollection<FolderModel> _folders;

        private FolderListStore _store;

        private FolderSelectedStore _selectedStore;
        public FolderModel Selected
        {
            get => _selectedStore.CurrentFolder;
            set
            {
                if(value != null)
                    _selectedStore.CurrentFolder = value;
            }
        }

        public bool IsSelected => (_selectedStore.IsSelected);
        public int? IsType => (_selectedStore.CurrentFolder?.Type);

       

        // NAVIGATION
        private readonly ProjectNavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand ProjectListNavigation { get; }
        public ICommand ProjectCreateNavigation { get; }
        public ICommand ProjectEditNavigation { get; }
        public ICommand ProjectDeleteNavigation { get; }


        public ICommand FolderCreateNavigation { get; }
        public ICommand FolderEditNavigation { get; }
        public ICommand FolderDeleteNavigation { get; }

        public ICommand SubCreateNavigation { get; }
        public ICommand SubEditNavigation { get; }
        public ICommand SubDeleteNavigation { get; }

        public ICommand LoadFolders {  get; }

        public ProjectViewModel(ProjectNavigationStore navigationStore,
            ProjectNavigationService<ProjectListViewModel> navigateProjectList,
            ProjectNavigationService<ProjectCreateViewModel> navigateProjectCreate,
            ProjectNavigationService<ProjectEditViewModel> navigateProjectEdit,
            ProjectNavigationService<ProjectDeleteViewModel> navigateProjectDelete,

            ModalNavigationService<FolderCreateViewModel> navigateCreateFolder,
            ModalNavigationService<FolderEditViewModel> navigateEditFolder,
            ModalNavigationService<FolderDeleteViewModel> navigateDeleteFolder,

            ModalNavigationService<SubBudgetCreateViewModel> navigateCreateSub,
            ModalNavigationService<SubBudgetEditViewModel> navigateEditSub,
            ModalNavigationService<SubBudgetDeleteViewModel> navigateDeleteSub,


            FolderListStore store,
            FolderSelectedStore selected
            )
        
        {
            _folders = new ObservableCollection<FolderModel>();
            _navigationStore = navigationStore;
            _store = store;

            ProjectListNavigation = new ProjectNavigateCommand<ProjectListViewModel>(navigateProjectList);
            ProjectCreateNavigation = new ProjectNavigateCommand<ProjectCreateViewModel>(navigateProjectCreate);
            ProjectEditNavigation = new ProjectNavigateCommand<ProjectEditViewModel>(navigateProjectEdit);
            ProjectDeleteNavigation = new ProjectNavigateCommand<ProjectDeleteViewModel>(navigateProjectDelete);

            FolderCreateNavigation = new ModalNavigateCommand<FolderCreateViewModel>(navigateCreateFolder);
            FolderEditNavigation = new ModalNavigateCommand<FolderEditViewModel>(navigateEditFolder);
            FolderDeleteNavigation = new ModalNavigateCommand<FolderDeleteViewModel>(navigateDeleteFolder);
            SubCreateNavigation = new ModalNavigateCommand<SubBudgetCreateViewModel>(navigateCreateSub);
            SubEditNavigation = new ModalNavigateCommand<SubBudgetEditViewModel>(navigateEditSub);
            SubDeleteNavigation = new ModalNavigateCommand<SubBudgetDeleteViewModel>(navigateDeleteSub);

            LoadFolders = new FolderLoadCommand(this, store);
            _selectedStore = selected;


            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _selectedStore.CurrentFolderChanged += OnFolderChanged;

            ProjectListNavigation.Execute(null);

            _store.Changes += OnChanges;

        }

        private async void OnChanges()
        {            
            UpdateFolders(_store.Folders);
           
        }



        public static ProjectViewModel LoadViewModel(ProjectNavigationStore navigationStore,
            ProjectNavigationService<ProjectListViewModel> navigateProjectList,
            ProjectNavigationService<ProjectCreateViewModel> navigateProjectCreate,
            ProjectNavigationService<ProjectEditViewModel> navigateProjectEdit,
            ProjectNavigationService<ProjectDeleteViewModel> navigateProjectDelete,
            ModalNavigationService<FolderCreateViewModel> navigateCreateFolder,
            ModalNavigationService<FolderEditViewModel> navigateEditFolder,
            ModalNavigationService<FolderDeleteViewModel> navigateDeleteFolder,
            ModalNavigationService<SubBudgetCreateViewModel> navigateCreateSub,
            ModalNavigationService<SubBudgetEditViewModel> navigateEditSub,
            ModalNavigationService<SubBudgetDeleteViewModel> navigateDeleteSub,
            FolderListStore store,
            FolderSelectedStore selected)
        {
            ProjectViewModel viewModel = new ProjectViewModel(navigationStore, 
                navigateProjectList, 
                navigateProjectCreate,
                navigateProjectEdit,
                navigateProjectDelete,
                navigateCreateFolder,
                navigateEditFolder,
                navigateDeleteFolder,
                navigateCreateSub,
                navigateEditSub,
                navigateDeleteSub,
                store,
                selected);

            viewModel.LoadFolders.Execute(null);            
            return viewModel;

        }
      
        public void UpdateFolders(IEnumerable<FolderModel> folders)
        {
            _folders.Clear();

            foreach (FolderModel folder in folders)
            {
                ICollectionView FolderCollection = CollectionViewSource.GetDefaultView(folder.Children);
                FolderCollection.SortDescriptions.Add(new SortDescription(nameof(FolderModel.Name), ListSortDirection.Ascending));                 
            }

            foreach (FolderModel folder in folders.Where(f => f.ParentId == null))
            {
                _folders.Add(folder);                
            }          
        }
        

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnFolderChanged()
        {
            OnPropertyChanged(nameof(Selected));
            OnPropertyChanged(nameof(IsSelected));
            OnPropertyChanged(nameof(IsType));

        }
    }
}
