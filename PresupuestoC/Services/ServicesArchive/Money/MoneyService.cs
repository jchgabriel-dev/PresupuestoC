using Microsoft.EntityFrameworkCore;
using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.Services.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Money
{


    public class MoneyService : IMoneyService
    {
        private readonly IArchiveContextFactory _dbContextFactory;

        public MoneyService(IArchiveContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<MoneyModel> Create(MoneyModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Moneys.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                MoneyModel entity = await context.Moneys.FirstOrDefaultAsync((e) => e.Id == id);
                context.Moneys.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<MoneyModel> Get(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                MoneyModel entity = await context.Moneys.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<MoneyModel>> GetAll()
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MoneyModel> entities = await context.Moneys.ToListAsync();
                return entities;
            }
        }


        public async Task<MoneyModel> Update(int id, MoneyModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Moneys.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
