using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresupuestoC.Models.Archive;

namespace PresupuestoC.Models.Main
{
    public class ClientModel : DomainModel
    {
        public string Name { get; set; }
        public string Address { get; set; }


    }
}
