using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.SubBudget
{

    public class SubBudgetSelectedStore
    {
        private ClientModel _currentSubBudget;

        public ClientModel CurrentSubBudget
        {
            get => _currentSubBudget;
            set
            {
                _currentSubBudget = value;
                CurrentSubBudgetChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentSubBudget != null;

        public event Action CurrentSubBudgetChanged;

        public void Deselected()
        {
            CurrentSubBudget = null;
        }
    }
}
