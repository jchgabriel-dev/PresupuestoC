using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Navigation.Project;
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
    public class ProjectCreateCommand : AsyncCommand
    {
        private readonly ProjectCreateViewModel _viewModel;
        private readonly ProjectNavigationService<ProjectListViewModel> _navigation;
        private readonly ProjectListStore _store;
        
       

        public ProjectCreateCommand(ProjectCreateViewModel viewModel, ProjectNavigationService<ProjectListViewModel> navigation, ProjectListStore store)
        {

            _viewModel = viewModel;
            _navigation = navigation;
            _store = store;
        }


        public async override Task ExecuteAsync(object parameter)
        {            
            try
            {
                _viewModel.Folder = _viewModel.Folder;
                _viewModel.Name = _viewModel.Name;
                _viewModel.Client = _viewModel.Client;
                _viewModel.Location = _viewModel.Location;
                _viewModel.Place = _viewModel.Place;
                _viewModel.Date = _viewModel.Date;
                _viewModel.Work = _viewModel.Work;
                _viewModel.Group = _viewModel.Group;
                _viewModel.Currency = _viewModel.Currency;
                _viewModel.IGV = _viewModel.IGV;
                _viewModel.Left = _viewModel.Left;
                _viewModel.Right = _viewModel.Right;

                if (_viewModel.HasErrors)
                {
                    return;
                }

               

                CustomerModel customer = new CustomerModel();
                customer.Name = _viewModel.Client.Name;
                customer.Address = _viewModel.Client.Address;

                MoneyModel money = new MoneyModel();
                money.Symbol = _viewModel.Currency.Symbol;
                money.Description = _viewModel.Currency.Description;

                LocationModel location = new LocationModel();
                location.District = _viewModel.Location.Name;
                location.Province = _viewModel.Location.Province.Name;
                location.Region = _viewModel.Location.Province.Region.Name;

                ProjectModel project = new ProjectModel();
                project.FolderId = _viewModel.Folder.Id;
                project.Date = DateOnly.FromDateTime(_viewModel.Date.Value);
                project.Name = _viewModel.Name;
                project.Place = _viewModel.Place;
                project.Group = _viewModel.Group;
                project.Work = Convert.ToInt32(_viewModel.Work);
                project.LetterheadLeft = _viewModel.Left;
                project.LetterheadRight = _viewModel.Right;
                project.IGV = _viewModel.IGV;
                project.StateId = 1;

                await _store.CreateProject(project, customer, money, location);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
