using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Client
{
    public class ClientSelectedStore
    {
        private ClientModel _currentClient;

        public ClientModel CurrentClient
        {
            get => _currentClient;
            set
            {
                _currentClient = value;
                CurrentCurrencyChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentClient != null;

        public event Action CurrentCurrencyChanged;

        public void Deselected()
        {
            CurrentClient = null;
        }
    }
}
