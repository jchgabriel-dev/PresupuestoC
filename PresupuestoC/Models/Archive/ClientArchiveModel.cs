using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Models.Archive
{
    public class ClientArchiveModel : DomainModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ProjectModel Project { get; set; }

    }
}
