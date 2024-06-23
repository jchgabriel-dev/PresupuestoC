using PresupuestoC.Models.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Position
{
    public interface IPositionService
    {
        Task<IEnumerable<LocationModel>> GetAll();
        Task<LocationModel> Get(int id);
        Task<LocationModel> Create(LocationModel entity);
        Task<LocationModel> Update(int id, LocationModel entity);
        Task<bool> Delete(int id);

    }
}
