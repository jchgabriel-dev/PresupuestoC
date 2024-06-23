using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Client
{
    public interface IClientService
    {
        Task<IEnumerable<ClientModel>> GetAll();
        Task<ClientModel> Get(int id);
        Task<ClientModel> Create(ClientModel entity);
        Task<ClientModel> Update(int id, ClientModel entity);
        Task<bool> Delete(int id);

    }
}
