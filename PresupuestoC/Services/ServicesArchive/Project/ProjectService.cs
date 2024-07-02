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

namespace PresupuestoC.Services.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IArchiveContextFactory _dbContextFactory;

        public ProjectService(IArchiveContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }


        // PROJECTS

        public async Task<ProjectModel> CreateProject(ProjectModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Projects.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> DeleteProject(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                ProjectModel entity = await context.Projects.FirstOrDefaultAsync((e) => e.Id == id);
                context.Projects.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                ProjectModel entity = await context.Projects.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<ProjectModel> UpdateProject(int id, ProjectModel entity)
        {

            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Projects.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }


        public async Task<IEnumerable<ProjectModel>> GetAllProjects()
        {
            if (!_dbContextFactory.Connected())
            {
                return Enumerable.Empty<ProjectModel>();
            }

            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ProjectModel> entities = await context.Projects
                    .Include(p => p.CurrencyArchive)
                    .Include(p => p.ClientArchive)
                    .Include(p => p.LocationArchive)
                    .ToListAsync();
                return entities;
            }
        }


        // STATES

        public async Task<IEnumerable<StateModel>> GetAllStates()
        {
            if (!_dbContextFactory.Connected())
            {
                return Enumerable.Empty<StateModel>();
            }

            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<StateModel> entities = await context.States.ToListAsync();
                return entities;
            }
        }

        // CURRENCY ARCHIVE

        public async Task<CurrencyArchiveModel> GetCurrencyArchive(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                CurrencyArchiveModel entity = await context.CurrencyArchives.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<CurrencyArchiveModel> CreateCurrencyArchive(CurrencyArchiveModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.CurrencyArchives.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<CurrencyArchiveModel> UpdateCurrencyArchive(int id, CurrencyArchiveModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.CurrencyArchives.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<bool> DeleteCurrencyArchive(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                CurrencyArchiveModel entity = await context.CurrencyArchives.FirstOrDefaultAsync((e) => e.Id == id);
                context.CurrencyArchives.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        // LOCATION ARCHIVE

        public async Task<LocationArchiveModel> GetLocationArchive(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                LocationArchiveModel entity = await context.LocationArchives.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<LocationArchiveModel> CreateLocationArchive(LocationArchiveModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.LocationArchives.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<LocationArchiveModel> UpdateLocationArchive(int id, LocationArchiveModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.LocationArchives.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<bool> DeleteLocationArchive(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                LocationArchiveModel entity = await context.LocationArchives.FirstOrDefaultAsync((e) => e.Id == id);
                context.LocationArchives.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        // CLIENT ARCHIVE


        public async Task<ClientArchiveModel> GetClientArchive(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                ClientArchiveModel entity = await context.ClientArchives.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<ClientArchiveModel> CreateClientArchive(ClientArchiveModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.ClientArchives.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<ClientArchiveModel> UpdateClientArchive(int id, ClientArchiveModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.ClientArchives.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<bool> DeleteClientArchive(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                ClientArchiveModel entity = await context.ClientArchives.FirstOrDefaultAsync((e) => e.Id == id);
                context.ClientArchives.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

    }

}
