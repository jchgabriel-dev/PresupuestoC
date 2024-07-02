using PresupuestoC.MVVM;
using PresupuestoC.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Home.Project
{
   

    public class ProjectNameUpperAlt : AsyncCommand
    {
        private readonly ProjectEditViewModel _viewModel;


        public ProjectNameUpperAlt(ProjectEditViewModel viewModel)
        {
            _viewModel = viewModel; ;
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
