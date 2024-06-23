using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresupuestoC.Command.Archive;
using PresupuestoC.Database;
using PresupuestoC.HostBuilders;
using PresupuestoC.Models;
using PresupuestoC.MVVM.Archive;
using PresupuestoC.MVVM.Database;
using PresupuestoC.Navigation.Budget;
using PresupuestoC.Navigation.Client;
using PresupuestoC.Navigation.Currency;

using PresupuestoC.Navigation.Home2;
using PresupuestoC.Navigation.Location;
using PresupuestoC.Navigation.Main;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.Project;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Services.Archive;
using PresupuestoC.Services.Client;
using PresupuestoC.Services.Currency;
using PresupuestoC.Services.Customer;
using PresupuestoC.Services.Folder;
using PresupuestoC.Services.Location;
using PresupuestoC.Services.Money;
using PresupuestoC.Services.Position;
using PresupuestoC.Services.Project;
using PresupuestoC.Services.SubBudget;
using PresupuestoC.Stores;
using PresupuestoC.Stores.Archive;
using PresupuestoC.Stores.Client;
using PresupuestoC.Stores.Currency;
using PresupuestoC.Stores.Folder;
using PresupuestoC.Stores.Location;
using PresupuestoC.Stores.Project;
using PresupuestoC.Stores.SubBudget;
using PresupuestoC.ViewModels.Client;
using PresupuestoC.ViewModels.Currency;
using PresupuestoC.ViewModels.Location;
using PresupuestoC.ViewModels.Main;

namespace PresupuestoC
{

    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source= presupuestos.prs";
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IDatabaseContextFactory>(new DatabaseContextFactory(CONNECTION_STRING));
                    services.AddSingleton<ICurrencyService, CurrencyService>();
                    services.AddSingleton<IClientService, ClientService>();
                    services.AddSingleton<ILocationService, LocationService>();
                    services.AddSingleton<IArchiveService, ArchiveService>();

                    services.AddSingleton<IArchiveContextFactory, ArchiveContextFactory>();
                    services.AddSingleton<IFolderService, FolderService>();
                    services.AddSingleton<IProjectService, ProjectService>();
                    services.AddSingleton<ICustomerService, CustomerService>();
                    services.AddSingleton<IMoneyService, MoneyService>();
                    services.AddSingleton<IPositionService, PositionService>();
                    services.AddSingleton<ISubBudgetService, SubBudgetService>();



                    services.AddSingleton<CurrencySelectedStore>();
                    services.AddSingleton<CurrencyListStore>();
                    services.AddSingleton<ClientSelectedStore>();
                    services.AddSingleton<ClientListStore>();
                    services.AddSingleton<LocationListStore>();
                    services.AddSingleton<RegionSelectedStore>();
                    services.AddSingleton<ProvinceSelectedStore>();
                    services.AddSingleton<DistrictSelectedStore>();
                    services.AddSingleton<LocationSelectedStore>();


                    services.AddSingleton<ArchivesListStore>();
                    services.AddSingleton<ArchiveSelectedStore>();
                    services.AddSingleton<ArchiveTemporalStore>();

                    services.AddSingleton<FolderListStore>();
                    services.AddSingleton<FolderSelectedStore>();
                    services.AddSingleton<ProjectListStore>();
                    services.AddSingleton<ProjectSelectedStore>();
                    services.AddSingleton<ProjectTemporalStore>();
                    services.AddSingleton<SubBudgetListStore>();
                    services.AddSingleton<SubBudgetListStore>();


                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<HomeNavigationStore>();
                    services.AddSingleton<BudgetNavigationStore>();
                    services.AddSingleton<ProjectNavigationStore>();
                    services.AddSingleton<CurrencyNavigationStore>();
                    services.AddSingleton<ClientNavigationStore>();
                    services.AddSingleton<LocationNavigationStore>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<SuperModalNavigationStore>();


                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });

            }).Build();                   

        }
       

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            IDatabaseContextFactory databaseFactory = _host.Services.GetRequiredService<IDatabaseContextFactory>();
            using (DatabaseContext context = databaseFactory.CreateDbContext())
            {
                context.Database.EnsureCreated();
            }
                    
            NavigationService<HomeViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<HomeViewModel>>();
            navigationService.Navigate();            

            _host.Services.GetRequiredService<LocationListViewModel>();
            _host.Services.GetRequiredService<CurrencyListViewModel>();
            _host.Services.GetRequiredService<ClientListViewModel>();


            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();                 
            base.OnStartup(e);
            
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }

    }

}
