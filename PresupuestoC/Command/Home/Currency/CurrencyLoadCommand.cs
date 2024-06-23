using PresupuestoC.Models;
using PresupuestoC.MVVM;
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
    public class CurrencyLoadCommand : AsyncCommand
    {
        private readonly CurrencyListViewModel _viewModel;
        private readonly CurrencyListStore _store;


        public CurrencyLoadCommand(CurrencyListViewModel viewModel, CurrencyListStore store)
        {
            _viewModel = viewModel;
            _store = store;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.Load();
                _viewModel.UpdateCurrencies(_store.Currencies);

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error al cargar los datos relacionados con las monedas", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
