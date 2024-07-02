using PresupuestoC.Models.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.SubBudget
{


    public interface ISubBudgetService
    {
        Task<IEnumerable<SubBudgetModel>> GetAll();
        Task<SubBudgetModel> Get(int id);
        Task<SubBudgetModel> Create(SubBudgetModel entity);
        Task<SubBudgetModel> Update(int id, SubBudgetModel entity);
        Task<bool> Delete(int id);
        Task<int> GetOrder(int id);
        Task<IEnumerable<SubBudgetModel>> GetProjectSubs(int id);

    }
}
