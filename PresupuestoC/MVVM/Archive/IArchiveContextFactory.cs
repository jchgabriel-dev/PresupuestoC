using PresupuestoC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.MVVM.Archive
{
  

    public interface IArchiveContextFactory
    {
        public void ChangeConnectionString(string newConnectionString);
        public ArchiveContext CreateArchive(string location);
        public ArchiveContext CreateDbContext();
        public bool Connected();

    }
}
