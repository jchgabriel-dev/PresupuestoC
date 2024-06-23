using PresupuestoC.Command.Currency;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Services.Currency;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Currency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Currency
{
    
    public class CurrencyCreateViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private CurrencySelectedStore _selectedStore;

        private readonly ErrorsViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool CanCreate => !HasErrors;
        public bool HasErrors => _errorsViewModel.HasErrors;


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

        public ICommand CreateCurrency { get; }
        public ICommand Cancel { get; }


        public CurrencyCreateViewModel(CurrencySelectedStore selectedStore,
            CurrencyNavigationService<CurrencyListViewModel> navigate,
            CurrencyListStore store)
        {
            CreateCurrency = new CurrencyCreateCommand(this, navigate, store);
            Cancel = new CurrencyNavigateCommand<CurrencyListViewModel>(navigate);

            _selectedStore = selectedStore;
            _errorsViewModel = new ErrorsViewModel();            

            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
            _selectedStore.Deselected();

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
