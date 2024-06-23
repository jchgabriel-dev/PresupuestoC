using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores.Currency;
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
    public class CurrencyDeleteCommand : AsyncCommand
    {
        private readonly CurrencyNavigationService<CurrencyListViewModel> _navigation;
        private readonly CurrencyListStore _store;
        private readonly CurrencySelectedStore _selected;

        public CurrencyDeleteCommand(CurrencyNavigationService<CurrencyListViewModel> navigation,
            CurrencyListStore store, CurrencySelectedStore selected)
        {
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {
                 
            try
            {
                await _store.DeleteCurrency(_selected.CurrentCurrency.Id);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la moneda", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
