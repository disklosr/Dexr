﻿using Dex.Core.DataAccess;
using Dex.Core.Repositories;
using Dex.Uwp.DataAccess;
using Dex.Uwp.Services;
using Dex.Uwp.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;

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
            RegisterServices();
            RegisterRepositories();
            RegisterViewModels();
        }

        private void Init()
        {
            Container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(Container));
            ConfigureRegistries();
        }

        private void RegisterRepositories()
        {
            Container.RegisterType<IPokemonRepository, PokemonRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMoveRepository, MoveRepository>(new ContainerControlledLifetimeManager());
        }

        private void RegisterServices()
        {
            Container.RegisterType<IJsonService, JsonService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<LocalFileDataSource>(new ContainerControlledLifetimeManager())
                .RegisterType<IPokemonsDataSource, LocalFileDataSource>()
                .RegisterType<IMovesDataSource, LocalFileDataSource>();
        }

        private void RegisterViewModels()
        {
            Container.RegisterType<PokedexViewModel>(new ContainerControlledLifetimeManager());
        }
    }
}