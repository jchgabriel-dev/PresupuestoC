using PresupuestoC.Models.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Money
{
    public interface IMoneyService
    {
        Task<IEnumerable<MoneyModel>> GetAll();
        Task<MoneyModel> Get(int id);
        Task<MoneyModel> Create(MoneyModel entity);
        Task<MoneyModel> Update(int id, MoneyModel entity);
        Task<bool> Delete(int id);

    }
}
