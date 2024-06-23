using PresupuestoC.Command.Currency;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Currency
{
    public class CurrencyDeleteViewModel : ViewModelBase
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

        public ICommand DeleteCurrency { get; }
        public ICommand Cancel { get; }

        public CurrencyDeleteViewModel(CurrencySelectedStore selectedStore,
            CurrencyNavigationService<CurrencyListViewModel> navigate,
            CurrencyListStore store)
        {
            DeleteCurrency = new CurrencyDeleteCommand(navigate, store, selectedStore);
            Cancel = new CurrencyNavigateCommand<CurrencyListViewModel>(navigate);
            Description = selectedStore.CurrentCurrency.Description;



        }
    }
}
