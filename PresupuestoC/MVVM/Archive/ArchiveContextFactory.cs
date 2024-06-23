using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PresupuestoC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.MVVM.Archive
{
  

    public class ArchiveContextFactory : IArchiveContextFactory
    {
        private string? _connectionString;
      
        public void ChangeConnectionString(string newConnectionString)
        {
            if (_connectionString != null)
            {
                using (ArchiveContext context = CreateDbContext())
                {
                    context.Database.EnsureCreated();
                    SqliteConnection.ClearPool((SqliteConnection)context.Database.GetDbConnection());
                }
            }
                                    
            string connectionString = $"Data Source={newConnectionString}";
            _connectionString = connectionString;
        }

        public ArchiveContext CreateDbContext()
        {
                        
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new ArchiveContext(options);
        }


        public ArchiveContext CreateArchive(string location)
        {
            string connectionString = $"Data Source={location}";
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(connectionString).Options;
            return new ArchiveContext(options);
        }

        public bool Connected()
        {
            return !string.IsNullOrEmpty(_connectionString);
        }

    }
}
