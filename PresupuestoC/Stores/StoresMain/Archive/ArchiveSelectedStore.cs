using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.Services.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace PresupuestoC.Stores.Archive
{
    public class ArchiveSelectedStore
    {
        private readonly IArchiveService _archiveService;
        private readonly IArchiveContextFactory _archiveContextFactory;
        private readonly Lazy<Task> _initializeLazy;
        private ArchiveModel _currentArchive;


        public ArchiveSelectedStore(IArchiveService archiveService, IArchiveContextFactory archiveFactor)
        {
            _archiveService = archiveService;
            _archiveContextFactory = archiveFactor;
            _initializeLazy = new Lazy<Task>(Initialize);
        }


        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        private async Task Initialize()
        {
            ArchiveModel archive = await _archiveService.GetSelected();
            if (archive != null && System.IO.File.Exists(System.IO.Path.Combine(archive?.Address, archive?.Name)))
            {
                CurrentArchive = archive;
            }         
        }

        public bool IsSelected => CurrentArchive != null;

        public event Action CurrentArchiveChanged;

        public void Deselected()
        {
            CurrentArchive = null;
        }


        public ArchiveModel CurrentArchive
        {
            get => _currentArchive;
            set
            {
                if(value != null)
                {
                    _currentArchive = value;
                    _archiveContextFactory.ChangeConnectionString(System.IO.Path.Combine(_currentArchive.Address, _currentArchive.Name));                    
                    CurrentArchiveChanged?.Invoke();
                }                
            }
        }             
    }
}
