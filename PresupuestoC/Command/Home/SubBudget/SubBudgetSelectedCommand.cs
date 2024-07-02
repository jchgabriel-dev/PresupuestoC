using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Main;
using PresupuestoC.Stores.Project;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Home.SubBudget
{

    public class SubBudgetSelectedCommand : AsyncCommand
    {
        private readonly BudgetViewModel _viewModel;
        private readonly SubBudgetListStore _store;

        public SubBudgetSelectedCommand(BudgetViewModel viewModel, SubBudgetListStore store)
        {
            _viewModel = viewModel;
            _store = store;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _viewModel.UpdateSubBudgets(_store.SubBudgetsAlt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los sub-presupuestos", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
