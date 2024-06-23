using Microsoft.EntityFrameworkCore;
using PresupuestoC.Database;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.MVVM.Database;
using PresupuestoC.Services.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Customer
{
  
    public class CustomerService : ICustomerService
    {
        private readonly IArchiveContextFactory _dbContextFactory;

        public CustomerService(IArchiveContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<CustomerModel> Create(CustomerModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Customers.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                CustomerModel entity = await context.Customers.FirstOrDefaultAsync((e) => e.Id == id);
                context.Customers.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<CustomerModel> Get(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                CustomerModel entity = await context.Customers.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<CustomerModel>> GetAll()
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CustomerModel> entities = await context.Customers.ToListAsync();
                return entities;
            }
        }


        public async Task<CustomerModel> Update(int id, CustomerModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Customers.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
