using PresupuestoC.Models;
using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Stores.Project
{


    public class ProjectTemporalStore
    {
     
        //

        private CurrencyModel _currentCurrency;

        public CurrencyModel CurrentCurrency
        {
            get => _currentCurrency;
            set
            {

                _currentCurrency = value;
                CurrentCurrencyChanged?.Invoke();
            }
        }

        public bool IsSelectedCurrency => CurrentCurrency != null;

        //

        private DistrictModel _currentDistrict;

        public DistrictModel CurrentDistrict
        {
            get => _currentDistrict;
            set
            {
                _currentDistrict = value;
                CurrentLocationChanged?.Invoke();
            }
        }

        public bool IsSelectedDistrict => CurrentDistrict != null;


        //

      
        private ClientModel _currentClient;

        public ClientModel CurrentClient
        {
            get => _currentClient;
            set
            {
                _currentClient = value;
                CurrentClientChanged?.Invoke();
            }
        }

        public bool IsSelectedClient => CurrentClient != null;


        public event Action CurrentLocationChanged;
        public event Action CurrentCurrencyChanged;
        public event Action CurrentClientChanged;


        public void DeselectedCurrency()
        {
            CurrentCurrency = null;
        }
        public void DeselectedLocation()
        {
            CurrentDistrict = null;
        }
        public void DeselectedClient()
        {
            CurrentClient = null;
        }
    }
}
