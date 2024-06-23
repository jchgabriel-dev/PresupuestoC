using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.Services.Currency;
using PresupuestoC.Services.Customer;
using PresupuestoC.Services.Money;
using PresupuestoC.Services.Position;
using PresupuestoC.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;

namespace PresupuestoC.Stores.Project
{
    public class ProjectListStore
    {
        private readonly List<ProjectModel> _projects;
        private readonly List<StateModel> _states;

        private readonly Lazy<Task> _initializeLazy;
        private readonly IProjectService _projectService;
        
        private readonly ICustomerService _customerService;
        private readonly IMoneyService _moneyService;
        private readonly IPositionService _positionService;



        public IEnumerable<ProjectModel> Projects => _projects;
        public IEnumerable<StateModel> States => _states;

        public ProjectListStore(IProjectService projectService, ICustomerService customerService, IMoneyService moneyService, IPositionService positionService)
        {
            _projectService = projectService;
            _customerService = customerService;
            _moneyService = moneyService;
            _positionService = positionService;

            _initializeLazy = new Lazy<Task>(Initialize);
            _projects = new List<ProjectModel>();
            _states = new List<StateModel>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task CreateProject(ProjectModel project, CustomerModel customer, MoneyModel money, LocationModel location)
        {
            CustomerModel customerCreated = await _customerService.Create(customer);
            MoneyModel moneyCreated = await _moneyService.Create(money);
            LocationModel locationCreated = await _positionService.Create(location);

            project.CustomerId = customerCreated.Id;
            project.MoneyId = moneyCreated.Id;
            project.LocationId = locationCreated.Id;

            ProjectModel itemCreated = await _projectService.Create(project);
            _projects.Add(itemCreated);
        }

        public async Task DeleteProject(int id)
        {
            await _projectService.Delete(id);
            var item = _projects.FirstOrDefault(c => c.Id == id);
            _projects.Remove(item);
        }


        public async Task UpdateProject(int id, ProjectModel project)
        {
            await _projectService.Update(id, project);
            var item = _projects.FirstOrDefault(c => c.Id == id);
           
        }

        public async Task Reload()
        {
            await Initialize();
        }

        private async Task Initialize()
        {
            IEnumerable<ProjectModel> projects = await _projectService.GetAll();
            _projects.Clear();
            _projects.AddRange(projects);

            IEnumerable<StateModel> states = await _projectService.GetAllStates();
            _states.Clear();
            _states.AddRange(states);
        }

    }
}
