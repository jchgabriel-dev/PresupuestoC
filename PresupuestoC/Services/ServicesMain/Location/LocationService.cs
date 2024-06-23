using Microsoft.EntityFrameworkCore;
using PresupuestoC.Database;
using PresupuestoC.Models;
using PresupuestoC.MVVM.Database;
using PresupuestoC.Services.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly IDatabaseContextFactory _dbContextFactory;

        public LocationService(IDatabaseContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<RegionModel> CreateRegion(RegionModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Regions.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }
        public async Task<ProvinceModel> CreateProvince(ProvinceModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Provinces.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }
        public async Task<DistrictModel> CreateDistrict(DistrictModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                var addedEntity = await context.Districts.AddAsync(entity);
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
        }

        public async Task<RegionModel> GetRegion(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                RegionModel entity = await context.Regions.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }
        public async Task<ProvinceModel> GetProvince(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                ProvinceModel entity = await context.Provinces.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }
        public async Task<DistrictModel> GetDistrict(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                DistrictModel entity = await context.Districts.FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }


        public async Task<bool> DeleteRegion(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                RegionModel entity = await context.Regions.FirstOrDefaultAsync((e) => e.Id == id);
                context.Regions.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> DeleteProvince(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                ProvinceModel entity = await context.Provinces.FirstOrDefaultAsync((e) => e.Id == id);
                context.Provinces.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> DeleteDistrict(int id)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                DistrictModel entity = await context.Districts.FirstOrDefaultAsync((e) => e.Id == id);
                context.Districts.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }


        public async Task<RegionModel> UpdateRegion(int id, RegionModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Regions.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<ProvinceModel> UpdateProvince(int id, ProvinceModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Provinces.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<DistrictModel> UpdateDistrict(int id, DistrictModel entity)
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {

                entity.Id = id;
                context.Districts.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }



        public async Task<IEnumerable<DistrictModel>> GetAllDistrict()
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<DistrictModel> entities = await context.Districts
                    .Include(d => d.Province)
                    .ThenInclude(p => p.Region)
                    .ToListAsync();
               
                return entities;
            }
        }

        public async Task<IEnumerable<ProvinceModel>> GetAllProvince()
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ProvinceModel> entities = await context.Provinces.ToListAsync();
                return entities;
            }
        }

        public async Task<IEnumerable<RegionModel>> GetAllRegion()
        {
            using (DatabaseContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<RegionModel> entities = await context.Regions.ToListAsync();
                return entities;
            }
        }        
    }
}
