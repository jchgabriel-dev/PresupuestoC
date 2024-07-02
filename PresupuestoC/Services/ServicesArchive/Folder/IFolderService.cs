using PresupuestoC.Models.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Folder
{
    public interface IFolderService
    {
        Task<IEnumerable<FolderModel>> GetAll();
        Task<FolderModel> Get(int id);
        Task<FolderModel> Create(FolderModel entity);
        Task<FolderModel> Update(int id, FolderModel entity);
        Task<bool> Delete(int id);

        Task<bool> Check(FolderModel entity);

    }
}
