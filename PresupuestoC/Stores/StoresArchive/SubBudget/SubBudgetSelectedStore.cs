using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Stores.SubBudget
{

    public class SubBudgetSelectedStore
    {
        private SubBudgetModel _currentSubBudget;

        public SubBudgetModel CurrentSubBudget
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
