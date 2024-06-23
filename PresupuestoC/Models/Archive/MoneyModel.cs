using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Models.Archive
{
    public class MoneyModel : DomainModel
    {
        public string Description { get; set; }
        public string Symbol { get; set; }     
        public ProjectModel Project { get; set; }

    }
}
