using PresupuestoC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Services.Location
{
    public interface ILocationService
    {
        Task<IEnumerable<RegionModel>> GetAllRegion();
        Task<IEnumerable<ProvinceModel>> GetAllProvince();
        Task<IEnumerable<DistrictModel>> GetAllDistrict();

        Task<RegionModel> GetRegion(int id);
        Task<ProvinceModel> GetProvince(int id);
        Task<DistrictModel> GetDistrict(int id);

        Task<RegionModel> CreateRegion(RegionModel entity);
        Task<ProvinceModel> CreateProvince(ProvinceModel entity);
        Task<DistrictModel> CreateDistrict(DistrictModel entity);

        Task<RegionModel> UpdateRegion(int id, RegionModel entity);
        Task<ProvinceModel> UpdateProvince(int id, ProvinceModel entity);
        Task<DistrictModel> UpdateDistrict(int id, DistrictModel entity);

        Task<bool> DeleteRegion(int id);
        Task<bool> DeleteProvince(int id);
        Task<bool> DeleteDistrict(int id);


    }
}
