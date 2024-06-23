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

        public async Task<ProjectModel> Create(ProjectModel entity)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Projects.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                ProjectModel entity = await context.Projects.FirstOrDefaultAsync((e) => e.Id == id);
                context.Projects.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<ProjectModel> Get(int id)
        {
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                ProjectModel entity = await context.Projects.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<ProjectModel>> GetAll()
        {
            if (!_dbContextFactory.Connected())
            {
                return Enumerable.Empty<ProjectModel>();
            }

            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ProjectModel> entities = await context.Projects.ToListAsync();
                return entities;
            }
        }

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


        public async Task<ProjectModel> Update(int id, ProjectModel entity)
        {
            
            using (ArchiveContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Projects.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }

}
