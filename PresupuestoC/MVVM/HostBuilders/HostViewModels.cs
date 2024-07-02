using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.Navigation.Budget;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Navigation.Currency;
using PresupuestoC.Navigation.Home2;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.Main;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.NavigationBudget.Heriarchy;
using PresupuestoC.Navigation.Project;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Archive;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Folder;
using PresupuestoC.Stores.Location;
using PresupuestoC.Stores.Project;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.Budget;
using PresupuestoC.ViewModels.Client;
using PresupuestoC.ViewModels.Currency;
using PresupuestoC.ViewModels.Folder;
using PresupuestoC.ViewModels.Home2;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.ViewModels.Location.District;
using PresupuestoC.ViewModels.Location.Province;
using PresupuestoC.ViewModels.Location.Region;
using PresupuestoC.ViewModels.Main;
using PresupuestoC.ViewModels.Project;
using PresupuestoC.ViewModels.SubBudget;
using PresupuestoC.ViewModels.ViewModelsBudget.Heriarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.HostBuilders
{
    public static class HostViewModels
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {

                // MAIN
                services.AddTransient<HomeViewModel>();
                services.AddSingleton<Func<HomeViewModel>>((s) => () => s.GetRequiredService<HomeViewModel>());
                services.AddSingleton<NavigationService<HomeViewModel>>();

                services.AddTransient((s) => createBudgetViewModel(s));
                services.AddSingleton<Func<BudgetViewModel>>((s) => () => s.GetRequiredService<BudgetViewModel>());
                services.AddSingleton<NavigationService<BudgetViewModel>>();

                
                // HOME
                services.AddTransient((s) => createProjectViewModel(s));
                services.AddSingleton<Func<ProjectViewModel>>((s) => () => s.GetRequiredService<ProjectViewModel>());
                services.AddSingleton<HomeNavigationService<ProjectViewModel>>();

                services.AddTransient((s) => createArchivesViewModel(s));
                services.AddSingleton<Func<ArchivesViewModel>>((s) => () => s.GetRequiredService<ArchivesViewModel>());
                services.AddSingleton<HomeNavigationService<ArchivesViewModel>>();

                services.AddTransient<SettingsViewModel>();
                services.AddSingleton<Func<SettingsViewModel>>((s) => () => s.GetRequiredService<SettingsViewModel>());
                services.AddSingleton<HomeNavigationService<SettingsViewModel>>();


                // BUDGET
                services.AddTransient<HierarchyViewModel>();
                services.AddSingleton<Func<HierarchyViewModel>>((s) => () => s.GetRequiredService<HierarchyViewModel>());
                services.AddSingleton<BudgetNavigationService<HierarchyViewModel>>();

                services.AddTransient<PolyViewModel>();
                services.AddSingleton<Func<PolyViewModel>>((s) => () => s.GetRequiredService<PolyViewModel>());
                services.AddSingleton<BudgetNavigationService<PolyViewModel>>();

                services.AddTransient<ProgramViewModel>();
                services.AddSingleton<Func<ProgramViewModel>>((s) => () => s.GetRequiredService<ProgramViewModel>());
                services.AddSingleton<BudgetNavigationService<ProgramViewModel>>();


                // PROJECT
                services.AddTransient((s) => createProjectListViewModel(s));
                services.AddSingleton<Func<ProjectListViewModel>>((s) => () => s.GetRequiredService<ProjectListViewModel>());
                services.AddSingleton<ProjectNavigationService<ProjectListViewModel>>();

                services.AddTransient<ProjectCreateViewModel>();
                services.AddSingleton<Func<ProjectCreateViewModel>>((s) => () => s.GetRequiredService<ProjectCreateViewModel>());
                services.AddSingleton<ProjectNavigationService<ProjectCreateViewModel>>();

                services.AddTransient<ProjectEditViewModel>();
                services.AddSingleton<Func<ProjectEditViewModel>>((s) => () => s.GetRequiredService<ProjectEditViewModel>());
                services.AddSingleton<ProjectNavigationService<ProjectEditViewModel>>();

                services.AddTransient<ProjectDeleteViewModel>();
                services.AddSingleton<Func<ProjectDeleteViewModel>>((s) => () => s.GetRequiredService<ProjectDeleteViewModel>());
                services.AddSingleton<ProjectNavigationService<ProjectDeleteViewModel>>();

                // CURRENCY

                services.AddTransient((s) => createCurrencyListViewModel(s));
                services.AddSingleton<Func<CurrencyListViewModel>>((s) => () => s.GetRequiredService<CurrencyListViewModel>());
                services.AddSingleton<CurrencyNavigationService<CurrencyListViewModel>>();

                services.AddTransient<CurrencyCreateViewModel>();
                services.AddSingleton<Func<CurrencyCreateViewModel>>((s) => () => s.GetRequiredService<CurrencyCreateViewModel>());
                services.AddSingleton<CurrencyNavigationService<CurrencyCreateViewModel>>();

                services.AddTransient<CurrencyEditViewModel>();
                services.AddSingleton<Func<CurrencyEditViewModel>>((s) => () => s.GetRequiredService<CurrencyEditViewModel>());
                services.AddSingleton<CurrencyNavigationService<CurrencyEditViewModel>>();

                services.AddTransient<CurrencyDeleteViewModel>();
                services.AddSingleton<Func<CurrencyDeleteViewModel>>((s) => () => s.GetRequiredService<CurrencyDeleteViewModel>());
                services.AddSingleton<CurrencyNavigationService<CurrencyDeleteViewModel>>();

                // CLIENT

                services.AddTransient((s) => createClientListViewModel(s));
                services.AddSingleton<Func<ClientListViewModel>>((s) => () => s.GetRequiredService<ClientListViewModel>());
                services.AddSingleton<ClientNavigationService<ClientListViewModel>>();

                services.AddTransient<ClientCreateViewModel>();
                services.AddSingleton<Func<ClientCreateViewModel>>((s) => () => s.GetRequiredService<ClientCreateViewModel>());
                services.AddSingleton<ClientNavigationService<ClientCreateViewModel>>();

                services.AddTransient<ClientEditViewModel>();
                services.AddSingleton<Func<ClientEditViewModel>>((s) => () => s.GetRequiredService<ClientEditViewModel>());
                services.AddSingleton<ClientNavigationService<ClientEditViewModel>>();

                services.AddTransient<ClientDeleteViewModel>();
                services.AddSingleton<Func<ClientDeleteViewModel>>((s) => () => s.GetRequiredService<ClientDeleteViewModel>());
                services.AddSingleton<ClientNavigationService<ClientDeleteViewModel>>();


                // LOCATION 

                services.AddTransient((s) => createLocationListViewModel(s));
                services.AddSingleton<Func<LocationListViewModel>>((s) => () => s.GetRequiredService<LocationListViewModel>());
                services.AddSingleton<LocationNavigationService<LocationListViewModel>>();

                services.AddTransient((s) => createLocationRegisterViewModel(s));
                services.AddSingleton<Func<LocationRegisterViewModel>>((s) => () => s.GetRequiredService<LocationRegisterViewModel>());
                services.AddSingleton<LocationNavigationService<LocationRegisterViewModel>>();


                // SUPER MODAL LOCATION

                services.AddTransient((s) => createLocationCreateDistrictViewModel(s));
                services.AddSingleton<Func<LocationCreateDistrictViewModel>>((s) => () => s.GetRequiredService<LocationCreateDistrictViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationCreateDistrictViewModel>>();

                services.AddTransient((s) => createLocationEditDistrictViewModel(s));
                services.AddSingleton<Func<LocationEditDistrictViewModel>>((s) => () => s.GetRequiredService<LocationEditDistrictViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationEditDistrictViewModel>>();

                services.AddTransient<LocationDeleteDistrictViewModel>();
                services.AddSingleton<Func<LocationDeleteDistrictViewModel>>((s) => () => s.GetRequiredService<LocationDeleteDistrictViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationDeleteDistrictViewModel>>();


                services.AddTransient((s) => createLocationCreateProvinceViewModel(s));
                services.AddSingleton<Func<LocationCreateProvinceViewModel>>((s) => () => s.GetRequiredService<LocationCreateProvinceViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationCreateProvinceViewModel>>();

                services.AddTransient((s) => createLocationEditProvinceViewModel(s));
                services.AddSingleton<Func<LocationEditProvinceViewModel>>((s) => () => s.GetRequiredService<LocationEditProvinceViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationEditProvinceViewModel>>();

                services.AddTransient<LocationDeleteProvinceViewModel>();
                services.AddSingleton<Func<LocationDeleteProvinceViewModel>>((s) => () => s.GetRequiredService<LocationDeleteProvinceViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationDeleteProvinceViewModel>>();


                services.AddTransient<LocationCreateRegionViewModel>();
                services.AddSingleton<Func<LocationCreateRegionViewModel>>((s) => () => s.GetRequiredService<LocationCreateRegionViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationCreateRegionViewModel>>();

                services.AddTransient<LocationEditRegionViewModel>();
                services.AddSingleton<Func<LocationEditRegionViewModel>>((s) => () => s.GetRequiredService<LocationEditRegionViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationEditRegionViewModel>>();

                services.AddTransient<LocationDeleteRegionViewModel>();
                services.AddSingleton<Func<LocationDeleteRegionViewModel>>((s) => () => s.GetRequiredService<LocationDeleteRegionViewModel>());
                services.AddSingleton<SuperModalNavigationService<LocationDeleteRegionViewModel>>();


                // MODALS
                services.AddTransient<ClientViewModel>();
                services.AddSingleton<Func<ClientViewModel>>((s) => () => s.GetRequiredService<ClientViewModel>());
                services.AddSingleton<ModalNavigationService<ClientViewModel>>();

                services.AddTransient<LocationViewModel>();
                services.AddSingleton<Func<LocationViewModel>>((s) => () => s.GetRequiredService<LocationViewModel>());
                services.AddSingleton<ModalNavigationService<LocationViewModel>>();

                services.AddTransient<CurrencyViewModel>();
                services.AddSingleton<Func<CurrencyViewModel>>((s) => () => s.GetRequiredService<CurrencyViewModel>());
                services.AddSingleton<ModalNavigationService<CurrencyViewModel>>();

                services.AddSingleton<CloseNavigationService>();
                services.AddSingleton<SuperCloseNavigationService>();


                // FOLDER
                services.AddTransient<FolderCreateViewModel>();
                services.AddSingleton<Func<FolderCreateViewModel>>((s) => () => s.GetRequiredService<FolderCreateViewModel>());
                services.AddSingleton<ModalNavigationService<FolderCreateViewModel>>();

                services.AddTransient<FolderEditViewModel>();
                services.AddSingleton<Func<FolderEditViewModel>>((s) => () => s.GetRequiredService<FolderEditViewModel>());
                services.AddSingleton<ModalNavigationService<FolderEditViewModel>>();

                services.AddTransient<FolderDeleteViewModel>();
                services.AddSingleton<Func<FolderDeleteViewModel>>((s) => () => s.GetRequiredService<FolderDeleteViewModel>());
                services.AddSingleton<ModalNavigationService<FolderDeleteViewModel>>();


                // SUBBUDGETS
                services.AddTransient<SubBudgetCreateViewModel>();
                services.AddSingleton<Func<SubBudgetCreateViewModel>>((s) => () => s.GetRequiredService<SubBudgetCreateViewModel>());
                services.AddSingleton<ModalNavigationService<SubBudgetCreateViewModel>>();

                services.AddTransient<SubBudgetEditViewModel>();
                services.AddSingleton<Func<SubBudgetEditViewModel>>((s) => () => s.GetRequiredService<SubBudgetEditViewModel>());
                services.AddSingleton<ModalNavigationService<SubBudgetEditViewModel>>();

                services.AddTransient<SubBudgetDeleteViewModel>();
                services.AddSingleton<Func<SubBudgetDeleteViewModel>>((s) => () => s.GetRequiredService<SubBudgetDeleteViewModel>());
                services.AddSingleton<ModalNavigationService<SubBudgetDeleteViewModel>>();

                // HERIARCHY

                services.AddTransient<LevelViewModel>();
                services.AddSingleton<Func<LevelViewModel>>((s) => () => s.GetRequiredService<LevelViewModel>());
                services.AddSingleton<HeriarchyNavigationService<LevelViewModel>>();

                services.AddTransient<TitleViewModel>();
                services.AddSingleton<Func<TitleViewModel>>((s) => () => s.GetRequiredService<TitleViewModel>());
                services.AddSingleton<HeriarchyNavigationService<TitleViewModel>>();

                services.AddTransient<CertificateViewModel>();
                services.AddSingleton<Func<CertificateViewModel>>((s) => () => s.GetRequiredService<CertificateViewModel>());
                services.AddSingleton<HeriarchyNavigationService<CertificateViewModel>>();


            });

            return hostBuilder;
        }

        private static BudgetViewModel createBudgetViewModel(IServiceProvider s)
        {
            return BudgetViewModel.LoadViewModel(s.GetRequiredService<BudgetNavigationStore>(),
                s.GetRequiredService<NavigationService<HomeViewModel>>(),
                s.GetRequiredService<BudgetNavigationService<HierarchyViewModel>>(),
                s.GetRequiredService<BudgetNavigationService<PolyViewModel>>(),
                s.GetRequiredService<BudgetNavigationService<ProgramViewModel>>(),
                s.GetRequiredService<ProjectSelectedStore>(),
                s.GetRequiredService<SubBudgetListStore>());
        }

        private static CurrencyListViewModel createCurrencyListViewModel(IServiceProvider s)
        {
            return CurrencyListViewModel.LoadViewModel(s.GetRequiredService<CurrencyListStore>(), 
                s.GetRequiredService<CurrencySelectedStore>(),
                s.GetRequiredService<CloseNavigationService>(),
                s.GetRequiredService<ProjectTemporalStore>());    
        }

        private static ClientListViewModel createClientListViewModel(IServiceProvider s)
        {
            return ClientListViewModel.LoadViewModel(s.GetRequiredService<ClientListStore>(),
                s.GetRequiredService<ClientSelectedStore>(),
                s.GetRequiredService<CloseNavigationService>(),
                s.GetRequiredService<ProjectTemporalStore>());
        }

        private static ArchivesViewModel createArchivesViewModel(IServiceProvider s)
        {
            return ArchivesViewModel.LoadViewModel(s.GetRequiredService<ArchivesListStore>(), 
                s.GetRequiredService<ArchiveSelectedStore>(),                
                s.GetRequiredService<ArchiveTemporalStore>(),
                s.GetRequiredService<FolderListStore>(),
                s.GetRequiredService<ProjectListStore>(),
                s.GetRequiredService<SubBudgetListStore>());        
        }


        private static ProjectListViewModel createProjectListViewModel(IServiceProvider s)
        {
            return ProjectListViewModel.LoadViewModel(s.GetRequiredService<ProjectListStore>(),
                s.GetRequiredService<ProjectSelectedStore>(),
                s.GetRequiredService<NavigationService<BudgetViewModel>>(),
                s.GetRequiredService<FolderSelectedStore>(),
                s.GetRequiredService<SubBudgetListStore>());            
        }



        // FOLDER
        private static ProjectViewModel createProjectViewModel(IServiceProvider s)
        {
            return ProjectViewModel.LoadViewModel(s.GetRequiredService<ProjectNavigationStore>(),
                s.GetRequiredService<ProjectNavigationService<ProjectListViewModel>>(),
                s.GetRequiredService<ProjectNavigationService<ProjectCreateViewModel>>(),
                s.GetRequiredService<ProjectNavigationService<ProjectEditViewModel>>(),
                s.GetRequiredService<ProjectNavigationService<ProjectDeleteViewModel>>(),

                s.GetRequiredService<ModalNavigationService<FolderCreateViewModel>>(),
                s.GetRequiredService<ModalNavigationService<FolderEditViewModel>>(),
                s.GetRequiredService<ModalNavigationService<FolderDeleteViewModel>>(),

                s.GetRequiredService<ModalNavigationService<SubBudgetCreateViewModel>>(),
                s.GetRequiredService<ModalNavigationService<SubBudgetEditViewModel>>(),
                s.GetRequiredService<ModalNavigationService<SubBudgetDeleteViewModel>>(),

                s.GetRequiredService<FolderListStore>(),
                s.GetRequiredService<FolderSelectedStore>(),
                s.GetRequiredService<SubBudgetListStore>(),
                s.GetRequiredService<SubBudgetSelectedStore>(),
                s.GetRequiredService<ProjectSelectedStore>()
            );
        }



        // LOCATION
        private static LocationListViewModel createLocationListViewModel(IServiceProvider s)
        {
            return LocationListViewModel.LoadViewModel(s.GetRequiredService<LocationListStore>(),               
                s.GetRequiredService<CloseNavigationService>(),
                s.GetRequiredService<LocationSelectedStore>(),
                s.GetRequiredService<ProjectTemporalStore>());
        }

        private static LocationRegisterViewModel createLocationRegisterViewModel(IServiceProvider s)
        {
            return LocationRegisterViewModel.LoadViewModel(
                s.GetRequiredService<LocationNavigationService<LocationListViewModel>>(),
                s.GetRequiredService<LocationListStore>(),
                s.GetRequiredService<RegionSelectedStore>(),
                s.GetRequiredService<ProvinceSelectedStore>(),
                s.GetRequiredService<DistrictSelectedStore>(),
                s.GetRequiredService<SuperModalNavigationService<LocationCreateRegionViewModel>>(),
                s.GetRequiredService<SuperModalNavigationService<LocationEditRegionViewModel>>(),
                s.GetRequiredService<SuperModalNavigationService<LocationDeleteRegionViewModel>>(),
                s.GetRequiredService<SuperModalNavigationService<LocationCreateProvinceViewModel>>(),
                s.GetRequiredService<SuperModalNavigationService<LocationEditProvinceViewModel>>(),
                s.GetRequiredService<SuperModalNavigationService<LocationDeleteProvinceViewModel>>(),
                s.GetRequiredService<SuperModalNavigationService<LocationCreateDistrictViewModel>>(),
                s.GetRequiredService<SuperModalNavigationService<LocationEditDistrictViewModel>>(),
                s.GetRequiredService<SuperModalNavigationService<LocationDeleteDistrictViewModel>>());
        }
      
        // PROVINCE

        public static LocationCreateProvinceViewModel createLocationCreateProvinceViewModel(IServiceProvider s)
        {
            return LocationCreateProvinceViewModel.LoadViewModel(s.GetRequiredService<SuperCloseNavigationService>(),
                s.GetRequiredService<LocationListStore>());
        }

        public static LocationEditProvinceViewModel createLocationEditProvinceViewModel(IServiceProvider s)
        {
            return LocationEditProvinceViewModel.LoadViewModel(s.GetRequiredService<SuperCloseNavigationService>(),
                s.GetRequiredService<LocationListStore>(), s.GetRequiredService<ProvinceSelectedStore>());
        }
    

        // DISTRICT

        public static LocationCreateDistrictViewModel createLocationCreateDistrictViewModel(IServiceProvider s)
        {
            return LocationCreateDistrictViewModel.LoadViewModel(s.GetRequiredService<SuperCloseNavigationService>(),
                s.GetRequiredService<LocationListStore>());
        }
        

        public static LocationEditDistrictViewModel createLocationEditDistrictViewModel(IServiceProvider s)
        {
            return LocationEditDistrictViewModel.LoadViewModel(s.GetRequiredService<SuperCloseNavigationService>(),
                s.GetRequiredService<LocationListStore>(), s.GetRequiredService<DistrictSelectedStore>());
        }
    
    }



}
