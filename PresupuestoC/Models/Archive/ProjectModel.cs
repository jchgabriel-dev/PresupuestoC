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



        public int LocationArchiveId { get; set; }
        public LocationArchiveModel LocationArchive { get; set; }

        public int CurrencyArchiveId { get; set; }
        public CurrencyArchiveModel CurrencyArchive { get; set; }

        public int ClientArchiveId { get; set; }
        public ClientArchiveModel ClientArchive { get; set; }

        public int FolderId { get; set; }
        public FolderModel Folder { get; set; }

        public int StateId { get; set; }
        public StateModel State { get; set; }


        public ICollection<SubBudgetModel> SubBudgets { get; set; }


    }
}
