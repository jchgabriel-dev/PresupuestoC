using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Customer
{

    public interface ICustomerService
    {
        Task<IEnumerable<CustomerModel>> GetAll();
        Task<CustomerModel> Get(int id);
        Task<CustomerModel> Create(CustomerModel entity);
        Task<CustomerModel> Update(int id, CustomerModel entity);
        Task<bool> Delete(int id);

    }
}
