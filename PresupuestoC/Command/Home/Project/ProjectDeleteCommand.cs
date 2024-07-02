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
using PresupuestoC.Navigation.Project;
using PresupuestoC.ViewModels.Project;
using PresupuestoC.Stores.Project;

namespace PresupuestoC.Command.Project
{

    public class ProjectDeleteCommand : AsyncCommand
    {
        private readonly ProjectNavigationService<ProjectListViewModel> _navigation;
        private readonly ProjectListStore _store;
        private readonly ProjectSelectedStore _selected;

        public ProjectDeleteCommand(ProjectNavigationService<ProjectListViewModel> navigation,
            ProjectListStore store, ProjectSelectedStore selected)
        {
            _navigation = navigation;
            _store = store;
            _selected = selected;
        }


        public async override Task ExecuteAsync(object parameter)
        {

            try
            {
                await _store.DeleteProject(_selected.CurrentProject.Id);
                _navigation.Navigate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el proyecto", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
