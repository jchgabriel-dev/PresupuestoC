using PresupuestoC.Command.Currency;
using PresupuestoC.Command.Home.SubBudget;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.Currency;
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

        private string _confirmation = string.Empty;
        public string Confirmation
        {
            get
            {
                return _confirmation;
            }
            set
            {
                _confirmation = value;
                OnPropertyChanged(nameof(Confirmation));
                OnPropertyChanged(nameof(CanDelete));
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public bool CanDelete => Confirmation.Equals("SI", StringComparison.OrdinalIgnoreCase);

        public ICommand DeleteSubBudget{ get; }
        public ICommand CloseModal { get; }

        public SubBudgetDeleteViewModel(SubBudgetSelectedStore selectedStore,
            CloseNavigationService navigate,
            SubBudgetListStore store)
        {
            DeleteSubBudget = new SubBudgetDeleteCommand(navigate, store, selectedStore);
            CloseModal = new CloseNavigateCommand(navigate);
            Description = selectedStore.CurrentSubBudget.Description;

        }
    }
}
