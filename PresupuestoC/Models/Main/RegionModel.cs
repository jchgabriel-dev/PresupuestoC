using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Models
{
    public class RegionModel : DomainModel
    {
   
        public string Name { get; set; }
        public ICollection<ProvinceModel> Provinces { get; set; }
    }
}
