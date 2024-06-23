using PresupuestoC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Location
{
 
    public class RegionSelectedStore
    {
        private RegionModel _currentRegion;

        public RegionModel CurrentRegion
        {
            get => _currentRegion;
            set
            {
                _currentRegion = value;
                CurrentRegionChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentRegion != null;

        public event Action CurrentRegionChanged;

        public void Deselected()
        {
            CurrentRegion = null;
        }
    }
}
