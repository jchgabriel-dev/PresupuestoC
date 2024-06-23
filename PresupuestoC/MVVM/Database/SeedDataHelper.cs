using PresupuestoC.Models;
using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PresupuestoC.MVVM.Database
{
    public static class SeedDataHelper
    {
      
        public static List<RegionModel> GetRegionsFromJson()
        {
            string resourceName = "PresupuestoC.MVVM.Seeds.RegionSeed.json";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Recurso incrustado '{resourceName}' no encontrado.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<RegionModel> regions = JsonSerializer.Deserialize<List<RegionModel>>(json);
                    return regions;
                }
            }
        }

        public static List<ProvinceModel> GetProvincesFromJson()
        {
            string resourceName = "PresupuestoC.MVVM.Seeds.ProvinceSeed.json";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Recurso incrustado '{resourceName}' no encontrado.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<ProvinceModel> provinces = JsonSerializer.Deserialize<List<ProvinceModel>>(json);
                    return provinces;
                }
            }
        }

        public static List<DistrictModel> GetDistrictsFromJson()
        {
            string resourceName = "PresupuestoC.MVVM.Seeds.DistrictSeed.json";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Recurso incrustado '{resourceName}' no encontrado.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<DistrictModel> districts = JsonSerializer.Deserialize<List<DistrictModel>>(json);
                    return districts;
                }
            }
        }

        public static List<StateModel> GetStatesFromJson()
        {
            string resourceName = "PresupuestoC.MVVM.Seeds.StateSeed.json";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Recurso incrustado '{resourceName}' no encontrado.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<StateModel> states = JsonSerializer.Deserialize<List<StateModel>>(json);
                    return states;
                }
            }
        }

        public static List<CurrencyModel> GetCurrenciesFromJson()
        {
            string resourceName = "PresupuestoC.MVVM.Seeds.CurrencySeed.json";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Recurso incrustado '{resourceName}' no encontrado.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<CurrencyModel> currencies = JsonSerializer.Deserialize<List<CurrencyModel>>(json);
                    return currencies;
                }
            }
        }

        public static List<ClientModel> GetClientsFromJson()
        {
            string resourceName = "PresupuestoC.MVVM.Seeds.ClientSeed.json";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Recurso incrustado '{resourceName}' no encontrado.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<ClientModel> clients = JsonSerializer.Deserialize<List<ClientModel>>(json);
                    return clients;
                }
            }
        }


        public static List<FolderModel> GetFoldersFromJson()
        {
            string resourceName = "PresupuestoC.MVVM.Seeds.FolderSeed.json";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Recurso incrustado '{resourceName}' no encontrado.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<FolderModel> folders = JsonSerializer.Deserialize<List<FolderModel>>(json);
                    return folders;
                }
            }
        }



    }
}
