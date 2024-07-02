using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.SubBudget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Home.SubBudget
{

    public class SubBudgetEditCommand : AsyncCommand
    {
        private readonly SubBudgetEditViewModel _viewModel;
        private readonly CloseNavigationService _navigation;
        private readonly SubBudgetListStore _store;
        private readonly SubBudgetSelectedStore _selected;

        public SubBudgetEditCommand(SubBudgetEditViewModel viewModel, CloseNavigationService navigation, SubBudgetListStore store, SubBudgetSelectedStore selected)
        {
            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
            _selected = selected;

        }


        public async override Task ExecuteAsync(object parameter)
        {

            try
            {
                _viewModel.Description = _viewModel.Description;
                if (_viewModel.LetWork == true)
                {
                    _viewModel.Work = _viewModel.Work;
                }

                if (_viewModel.HasErrors)
                {
                    return;
                }

                SubBudgetModel item = await _store.GetSubBudger(_selected.CurrentSubBudget.Id);
                item.Description = _viewModel.Description;
                item.Active = _viewModel.Active;
                item.Work = Convert.ToInt32(_viewModel.Work);

                await _store.UpdateSubBudget(_selected.CurrentSubBudget.Id, item);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el sub-presupuesto", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
