using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Services.Currency;
using PresupuestoC.Stores;
using PresupuestoC.ViewModels.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Currency
{
    public class CurrencyCreateCommand : AsyncCommand
    {
        private readonly CurrencyCreateViewModel _viewModel;
        private readonly CurrencyNavigationService<CurrencyListViewModel> _navigation;
        private readonly CurrencyListStore _store;

        public CurrencyCreateCommand(CurrencyCreateViewModel viewModel, CurrencyNavigationService<CurrencyListViewModel> navigation, CurrencyListStore store)
        {

            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
        }


        public async override Task ExecuteAsync(object parameter)
        {
                       

            try
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

                await _store.CreateCurrency(currency);                 
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la moneda", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
