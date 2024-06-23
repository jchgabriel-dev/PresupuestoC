using PresupuestoC.Models;
using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Location
{


    public class ProvinceSelectedStore
    {
        private ProvinceModel _currentProvince;

        public ProvinceModel CurrentProvince
        {
            get => _currentProvince;
            set
            {
                _currentProvince = value;
                CurrentProvinceChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentProvince != null;

        public event Action CurrentProvinceChanged;

        public void Deselected()
        {
            CurrentProvince = null;
        }
    }
}
