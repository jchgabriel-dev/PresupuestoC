using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Main;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Project
{
 
    public class ProjectSelectedCommand : AsyncCommand
    {
        private readonly ProjectSelectedStore _selected;
        private readonly NavigationService<BudgetViewModel> _navigate;


        public ProjectSelectedCommand(ProjectSelectedStore selected, NavigationService<BudgetViewModel> navigate)
        {
            _selected = selected;
            _navigate = navigate;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {               
                _navigate.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el proyecto", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
