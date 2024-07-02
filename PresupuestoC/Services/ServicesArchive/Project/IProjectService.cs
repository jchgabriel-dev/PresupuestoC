using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Project
{


    public interface IProjectService
    {
        Task<IEnumerable<ProjectModel>> GetAllProjects();
        Task<ProjectModel> GetProject(int id);
        Task<ProjectModel> CreateProject(ProjectModel entity);
        Task<ProjectModel> UpdateProject(int id, ProjectModel entity);
        Task<bool> DeleteProject(int id);


        Task<IEnumerable<StateModel>> GetAllStates();


        Task<CurrencyArchiveModel> GetCurrencyArchive(int id);
        Task<CurrencyArchiveModel> CreateCurrencyArchive(CurrencyArchiveModel entity);
        Task<CurrencyArchiveModel> UpdateCurrencyArchive(int id, CurrencyArchiveModel entity);
        Task<bool> DeleteCurrencyArchive(int id);


        Task<LocationArchiveModel> GetLocationArchive(int id);
        Task<LocationArchiveModel> CreateLocationArchive(LocationArchiveModel entity);
        Task<LocationArchiveModel> UpdateLocationArchive(int id, LocationArchiveModel entity);
        Task<bool> DeleteLocationArchive(int id);


        Task<ClientArchiveModel> GetClientArchive(int id);
        Task<ClientArchiveModel> CreateClientArchive(ClientArchiveModel entity);
        Task<ClientArchiveModel> UpdateClientArchive(int id, ClientArchiveModel entity);
        Task<bool> DeleteClientArchive(int id);

    }
}
