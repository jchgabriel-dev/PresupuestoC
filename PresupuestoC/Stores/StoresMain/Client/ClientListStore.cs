using PresupuestoC.Models.Main;
using PresupuestoC.Services.Client;
using PresupuestoC.Services.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Client
{
    public class ClientListStore
    {
        private readonly List<ClientModel> _clients;
        private readonly Lazy<Task> _initializeLazy;
        private readonly IClientService _clientService;

        public IEnumerable<ClientModel> Clients => _clients;


        public ClientListStore(IClientService clientService)
        {
            _clientService = clientService;
            _initializeLazy = new Lazy<Task>(Initialize);

            _clients = new List<ClientModel>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task CreateClient(ClientModel client)
        {
            await _clientService.Create(client);
            _clients.Add(client);
        }

        public async Task DeleteClient(int id)
        {
            await _clientService.Delete(id);
            var item = _clients.FirstOrDefault(c => c.Id == id);
            _clients.Remove(item);
        }


        public async Task UpdateClient(int id, ClientModel client)
        {
            await _clientService.Update(id, client);
            var item = _clients.FirstOrDefault(c => c.Id == id);
            item.Name = client.Name;
            item.Address = client.Address;
        }


        private async Task Initialize()
        {
            IEnumerable<ClientModel> clients = await _clientService.GetAll();
            _clients.Clear();
            _clients.AddRange(clients);
        }

    }
}
