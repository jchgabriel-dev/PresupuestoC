using PresupuestoC.Models;
using PresupuestoC.Services.Location;
using PresupuestoC.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Location
{
    public class LocationListStore
    {
        private readonly List<RegionModel> _regions;
        private readonly List<ProvinceModel> _provinces;
        private readonly List<DistrictModel> _districts;

        private readonly Lazy<Task> _initializeLazy;
        private readonly ILocationService _locationService;

        public IEnumerable<RegionModel> Regions => _regions;
        public IEnumerable<ProvinceModel> Provinces => _provinces;
        public IEnumerable<DistrictModel> Districts => _districts;

        public Action Changes;


        public LocationListStore(ILocationService regionService)
        {
            _locationService = regionService;
            _initializeLazy = new Lazy<Task>(Initialize);
            _regions = new List<RegionModel>();
            _provinces = new List<ProvinceModel>();
            _districts = new List<DistrictModel>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        // REGION

        public async Task CreateRegion(RegionModel region)
        {
            RegionModel regionCreated = await _locationService.CreateRegion(region);
            _regions.Add(regionCreated);
            Changes?.Invoke();
        }

        public async Task DeleteRegion(int id)
        {
            await _locationService.DeleteRegion(id);
            var item = _regions.FirstOrDefault(c => c.Id == id);
            _regions.Remove(item);
            Changes?.Invoke();
        }


        public async Task UpdateRegion(int id, RegionModel region)
        {
            await _locationService.UpdateRegion(id, region);
            var item = _regions.FirstOrDefault(c => c.Id == id);
            item.Name = region.Name;
            Changes?.Invoke();
        }

        // PROVINCE 
        public async Task CreateProvince(ProvinceModel province, RegionModel region)
        {        
            ProvinceModel itemCreated = await _locationService.CreateProvince(province);
            itemCreated.Region = region;
            _provinces.Add(itemCreated);
            Changes?.Invoke();

        }

        public async Task DeleteProvince(int id)
        {
            await _locationService.DeleteProvince(id);
            var item = _provinces.FirstOrDefault(c => c.Id == id);
            _provinces.Remove(item);
            Changes?.Invoke();

        }


        public async Task UpdateProvince(int id, ProvinceModel province, RegionModel region)
        {           
            await _locationService.UpdateProvince(id, province);

            var editItem = _provinces.FirstOrDefault(c => c.Id == id);
            editItem.Name = province.Name;
            editItem.RegionId = province.RegionId;
            editItem.Region = region;
            Changes?.Invoke();

        }


        // DISTRICT
        public async Task CreateDistrict(DistrictModel district, ProvinceModel province)
        {                      
            DistrictModel itemCreated = await _locationService.CreateDistrict(district);
            itemCreated.Province = province;
            _districts.Add(itemCreated);
            Changes?.Invoke();

        }

        public async Task DeleteDistrict(int id)
        {
            await _locationService.DeleteDistrict(id);
            var item = _districts.FirstOrDefault(c => c.Id == id);
            _districts.Remove(item);
            Changes?.Invoke();

        }


        public async Task UpdateDistrict(int id, DistrictModel district, ProvinceModel province)
        {      
            await _locationService.UpdateDistrict(id, district);
            var editItem = _districts.FirstOrDefault(c => c.Id == id);
            editItem.Name = district.Name;
            editItem.ProvinceId = district.ProvinceId;
            editItem.Province = province;
            Changes?.Invoke();

        }

        // INITIALIZE

        private async Task Initialize()
        {
            IEnumerable<RegionModel> regions = await _locationService.GetAllRegion();
            _regions.Clear();
            _regions.AddRange(regions);

            IEnumerable<ProvinceModel> provinces = await _locationService.GetAllProvince();
            _provinces.Clear();
            _provinces.AddRange(provinces);

            IEnumerable<DistrictModel> districts = await _locationService.GetAllDistrict();
            _districts.Clear();
            _districts.AddRange(districts);

            
            foreach (var province in _provinces)
            {
                province.Region = _regions.FirstOrDefault(r => r.Id == province.RegionId);
            }

            foreach (var district in _districts)
            {
                district.Province = _provinces.FirstOrDefault(p => p.Id == district.ProvinceId);
            }
        }
    }
}
