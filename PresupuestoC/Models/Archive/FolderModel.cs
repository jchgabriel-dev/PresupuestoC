using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Models.Archive
{
    public class FolderModel : DomainModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int Type { get; set; }

        public bool? Expanded { get; set; }
        public bool? Selected { get; set; }

        public FolderModel Parent { get; set; }
        public ICollection<FolderModel> Children { get; set; } = new Collection<FolderModel>();
        public ICollection<ProjectModel> Projects { get; set; }

    }
}
