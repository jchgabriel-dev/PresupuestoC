using PresupuestoC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Location
{

    public class DistrictSelectedStore
    {
        private DistrictModel _currentDistrict;

        public DistrictModel CurrentDistrict
        {
            get => _currentDistrict;
            set
            {
                _currentDistrict = value;
                CurrentDistrictChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentDistrict != null;

        public event Action CurrentDistrictChanged;

        public void Deselected()
        {
            CurrentDistrict = null;
        }
    }
}
