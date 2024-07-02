using PresupuestoC.Models.Archive;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Project;
using PresupuestoC.Stores.Project;
using PresupuestoC.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Command.Project
{
    

    public class ProjectEditCommand : AsyncCommand
    {
        private readonly ProjectEditViewModel _viewModel;
        private readonly ProjectNavigationService<ProjectListViewModel> _navigation;
        private readonly ProjectListStore _store;
        private readonly ProjectSelectedStore _selected;



        public ProjectEditCommand(ProjectEditViewModel viewModel, ProjectNavigationService<ProjectListViewModel> navigation, ProjectListStore store, ProjectSelectedStore selected)
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
                _viewModel.ClientChanged();
                _viewModel.LocationChanged();
                _viewModel.CurrencyChanged();

                _viewModel.Name = _viewModel.Name;
                _viewModel.Place = _viewModel.Place;
                _viewModel.Date = _viewModel.Date;
                _viewModel.Work = _viewModel.Work;
                _viewModel.Group = _viewModel.Group;
                _viewModel.IGV = _viewModel.IGV;
                _viewModel.Left = _viewModel.Left;
                _viewModel.Right = _viewModel.Right;

                if (_viewModel.HasErrors)
                {
                    return;
                }


                ProjectModel project = await _store.GetProject(_selected.CurrentProject.Id);
                project.Date = DateOnly.FromDateTime(_viewModel.Date.Value);
                project.Name = _viewModel.Name;
                project.Place = _viewModel.Place;
                project.Group = _viewModel.Group;
                project.Work = Convert.ToInt32(_viewModel.Work);
                project.LetterheadLeft = _viewModel.Left;
                project.LetterheadRight = _viewModel.Right;
                project.IGV = _viewModel.IGV;

                ClientArchiveModel customer = new ClientArchiveModel();
                customer.Name = _viewModel.Client.Name;
                customer.Address = _viewModel.Client.Address;

                CurrencyArchiveModel money = new CurrencyArchiveModel();
                money.Symbol = _viewModel.Currency.Symbol;
                money.Description = _viewModel.Currency.Description;

                LocationArchiveModel location = new LocationArchiveModel();
                location.District = _viewModel.Location.Name;
                location.Province = _viewModel.Location.Province.Name;
                location.Region = _viewModel.Location.Province.Region.Name;                

                await _store.UpdateProject(_selected.CurrentProject.Id, project, customer, money, location);
                _navigation.Navigate();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
