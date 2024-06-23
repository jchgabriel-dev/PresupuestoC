using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Stores;
using PresupuestoC.ViewModels.Currency;
using PresupuestoC.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Project
{
   
    public class ProjectNameUpper : AsyncCommand
    {
        private readonly ProjectCreateViewModel _viewModel;
     

        public ProjectNameUpper(ProjectCreateViewModel viewModel)
        {
            _viewModel = viewModel;      ;
        }


        public async override Task ExecuteAsync(object parameter)
        {           
            try
            {
                string name = _viewModel.Name;

                if (name != null) 
                {
                    _viewModel.Name = name.ToUpper();

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de proceso", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
