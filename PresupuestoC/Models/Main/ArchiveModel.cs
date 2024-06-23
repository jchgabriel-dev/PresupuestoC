using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Models.Main
{
    public class ArchiveModel : DomainModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateOnly Creation { get; set; }
        public bool? Selected { get; set; }
    }
}
