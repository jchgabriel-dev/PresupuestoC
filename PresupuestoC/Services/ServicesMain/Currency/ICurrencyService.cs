using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Currency
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyModel>> GetAll();
        Task<CurrencyModel> Get(int id);
        Task<CurrencyModel> Create(CurrencyModel entity);
        Task<CurrencyModel> Update(int id, CurrencyModel entity);
        Task<bool> Delete(int id);

    }
}
