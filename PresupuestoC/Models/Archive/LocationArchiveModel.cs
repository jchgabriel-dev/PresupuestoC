using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Models.Archive
{
    public class LocationArchiveModel : DomainModel
    {
        public string Region { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public ProjectModel Project { get; set; }
    }
}
