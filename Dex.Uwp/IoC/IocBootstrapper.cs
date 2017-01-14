using Dex.Core.DataAccess;
using Dex.Core.Repositories;
using Dex.Uwp.DataAccess;
using Dex.Uwp.Services;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Serilog;
using System.IO;
using Windows.Storage;
using Dex.Core.Services;

namespace Dex.Uwp.IoC
{
    internal class IocBootstrapper
    {
        public IocBootstrapper()
        {
            Init();
        }

        private UnityContainer Container { get; set; }

        private void ConfigureRegistries()
        {
            InitLogger();
            RegisterServices();
            RegisterRepositories();
        }

        private void Init()
        {
            Container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(Container));

            ConfigureRegistries();
        }

        private void InitLogger()
        {
            ILogger logger = new LoggerConfiguration()
               .WriteTo.File(Path.Combine(ApplicationData.Current.LocalFolder.Path, "log.txt"))
               .CreateLogger();

            UwpExceptionSinkService.CreateExceptionSink(logger);

            Container.RegisterInstance(logger);
        }

        private void RegisterRepositories()
        {
            Container.RegisterType<IPokemonRepository, PokemonRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMoveRepository, MoveRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEvolutionsRepository, EvolutionsRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPokePicturesSource, DefaultPokePicturesSource>("defaultSource", new ContainerControlledLifetimeManager());
            Container.RegisterType<IPokePicturesSource, OfficialPokePicturesSource>("officialSource", new ContainerControlledLifetimeManager());
        }

        private void RegisterServices()
        {
            Container.RegisterType<IJsonService, JsonService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IStoreService, StoreService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITypesService, TypesService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEncryptionService, SimpleSymmetricEncryptionService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAppLifecycleManager, AppLifecycleManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPokePicturesSourceProvider, PokePicturesSourceProvider>(new ContainerControlledLifetimeManager());

            Container.RegisterType<LocalFileDataSource>(new ContainerControlledLifetimeManager())
                .RegisterType<IPokemonsDataSource, LocalFileDataSource>()
                .RegisterType<IMovesDataSource, LocalFileDataSource>()
                .RegisterType<ITypeEffectivenessDataSource, LocalFileDataSource>()
                .RegisterType<IEvolutionsDataSource, LocalFileDataSource>();
        }
    }
}