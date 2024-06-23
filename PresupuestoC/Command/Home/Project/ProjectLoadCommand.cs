using PresupuestoC.MVVM;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Client;
using PresupuestoC.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Project
{
    
    public class ProjectLoadCommand : AsyncCommand
    {
        private readonly ProjectListViewModel _viewModel;
        private readonly ProjectListStore _store;


        public ProjectLoadCommand(ProjectListViewModel viewModel, ProjectListStore store)
        {
            _viewModel = viewModel;
            _store = store;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.Load();
                _viewModel.UpdateProjects(_store.Projects, _store.States);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos relacionados con los proyectos", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
