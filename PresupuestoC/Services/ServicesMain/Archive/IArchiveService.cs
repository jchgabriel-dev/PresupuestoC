using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Archive
{
    public interface IArchiveService
    {
        Task<IEnumerable<ArchiveModel>> GetAll();
        Task<ArchiveModel> Get(int id);
        Task<ArchiveModel> GetSelected();
        Task<ArchiveModel> Create(ArchiveModel entity);
        Task<ArchiveModel> Update(int id, ArchiveModel entity);
        Task<bool> Delete(int id);

        Task<bool> Check(ArchiveModel entity);


    }
}
