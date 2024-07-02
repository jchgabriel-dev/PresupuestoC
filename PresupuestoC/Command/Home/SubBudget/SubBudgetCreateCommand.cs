using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.Currency;
using PresupuestoC.ViewModels.SubBudget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Home.SubBudget
{
 
    public class SubBudgetCreateCommand : AsyncCommand
    {
        private readonly SubBudgetCreateViewModel _viewModel;
        private readonly CloseNavigationService _navigation;
        private readonly SubBudgetListStore _store;        

        public SubBudgetCreateCommand(SubBudgetCreateViewModel viewModel, CloseNavigationService navigation, SubBudgetListStore store)
        {

            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
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
                int order = await _store.GetOrder(_viewModel.Project.Id);

                SubBudgetModel sub = new SubBudgetModel();
                sub.Description = _viewModel.Description;
                sub.Active = _viewModel.Active;
                sub.Work = Convert.ToInt32(_viewModel.Work);
                sub.ProjectId = _viewModel.Project.Id;
                sub.Order = order + 1;

                await _store.CreateSubBudget(sub);

                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el sub-presupuesto", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
