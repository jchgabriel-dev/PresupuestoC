using Microsoft.EntityFrameworkCore;
using PresupuestoC.MVVM.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Database
{
    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        private readonly string _connectionString;
        
        
        public DatabaseContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public DatabaseContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new DatabaseContext(options);
        }
    }
}
