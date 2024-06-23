using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Currency;
using PresupuestoC.ViewModels.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Currency
{
    public class CurrencyEditCommand : AsyncCommand
    {
        private readonly CurrencyEditViewModel _viewModel;
        private readonly CurrencyNavigationService<CurrencyListViewModel> _navigation;
        private readonly CurrencyListStore _store;
        private readonly CurrencySelectedStore _selected;

        public CurrencyEditCommand(CurrencyEditViewModel viewModel, CurrencyNavigationService<CurrencyListViewModel> navigation, 
            CurrencyListStore store, CurrencySelectedStore selected)
        {
            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
            _selected = selected;   
        }


        public async override Task ExecuteAsync(object parameter)
        {
            _viewModel.Symbol = _viewModel.Symbol;
            _viewModel.Description = _viewModel.Description;

            if (_viewModel.HasErrors)
            {
                return;
            }

            CurrencyModel currency = new CurrencyModel();
            currency.Description = _viewModel.Description;
            currency.Symbol = _viewModel.Symbol;


            try
            {
                await _store.UpdateCurrency(_selected.CurrentCurrency.Id ,currency);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar la moneda", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
