using PresupuestoC.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.SubBudget
{
    public class SubBudgetDeleteViewModel : ViewModelBase
    {
        public ICommand CloseModal { get; }

    }
}
