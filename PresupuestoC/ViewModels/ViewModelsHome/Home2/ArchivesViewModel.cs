using PresupuestoC.Command.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Archive;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Folder;
using PresupuestoC.Stores.Project;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Home2
{
    public class ArchivesViewModel : ViewModelBase
    {
        // TEMPORAL STORE
        
        private ArchiveTemporalStore _archiveTemporalStore;
        
        private ArchiveModel _temporalSelection;
        public ArchiveModel TemporalSelection
        {
            get => _temporalSelection;
            set
            {
                _temporalSelection = value;
                _archiveTemporalStore.CurrentArchive = _temporalSelection;
                OnPropertyChanged();
            }
        }

        public bool IsSelected => _archiveTemporalStore.IsSelected;


        // STORES

        private readonly ObservableCollection<ArchiveModel> _archives;
        public IEnumerable<ArchiveModel> Archives => _archives;

        private ArchivesListStore _store;
        private ArchiveSelectedStore _selectedStore;
        public ArchiveModel SelectedStore => _selectedStore.CurrentArchive;


        public ICommand LoadArchives { get; }
        public ICommand CreateArchive { get; }
        public ICommand SearchArchive { get; }
        public ICommand OpenArchive { get; }


        public ArchivesViewModel(ArchivesListStore store, 
            ArchiveSelectedStore selected,
            ArchiveTemporalStore temporal,
            FolderListStore folderStore,
            ProjectListStore projectStore,
            SubBudgetListStore subStore)
        {
            _archives = new ObservableCollection<ArchiveModel>();
            _store = store;
            _selectedStore = selected;
            _archiveTemporalStore = temporal;

            LoadArchives = new ArchiveLoadCommand(this, store);
            CreateArchive = new ArchiveCreateCommand(store);
            SearchArchive = new ArchiveSearchCommand(store);
            OpenArchive = new ArchiveOpenCommand(store, temporal, selected, folderStore, projectStore, subStore);

            temporal.Deselected();
            _store.ArchiveChanges += ArchiveChanged;
            _selectedStore.CurrentArchiveChanged += OnArchiveChanged;

        }

        private void ArchiveChanged()
        {
            UpdateArchives(_store.Archives);
        }

        public static ArchivesViewModel LoadViewModel(ArchivesListStore store, 
            ArchiveSelectedStore selected,  
            ArchiveTemporalStore temporal,
            FolderListStore folderStore,
            ProjectListStore projectStore,
            SubBudgetListStore subStore)
        {
            ArchivesViewModel viewModel = new ArchivesViewModel(store, selected, temporal, folderStore, projectStore, subStore);
            viewModel.LoadArchives.Execute(null);
            return viewModel;

        }


        public void UpdateArchives(IEnumerable<ArchiveModel> archives)
        {
            _archives.Clear();

            foreach (ArchiveModel archive in archives)
            {
                _archives.Add(archive);
            }
        }

        private void OnArchiveChanged()
        {
            OnPropertyChanged(nameof(SelectedStore));
        }

    }
}
