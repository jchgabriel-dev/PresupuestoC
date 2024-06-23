using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Models
{
    public class ProvinceModel : DomainModel
    {
    
        public string Name { get; set; }
        public int RegionId { get; set; }
        public RegionModel Region { get; set; }

        public ICollection<DistrictModel> Districts { get; set; }
    }
}
