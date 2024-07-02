using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.Services.Client;
using PresupuestoC.Services.SubBudget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.SubBudget
{

    public class SubBudgetListStore
    {
        private readonly List<SubBudgetModel> _subBudgetsAlt;
        public IEnumerable<SubBudgetModel> SubBudgetsAlt => _subBudgetsAlt;


        private readonly List<SubBudgetModel> _subBudgets;               
        private readonly Lazy<Task> _initializeLazy;
        private readonly ISubBudgetService _subBudgetService;

        public IEnumerable<SubBudgetModel> SubBudgets => _subBudgets;
        public Action Changes;

        public SubBudgetListStore(ISubBudgetService clientService)
        {
            _subBudgetService = clientService;
            _initializeLazy = new Lazy<Task>(Initialize);
            _subBudgets = new List<SubBudgetModel>();
            _subBudgetsAlt = new List<SubBudgetModel>();

        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task CreateSubBudget(SubBudgetModel client)
        {           
            await _subBudgetService.Create(client);
            await Reload();
            Changes?.Invoke();
        }

        public async Task DeleteSubBudget(int id)
        {
            await _subBudgetService.Delete(id);
            await Reload();
            Changes?.Invoke();
        }

        public async Task UpdateSubBudget(int id, SubBudgetModel client)
        {
            await _subBudgetService.Update(id, client);
            await Reload();
            Changes?.Invoke();
        }

        public async Task<SubBudgetModel> GetSubBudger(int id)
        {
            return await _subBudgetService.Get(id);
        }

        public async Task<int> GetOrder(int id)
        {
            return await _subBudgetService.GetOrder(id);
        }
        public async Task Reload()
        {
            await Initialize();
        }
       
        private async Task Initialize()
        {
            IEnumerable<SubBudgetModel> subBudgets = await _subBudgetService.GetAll();
            _subBudgets.Clear();
            _subBudgets.AddRange(subBudgets);
        }

        public async Task GetProjectSubs(int id)
        {
            IEnumerable<SubBudgetModel> subBudgets = await _subBudgetService.GetProjectSubs(id);
            _subBudgetsAlt.Clear();
            _subBudgetsAlt.AddRange(subBudgets);
        }

    }
}
