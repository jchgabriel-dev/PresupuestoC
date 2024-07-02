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
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.SubBudget;

namespace PresupuestoC.Command.Home.SubBudget
{

    public class SubBudgetDeleteCommand : AsyncCommand
    {
        private readonly CloseNavigationService _navigation;
        private readonly SubBudgetListStore _store;
        private readonly SubBudgetSelectedStore _selected;

        public SubBudgetDeleteCommand(CloseNavigationService navigation,
            SubBudgetListStore store, SubBudgetSelectedStore selected)
        {
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {

            try
            {
                await _store.DeleteSubBudget(_selected.CurrentSubBudget.Id);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el sub-presupuesto", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
