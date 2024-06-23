using Microsoft.EntityFrameworkCore;
using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.Services.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Position
{
    public class PositionService : IPositionService
    {
        private readonly IArchiveContextFactory _dbContextFactory;

        public PositionService(IArchiveContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<LocationModel> Create(LocationModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Locations.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                LocationModel entity = await context.Locations.FirstOrDefaultAsync((e) => e.Id == id);
                context.Locations.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<LocationModel> Get(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                LocationModel entity = await context.Locations.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<LocationModel>> GetAll()
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<LocationModel> entities = await context.Locations.ToListAsync();
                return entities;
            }
        }


        public async Task<LocationModel> Update(int id, LocationModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Locations.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
