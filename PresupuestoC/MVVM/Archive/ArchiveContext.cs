using Microsoft.EntityFrameworkCore;
using PresupuestoC.Models;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.MVVM.Archive
{
    public class ArchiveContext : DbContext
    {
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<ClientArchiveModel> ClientArchives { get; set; }
        public DbSet<CurrencyArchiveModel> CurrencyArchives { get; set; }
        public DbSet<LocationArchiveModel> LocationArchives { get; set; }


        public DbSet<StateModel> States { get; set; }
        public DbSet<FolderModel> Folders { get; set; }
        public DbSet<SubBudgetModel> SubBudgets { get; set; }


        public ArchiveContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureModels(modelBuilder);
            SeedData(modelBuilder);
        }

        private void ConfigureModels(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProjectModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();

                entity.HasOne(p => p.LocationArchive)
                    .WithOne(a => a.Project)
                    .HasForeignKey<ProjectModel>(p => p.LocationArchiveId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.ClientArchive)
                   .WithOne(a => a.Project)
                   .HasForeignKey<ProjectModel>(p => p.ClientArchiveId)
                   .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.CurrencyArchive)
                   .WithOne(a => a.Project)
                   .HasForeignKey<ProjectModel>(p => p.CurrencyArchiveId)
                   .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Folder)
                    .WithMany(a => a.Projects)
                    .HasForeignKey(p => p.FolderId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.State)
                    .WithMany(a => a.Projects)
                    .HasForeignKey(p => p.StateId)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<LocationArchiveModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Region).IsRequired();
                entity.Property(p => p.Province).IsRequired();
                entity.Property(p => p.District).IsRequired();

            });

            modelBuilder.Entity<ClientArchiveModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<CurrencyArchiveModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Symbol).IsRequired();
            });

            modelBuilder.Entity<StateModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<FolderModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.ParentId).IsRequired(false);

                entity.HasOne(p => p.Parent)
                    .WithMany(a => a.Children)
                    .HasForeignKey(p => p.ParentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            

            modelBuilder.Entity<SubBudgetModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Description).IsRequired();

                entity.HasOne(p => p.Project)
                    .WithMany(a => a.SubBudgets)
                    .HasForeignKey(p => p.ProjectId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StateModel>().HasData(
                SeedDataHelper.GetStatesFromJson().ToArray()
            );

            modelBuilder.Entity<FolderModel>().HasData(
                SeedDataHelper.GetFoldersFromJson().ToArray()
            );




        }
    }
}
