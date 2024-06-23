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
        private readonly List<SubBudgetModel> _subBudgets;
        private readonly Lazy<Task> _initializeLazy;
        private readonly ISubBudgetService _subBudgetService;

        public IEnumerable<SubBudgetModel> SubBudgets => _subBudgets;


        public SubBudgetListStore(ISubBudgetService clientService)
        {
            _subBudgetService = clientService;
            _initializeLazy = new Lazy<Task>(Initialize);
            _subBudgets = new List<SubBudgetModel>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task CreateSubBudget(SubBudgetModel client)
        {
            await _subBudgetService.Create(client);
            _subBudgets.Add(client);
        }

        public async Task DeleteSubBudget(int id)
        {
            await _subBudgetService.Delete(id);
            var item = _subBudgets.FirstOrDefault(c => c.Id == id);
            _subBudgets.Remove(item);
        }


        public async Task UpdateSubBudget(int id, SubBudgetModel client)
        {
            await _subBudgetService.Update(id, client);
            var item = _subBudgets.FirstOrDefault(c => c.Id == id);
     
        }


        private async Task Initialize()
        {
            IEnumerable<SubBudgetModel> subBudgets = await _subBudgetService.GetAll();
            _subBudgets.Clear();
            _subBudgets.AddRange(subBudgets);
        }

    }
}
