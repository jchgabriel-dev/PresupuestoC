using PresupuestoC.Command.Folder;
using PresupuestoC.Command.Home.SubBudget;
using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Home2;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.Project;
using PresupuestoC.Services;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Folder;
using PresupuestoC.Stores.Project;
using PresupuestoC.Stores.SubBudget;
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
        // FILTERS 
        private ProjectSelectedStore _selectedProject;
        public bool SelectedProject => _selectedProject.IsSelected;

        public ICollectionView SubBudgetCollection { get; }
        public ProjectModel SubFilterProject => _selectedProject.CurrentProject;


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

        
        public IEnumerable<SubBudgetModel> SubBudgets => _subBudgets;
        private readonly ObservableCollection<SubBudgetModel> _subBudgets;
        
        private SubBudgetListStore _subBudgetStore;

        private SubBudgetSelectedStore _selectedSubBudgetStore;
        public SubBudgetModel SelectedSubBudget
        {
            get => _selectedSubBudgetStore.CurrentSubBudget;
            set => _selectedSubBudgetStore.CurrentSubBudget = value;
            
        }

        public bool IsSelectedSubBudget => (_selectedSubBudgetStore.IsSelected);



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
        public ICommand LoadSubBudgets { get; }


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
            FolderSelectedStore selected,

            SubBudgetListStore storeSubBudget,
            SubBudgetSelectedStore selectedSubBudget,
            ProjectSelectedStore selectedProject)
        
        {
            _folders = new ObservableCollection<FolderModel>();
            _subBudgets = new ObservableCollection<SubBudgetModel>();


            _navigationStore = navigationStore;
            _store = store;
            _selectedStore = selected;
            _selectedProject = selectedProject;

            _subBudgetStore = storeSubBudget;
            _selectedSubBudgetStore = selectedSubBudget;

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
            LoadSubBudgets = new SubBudgetLoadCommand(this, storeSubBudget);

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _selectedStore.CurrentFolderChanged += OnFolderChanged;
            _selectedProject.CurrentProjectChanged += OnProjectChanged;
            _selectedSubBudgetStore.CurrentSubBudgetChanged += OnSubBudgetChanged;

            ProjectListNavigation.Execute(null);
            SubBudgetCollection = CollectionViewSource.GetDefaultView(_subBudgets);
            SubBudgetCollection.Filter = FilterSub;

            _store.Changes += OnChanges;
            _subBudgetStore.Changes += OnChangesSub;

        }

        private async void OnChanges()
        {            
            UpdateFolders(_store.Folders);
           
        }

        private async void OnChangesSub()
        {
            UpdateSubBudgets(_subBudgetStore.SubBudgets);   
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
            FolderSelectedStore selected,
            SubBudgetListStore storeSubBudget,
            SubBudgetSelectedStore selectedSubBudget,
            ProjectSelectedStore selectedProject)
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
                selected,
                storeSubBudget,
                selectedSubBudget,
                selectedProject);

            viewModel.LoadFolders.Execute(null);
            viewModel.LoadSubBudgets.Execute(null);

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
        

        public void UpdateSubBudgets(IEnumerable<SubBudgetModel> subBudgets)
        {
            _subBudgets.Clear();

            foreach(SubBudgetModel subBudget in subBudgets)
            {
                _subBudgets.Add(subBudget);
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

        private void OnProjectChanged()
        {
            OnPropertyChanged(nameof(SelectedProject));
            OnPropertyChanged(nameof(SubFilterProject));
            SubBudgetCollection?.Refresh();

        }

        private void OnSubBudgetChanged()
        {
            OnPropertyChanged(nameof(IsSelectedSubBudget));

        }

        private bool FilterSub(object obj)
        {
            if (obj is SubBudgetModel subModel)
            {
                return (subModel.ProjectId.Equals(SubFilterProject?.Id));
            }
            return false;
        }
    }
}
