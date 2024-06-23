using PresupuestoC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Location
{
    public class LocationSelectedStore
    {
        private DistrictModel _currentLocation;

        public DistrictModel CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;
                CurrentLocationChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentLocation != null;

        public event Action CurrentLocationChanged;

        public void Deselected()
        {
            CurrentLocation = null;
        }

    }
}
