using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Currency
{
    public class CurrencySelectedStore
    {
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

        public bool IsSelected => CurrentCurrency != null;

        public event Action CurrentCurrencyChanged;

        public void Deselected()
        {
            CurrentCurrency = null;
        }
    }
}
