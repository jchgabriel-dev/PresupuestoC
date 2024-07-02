using Microsoft.EntityFrameworkCore;
using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM.Archive;
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
            if (!_dbContextFactory.Connected())
            {
                return Enumerable.Empty<SubBudgetModel>();
            }

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

        public async Task<int> GetOrder(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                int count = await context.SubBudgets.CountAsync(sb => sb.ProjectId == id);
                return count;
            }
        }

        public async Task<IEnumerable<SubBudgetModel>> GetProjectSubs(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SubBudgetModel> entities = await context.SubBudgets
                    .Where(sb => sb.ProjectId == id)
                    .ToListAsync();
                return entities;
            }
        }
    }
}
