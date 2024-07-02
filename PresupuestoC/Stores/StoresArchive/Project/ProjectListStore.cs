using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.Services.Currency;
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
        

        public IEnumerable<ProjectModel> Projects => _projects;
        public IEnumerable<StateModel> States => _states;

        public ProjectListStore(IProjectService projectService)
        {
            _projectService = projectService;          

            _initializeLazy = new Lazy<Task>(Initialize);
            _projects = new List<ProjectModel>();
            _states = new List<StateModel>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task CreateProject(ProjectModel project, ClientArchiveModel client, CurrencyArchiveModel currency, LocationArchiveModel location)
        {
            ClientArchiveModel clientCreated = await _projectService.CreateClientArchive(client);
            CurrencyArchiveModel currencyCreated = await _projectService.CreateCurrencyArchive(currency);
            LocationArchiveModel locationCreated = await _projectService.CreateLocationArchive(location);

            project.ClientArchiveId = clientCreated.Id;
            project.CurrencyArchiveId = currencyCreated.Id;
            project.LocationArchiveId = locationCreated.Id;

            ProjectModel itemCreated = await _projectService.CreateProject(project);
            await Reload();
        }

        public async Task DeleteProject(int id)
        {
            await _projectService.DeleteProject(id);
            await Reload();

        }


        public async Task UpdateProject(int id, ProjectModel project, ClientArchiveModel client, CurrencyArchiveModel currency, LocationArchiveModel location)
        {
            await _projectService.UpdateClientArchive(project.ClientArchiveId, client);
            await _projectService.UpdateCurrencyArchive(project.CurrencyArchiveId, currency);
            await _projectService.UpdateLocationArchive(project.LocationArchiveId, location);

            await _projectService.UpdateProject(id, project);
            await Reload();           
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            return await _projectService.GetProject(id);
        }


        public async Task Reload()
        {
            await Initialize();
        }

        private async Task Initialize()
        {
            IEnumerable<ProjectModel> projects = await _projectService.GetAllProjects();
            _projects.Clear();
            _projects.AddRange(projects);

            IEnumerable<StateModel> states = await _projectService.GetAllStates();
            _states.Clear();
            _states.AddRange(states);
        }

    }
}
