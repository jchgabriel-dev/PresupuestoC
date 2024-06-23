using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresupuestoC.Models.Main;

namespace PresupuestoC.Models.Archive
{
    public class ProjectModel : DomainModel
    {

        public string Name { get; set; }
        public string? Place { get; set; }
        public DateOnly Date { get; set; }
        public int Work { get; set; }
        public string Group { get; set; }
        public string? LetterheadRight { get; set; }
        public string? LetterheadLeft { get; set; }
        public bool IGV { get; set; }



        public int LocationId { get; set; }
        public LocationModel Location { get; set; }

        public int MoneyId { get; set; }
        public MoneyModel Money { get; set; }

        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }

        public int FolderId { get; set; }
        public FolderModel Folder { get; set; }

        public int StateId { get; set; }
        public StateModel State { get; set; }


        public ICollection<SubBudgetModel> SubBudgets { get; set; }


    }
}
