using PresupuestoC.Command.Currency;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Services;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace PresupuestoC.ViewModels.Currency
{
    public class CurrencyListViewModel : ViewModelBase
    {
        // FILTERS
        public ICollectionView CurrencyCollection { get; }
        private string _currencyFilterSymbol = string.Empty;
        public string CurrencyFilterSymbol 
        {
            get
            {
                return _currencyFilterSymbol;
            }
            set
            {
                _currencyFilterSymbol = value;
                OnPropertyChanged(nameof(CurrencyFilterSymbol));
                CurrencyCollection.Refresh();
            }
        }

        private string _currencyFilterDescription = string.Empty;
        public string CurrencyFilterDescription
        {
            get
            {
                return _currencyFilterDescription;
            }
            set
            {
                _currencyFilterDescription = value;
                OnPropertyChanged(nameof(CurrencyFilterDescription));
                CurrencyCollection.Refresh();
            }
        }


        // STORES
        private readonly ObservableCollection<CurrencyModel> _currencies;
        private CurrencySelectedStore _selectedStore;
       
        public CurrencyModel Selected
        {
            get => _selectedStore.CurrentCurrency; 
            set => _selectedStore.CurrentCurrency = value;                
        }

        public bool IsSelected => (_selectedStore.IsSelected);

        public IEnumerable<CurrencyModel> Currencies => _currencies;

        public ICommand LoadCurrencies { get;  }

        public ICommand SelectCurrency { get; }
        public ICommand CloseModal { get; }


        // CONSTRUCTOR
        public CurrencyListViewModel(CurrencyListStore store,
            CurrencySelectedStore selectedStore,
            CloseNavigationService close,
            ProjectTemporalStore project)
            
        {
            _selectedStore = selectedStore;
            _selectedStore.CurrentCurrencyChanged += CurrencyChanged;
            _currencies = new ObservableCollection<CurrencyModel>();
            LoadCurrencies = new CurrencyLoadCommand(this, store);
            CloseModal = new CloseNavigateCommand(close);
            SelectCurrency = new CurrencySelectedCommand(selectedStore, project, close);


            CurrencyCollection = CollectionViewSource.GetDefaultView(_currencies);
            CurrencyCollection.Filter = FilterCurrency;

            _selectedStore.Deselected();
        }

        public static CurrencyListViewModel LoadViewModel(CurrencyListStore store, CurrencySelectedStore selectedStore, CloseNavigationService close, ProjectTemporalStore project)
        {
            CurrencyListViewModel viewModel = new CurrencyListViewModel(store, selectedStore, close, project);
            viewModel.LoadCurrencies.Execute(null);
            return viewModel;
        }


        private void CurrencyChanged()
        {
            OnPropertyChanged(nameof(IsSelected));
            OnPropertyChanged(nameof(Selected));
        }


        public void UpdateCurrencies(IEnumerable<CurrencyModel> currencies)
        {
            _currencies.Clear();

            foreach (CurrencyModel currency in currencies)
            {
                _currencies.Add(currency);
            }
        }       

        private bool FilterCurrency(object obj)
        {
            if (obj is CurrencyModel currencyModel)
            {
                return currencyModel.Description.Contains(CurrencyFilterDescription, StringComparison.InvariantCultureIgnoreCase) 
                    && currencyModel.Symbol.Contains(CurrencyFilterSymbol, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;


        }
    }   


}