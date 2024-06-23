using Microsoft.EntityFrameworkCore;
using PresupuestoC.Database;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Database;
using PresupuestoC.Services.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Archive
{

    public class ArchiveService : IArchiveService
    {
        private readonly IDatabaseContextFactory _dbContextFactory;

        public ArchiveService(IDatabaseContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ArchiveModel> Create(ArchiveModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Archives.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                ArchiveModel entity = await context.Archives.FirstOrDefaultAsync((e) => e.Id == id);
                context.Archives.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<ArchiveModel> Get(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                ArchiveModel entity = await context.Archives.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<ArchiveModel> GetSelected()
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
              
                ArchiveModel entity = await context.Archives.FirstOrDefaultAsync((e) => e.Selected == true);
                return entity;
            }
        }

        public async Task<IEnumerable<ArchiveModel>> GetAll()
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ArchiveModel> entities = await context.Archives.ToListAsync();
                return entities;
            }
        }


        public async Task<ArchiveModel> Update(int id, ArchiveModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Archives.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }





        // OTROS SERVICIOS
        public async Task<bool> Check(ArchiveModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                bool exists = await context.Archives.AnyAsync(a => a.Address == entity.Address && a.Name == entity.Name);
                return exists;
            }
        }
    }
}
