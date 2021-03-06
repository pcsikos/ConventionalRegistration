﻿using System;
using System.Linq;
using ConventionalRegistration.Configuration;
using ConventionalRegistration.Diagnostics;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAssembly;
using TestAssembly.Data;

namespace ConventionalRegistration.Unity.Tests
{
    [TestClass]
    public class UnityConventionRegistrationTests : InstanceResolverTests
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            var config = BehaviorConfiguration.Default;
            config.Log = (mode, message) =>
            {
                if (mode == DiagnosticLevel.Warning)
                {
                    TestContext.WriteLine(message);
                }
            };
            config.ExcludedServices = new[] { typeof(ICommand), typeof(IQuery<>) };

            Container.RegistrationByConvention(config, (builder, implementaionFilter) =>
            {
                builder.Define(source => source
                          .FromAssemblyContaining<ITransientService>()
                          .Select(t => t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                          .UsingAllInterfacesStrategy()
                        .RegisterEach(entry =>
                        {
                            if (entry.ServicesTypes.Count() == 1)
                            {
                                Container.RegisterType(entry.ServicesTypes.First(), entry.ImplementationType, "Decoratable");
                            }
                            else
                            {
                                foreach (var serviceType in entry.ServicesTypes)
                                {
                                    Container.RegisterType(serviceType, entry.ImplementationType, "Decoratable");
                                }
                            }
                            implementaionFilter.ExcludeImplementation(entry.ImplementationType);//, entry.ServicesTypes);
                        }));

                builder.Define(source => source
                         .FromAssemblyContaining<ITransientService>()
                         .SelectAllClasses()
                         .WithoutAttribute<ExcludeFromRegistrationAttribute>()
                         .Excluding(typeof(TransientService2), typeof(Repository<>), typeof(XmlDataProvider))
                         .UsingAllInterfacesStrategy());

                builder.RegisterAsCollection(typeof(IValidator<>));
                builder.RegisterAsCollection(typeof(ICommandValidator<>));
            });
          
            Container.RegisterType<ISingletonService, SingletonService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<DbConnection>(new PerThreadLifetimeManager(), new InjectionFactory(c => new DbConnection("Datasource=source")));
            Container.RegisterType<IDataProvider>(new InjectionFactory(c => new XmlDataProvider("drive:\\path")));
            Container.RegisterType<DbContext1>(new PerThreadLifetimeManager());
            Container.RegisterType<DbContext2>(new PerThreadLifetimeManager());
            RegisterMultipleServices(new[] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), new ContainerControlledLifetimeManager());
            Container.RegisterType<Func<ITransientService>>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => (Func<ITransientService>)(() => c.Resolve<ITransientService>())));
            Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            Container.RegisterType<IValidator<Customer>, IdValidator<Customer>>("IdValidator<Customer>");
            Container.RegisterType<IValidator<Product>, IdValidator<Product>>("IdValidator<Product>");
            Container.RegisterType<IValidator<Customer>, CustomerLocationValidator>("CustomerLocationValidator", new InjectionFactory(c => new CustomerLocationValidator { Name = "abc" }));

            Container.RegisterType(typeof(ICommandHandler<>), typeof(TransactionCommandHandler<>), "TransactionCommandHandler",
                new InjectionConstructor(
                    new ResolvedParameter(typeof(ICommandHandler<>), "Decoratable")));
            Container.RegisterType(typeof(ICommandHandler<>), typeof(LoggerCommandHandler<>), new InjectionConstructor(new ResolvedParameter(typeof(ICommandHandler<>), "TransactionCommandHandler")));
            //Container.Verify();
        }
    }


}
