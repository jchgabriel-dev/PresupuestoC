using Microsoft.EntityFrameworkCore;
using PresupuestoC.Models;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Database
{
    public class DatabaseContext : DbContext
    {

        public DbSet<CurrencyModel> Currencies { get; set; }
        public DbSet<ClientModel> Clients { get; set; }       
        public DbSet<ArchiveModel> Archives { get; set; }
        public DbSet<RegionModel> Regions { get; set; }
        public DbSet<DistrictModel> Districts { get; set; }
        public DbSet<ProvinceModel> Provinces { get; set; }

        


        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureModels(modelBuilder);
            SeedData(modelBuilder);
        }

        private void ConfigureModels(ModelBuilder modelBuilder)
        {
                       

            modelBuilder.Entity<CurrencyModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Description).IsRequired();
            });

            modelBuilder.Entity<ClientModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();

            });
            

            modelBuilder.Entity<RegionModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<ProvinceModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();

                entity.HasOne(p => p.Region)
                    .WithMany(a => a.Provinces)
                    .HasForeignKey(p => p.RegionId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DistrictModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();

                entity.HasOne(p => p.Province)
                    .WithMany(a => a.Districts)
                    .HasForeignKey(p => p.ProvinceId)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<ArchiveModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Address).IsRequired();
            });            

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegionModel>().HasData(
                SeedDataHelper.GetRegionsFromJson().ToArray()
            );

            modelBuilder.Entity<ProvinceModel>().HasData(
                SeedDataHelper.GetProvincesFromJson().ToArray()
            );

            modelBuilder.Entity<DistrictModel>().HasData(
                SeedDataHelper.GetDistrictsFromJson().ToArray()
            );

          

            modelBuilder.Entity<ClientModel>().HasData(
                SeedDataHelper.GetClientsFromJson().ToArray()
            );

            modelBuilder.Entity<CurrencyModel>().HasData(
                SeedDataHelper.GetCurrenciesFromJson().ToArray()
            );

        }
    }
}
