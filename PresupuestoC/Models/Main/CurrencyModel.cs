using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresupuestoC.Models.Archive;

namespace PresupuestoC.Models.Main
{
    public class CurrencyModel : DomainModel
    {
        public string Description { get; set; }
        public string Symbol { get; set; }

    }
}
