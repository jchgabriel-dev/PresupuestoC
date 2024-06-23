using PresupuestoC.Models.Main;
using PresupuestoC.Services.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores
{
    public class CurrencyListStore
    {
        private readonly List<CurrencyModel> _currencies;
        private readonly Lazy<Task> _initializeLazy;
        private readonly ICurrencyService _currencyService;

        public IEnumerable<CurrencyModel> Currencies => _currencies;

        public CurrencyListStore(ICurrencyService currencyService) 
        {
            _currencyService = currencyService;
            _initializeLazy = new Lazy<Task>(Initialize);

            _currencies = new List<CurrencyModel>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }
        
        public async Task CreateCurrency(CurrencyModel currency)
        {
            await _currencyService.Create(currency);
            _currencies.Add(currency);
        }

        public async Task DeleteCurrency(int id)
        {
            await _currencyService.Delete(id);
            var item = _currencies.FirstOrDefault(c => c.Id == id);
            _currencies.Remove(item);
        }


        public async Task UpdateCurrency(int id, CurrencyModel currency)
        {
            await _currencyService.Update(id, currency);
            var item = _currencies.FirstOrDefault(c => c.Id == id);
            item.Symbol = currency.Symbol;
            item.Description = currency.Description;
        }


        private async Task Initialize()
        {
            IEnumerable<CurrencyModel> currencies = await _currencyService.GetAll();
            _currencies.Clear();
            _currencies.AddRange(currencies);
        }

    }
}
