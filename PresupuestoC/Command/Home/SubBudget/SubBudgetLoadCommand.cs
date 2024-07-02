using PresupuestoC.MVVM;
using PresupuestoC.Stores.Project;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.Home2;
using PresupuestoC.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Home.SubBudget
{
    public class SubBudgetLoadCommand : AsyncCommand
    {
        private readonly ProjectViewModel _viewModel;
        private readonly SubBudgetListStore _store;


        public SubBudgetLoadCommand(ProjectViewModel viewModel, SubBudgetListStore store)
        {
            _viewModel = viewModel;
            _store = store;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.Load();
                _viewModel.UpdateSubBudgets(_store.SubBudgets);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos relacionados con los sub-presupuestos", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
