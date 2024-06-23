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
        Task<IEnumerable<ProjectModel>> GetAll();
        Task<IEnumerable<StateModel>> GetAllStates();
        Task<ProjectModel> Get(int id);
        Task<ProjectModel> Create(ProjectModel entity);
        Task<ProjectModel> Update(int id, ProjectModel entity);
        Task<bool> Delete(int id);

    }
}
