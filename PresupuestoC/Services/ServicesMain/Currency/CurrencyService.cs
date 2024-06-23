using Microsoft.EntityFrameworkCore;
using PresupuestoC.Database;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IDatabaseContextFactory _dbContextFactory;

        public CurrencyService(IDatabaseContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<CurrencyModel> Create(CurrencyModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Currencies.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                CurrencyModel entity = await context.Currencies.FirstOrDefaultAsync((e) => e.Id == id);
                context.Currencies.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<CurrencyModel> Get(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                CurrencyModel entity = await context.Currencies.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<CurrencyModel>> GetAll()
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CurrencyModel> entities = await context.Currencies.ToListAsync();
                return entities;
            }
        }


        public async Task<CurrencyModel> Update(int id, CurrencyModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Currencies.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
