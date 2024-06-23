using Microsoft.EntityFrameworkCore;
using PresupuestoC.Database;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Client
{
    public class ClientService : IClientService
    {
        private readonly IDatabaseContextFactory _dbContextFactory;

        public ClientService(IDatabaseContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ClientModel> Create(ClientModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Clients.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                ClientModel entity = await context.Clients.FirstOrDefaultAsync((e) => e.Id == id);
                context.Clients.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<ClientModel> Get(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                ClientModel entity = await context.Clients.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<ClientModel>> GetAll()
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ClientModel> entities = await context.Clients.ToListAsync();
                return entities;
            }
        }


        public async Task<ClientModel> Update(int id, ClientModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Clients.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
