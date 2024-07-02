using Microsoft.EntityFrameworkCore;
using PresupuestoC.Database;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.MVVM.Database;
using PresupuestoC.Services.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Services.Folder
{


    public class FolderService : IFolderService
    {
        private readonly IArchiveContextFactory _dbContextFactory;

        public FolderService(IArchiveContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<FolderModel> Create(FolderModel entity)
        {

            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Folders.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                FolderModel entity = await context.Folders.FirstOrDefaultAsync((e) => e.Id == id);
                context.Folders.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<FolderModel> Get(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                FolderModel entity = await context.Folders.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<FolderModel>> GetAll()
        {
            
            if (!_dbContextFactory.Connected())
            {
                return Enumerable.Empty<FolderModel>();
            }

            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<FolderModel> entities = await context.Folders.ToListAsync();
                return entities;
            }
        }


        public async Task<FolderModel> Update(int id, FolderModel entity)
        {
            
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Folders.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }


        // OTROS SERVICIOS 

        public async Task<bool> Check(FolderModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                bool exists = await context.Folders.AnyAsync(a => a.ParentId == entity.ParentId && a.Name == entity.Name);
                return exists;
            }
        }

    }
}
