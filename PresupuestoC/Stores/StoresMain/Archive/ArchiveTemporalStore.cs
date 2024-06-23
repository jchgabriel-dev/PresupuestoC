using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Archive
{

    public class ArchiveTemporalStore
    {
        private ArchiveModel _currentArchive;

        public ArchiveModel CurrentArchive
        {
            get => _currentArchive;
            set
            {
                _currentArchive = value;
                CurrentArchiveChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentArchive != null;

        public event Action CurrentArchiveChanged;

        public void Deselected()
        {
            CurrentArchive = null;
        }
    }
}
