using PresupuestoC.Database;
using PresupuestoC.MVVM.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.MVVM.Database
{
  
    public interface IDatabaseContextFactory
    {
    
        public DatabaseContext CreateDbContext();

    }
}
