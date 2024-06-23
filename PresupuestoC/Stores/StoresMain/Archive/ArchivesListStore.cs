using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.Services.Archive;
using PresupuestoC.Services.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Archive
{
    public class ArchivesListStore
    {
        private readonly List<ArchiveModel> _archives;
        private readonly Lazy<Task> _initializeLazy;
        private readonly IArchiveService _archiveService;
        private readonly IArchiveContextFactory _archiveFactory;
        


        public IEnumerable<ArchiveModel> Archives => _archives;

        public Action ArchiveChanges;

        public ArchivesListStore(IArchiveService archiveService, IArchiveContextFactory archiveFactory)
        {
            _archiveService = archiveService;
            _archiveFactory = archiveFactory;

            _initializeLazy = new Lazy<Task>(Initialize);
            _archives = new List<ArchiveModel>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task CreateArchiveReal(string path, ArchiveModel archive)
        {
            ArchiveModel archiveCreated = await _archiveService.Create(archive);
            _archives.Add(archiveCreated);

            using (ArchiveContext context = _archiveFactory.CreateArchive(path))
            {
                context.Database.EnsureCreated();
                SqliteConnection.ClearPool((SqliteConnection)context.Database.GetDbConnection());
            }

            ArchiveChanges?.Invoke();
        }


        public async Task CreateArchiveVirtual(ArchiveModel archive)
        {
            bool created = await _archiveService.Check(archive);
            
            if(created)
            {
                throw new InvalidOperationException();
            }

            ArchiveModel archiveCreated = await _archiveService.Create(archive);
            _archives.Add(archiveCreated);          
            ArchiveChanges?.Invoke();
        }



        public async Task DeleteArchive(int id)
        {
            await _archiveService.Delete(id);
            var item = _archives.FirstOrDefault(c => c.Id == id);
            _archives.Remove(item);
            ArchiveChanges?.Invoke();

        }

        public async Task UpdateArchive(int id, ArchiveModel archive)
        {
            await _archiveService.Update(id, archive);
            var item = _archives.FirstOrDefault(c => c.Id == id);
            item.Selected = archive.Selected;
        }


        public async Task<ArchiveModel> GetArchive(int id)
        {
            return await _archiveService.Get(id);         
        }


        private async Task Initialize()
        {
            IEnumerable<ArchiveModel> archives = await _archiveService.GetAll();
            _archives.Clear();
            _archives.AddRange(archives);
        }

    }
}
