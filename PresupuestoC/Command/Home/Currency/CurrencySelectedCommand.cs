using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Currency
{ 
    public class CurrencySelectedCommand : AsyncCommand
    {
        private readonly CurrencySelectedStore _selected;
        private readonly ProjectTemporalStore _project;
        private readonly CloseNavigationService _navigate;


        public CurrencySelectedCommand(CurrencySelectedStore selected, ProjectTemporalStore project, CloseNavigationService navigate)
        {
            _selected = selected;
            _project = project;      
            _navigate = navigate;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                if (_selected.CurrentCurrency == null)
                    return;

                _project.CurrentCurrency = _selected.CurrentCurrency;
                _navigate.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar la moneda", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
