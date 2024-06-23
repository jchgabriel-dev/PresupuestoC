using Microsoft.EntityFrameworkCore;
using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.Services.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.SubBudget
{

    public class SubBudgetService : ISubBudgetService
    {
        private readonly IArchiveContextFactory _dbContextFactory;

        public SubBudgetService(IArchiveContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<SubBudgetModel> Create(SubBudgetModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.SubBudgets.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                SubBudgetModel entity = await context.SubBudgets.FirstOrDefaultAsync((e) => e.Id == id);
                context.SubBudgets.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<SubBudgetModel> Get(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                SubBudgetModel entity = await context.SubBudgets.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<SubBudgetModel>> GetAll()
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SubBudgetModel> entities = await context.SubBudgets.ToListAsync();
                return entities;
            }
        }


        public async Task<SubBudgetModel> Update(int id, SubBudgetModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.SubBudgets.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
