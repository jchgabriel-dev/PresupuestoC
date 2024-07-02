using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using PresupuestoC.Services.Currency;
using PresupuestoC.Services.Folder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PresupuestoC.Stores.Folder
{
    public class FolderListStore
    {
        private readonly List<FolderModel> _folders;
        private readonly Lazy<Task> _initializeLazy;
        private readonly IFolderService _folderService;

        public IEnumerable<FolderModel> Folders => _folders;
        public Action Changes;

        public FolderListStore(IFolderService currencyFolder)
        {
            _folderService = currencyFolder;
            _initializeLazy = new Lazy<Task>(Initialize);
            _folders = new List<FolderModel>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task CreateFolder(FolderModel folder, FolderModel selected)
        {
            bool created = await _folderService.Check(folder);

            if (created)
            {
                throw new InvalidOperationException();
            }


            FolderModel itemCreated = await _folderService.Create(folder);    
            selected.Children.Add(itemCreated);
            itemCreated.Parent = selected;
            selected.Expanded = true;       

            _folders.Add(itemCreated);
            Changes?.Invoke();
        }

        public async Task DeleteFolder(int id)
        {
            await _folderService.Delete(id);
            FolderModel item = _folders.FirstOrDefault(c => c.Id == id);
            item.Parent.Selected = true;

            item.Parent.Children.Remove(item);   
            _folders.Remove(item);           
            Changes?.Invoke();
        }


        public async Task UpdateFolder(int id, FolderModel folder)
        {
            bool created = await _folderService.Check(folder);

            if (created)
            {
                throw new InvalidOperationException();
            }

            await _folderService.Update(id, folder);
            var item = _folders.FirstOrDefault(c => c.Id == id);
            item.Name = folder.Name;
            Changes?.Invoke();

        }
     
        public async Task<FolderModel> GetFolder(int id)
        {
            return await _folderService.Get(id);
        }

        public async Task Reload()
        {
            await Initialize();           
        }


        private async Task Initialize()
        {
            
            IEnumerable<FolderModel> folders = await _folderService.GetAll();
            _folders.Clear();
            _folders.AddRange(folders);


        }

    }
}
