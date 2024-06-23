using PresupuestoC.Command.Currency;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Currency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Currency
{
    public class CurrencyEditViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly ErrorsViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool CanCreate => !HasErrors;
        public bool HasErrors => _errorsViewModel.HasErrors;


        private readonly CurrencySelectedStore _currencyStore;
        private string _symbol;
        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value;
                _errorsViewModel.ClearErrors(nameof(Symbol));
                if (string.IsNullOrWhiteSpace(_symbol))
                {
                    _errorsViewModel.AddError(nameof(Symbol), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Symbol));

            }
        }

        private string _decription;
        public string Description
        {
            get
            {
                return _decription;
            }
            set
            {
                _decription = value;
                _errorsViewModel.ClearErrors(nameof(Description));
                if (string.IsNullOrWhiteSpace(_decription))
                {
                    _errorsViewModel.AddError(nameof(Description), "Este campo es obligatorio");
                }
                OnPropertyChanged(nameof(Description));

            }
        }

        public ICommand EditCurrency { get; }
        public ICommand Cancel { get; }


        public CurrencyEditViewModel(CurrencySelectedStore selectedStore,
            CurrencyNavigationService<CurrencyListViewModel> navigate,
            CurrencyListStore store) 
        {
            EditCurrency = new CurrencyEditCommand(this, navigate, store, selectedStore);
            Cancel = new CurrencyNavigateCommand<CurrencyListViewModel>(navigate);

            _currencyStore = selectedStore;
            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            Symbol = _currencyStore.CurrentCurrency.Symbol;
            Description = _currencyStore.CurrentCurrency.Description;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanCreate));
        }

    }
}
