using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresupuestoC.Models.Archive;

namespace PresupuestoC.Models
{
    public class DistrictModel : DomainModel
    {
        
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public ProvinceModel Province { get; set; }

    }

}
