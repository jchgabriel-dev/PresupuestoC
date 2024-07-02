using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Models.Archive
{
    public class SubBudgetModel : DomainModel
    {

        public string Description { get; set; }
        public bool Active { get; set; }
        public int Work {  get; set; }

        public int Direct {  get; set; }
        public int Total { get; set; }

        public int Order { get; set; }

        public int ProjectId { get; set; }
        public ProjectModel Project { get; set; }

    }
}
