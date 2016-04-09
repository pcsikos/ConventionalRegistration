using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAssembly;
using TestAssembly.Data;

namespace Flubar.Unity.Tests
{
    [TestClass]
    public class UnityConventionRegistrationTests : InstanceResolverTests
    {
        class UnityContainerAdapter : IContainer<LifetimeManager>
        {
            public LifetimeManager GetDefaultLifetime()
            {
                throw new NotImplementedException();
            }

            public LifetimeManager GetSingletonLifetime()
            {
                throw new NotImplementedException();
            }

            public void RegisterAll(IEnumerable<Type> serviceTypes, Type implementation, LifetimeManager lifetime = null)
            {
                throw new NotImplementedException();
            }

            public void RegisterType(Type serviceType, Type implementation, LifetimeManager lifetime = null)
            {
                throw new NotImplementedException();
            }
        }

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            Container.RegisterType<ISingletonService, SingletonService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<DbConnection>(new PerThreadLifetimeManager(), new InjectionFactory(c => new DbConnection("Datasource=flubar")));
            Container.RegisterType<IDataProvider>(new InjectionFactory(c => new XmlDataProvider("flubar:\\path")));
            Container.RegisterType<DbContext1>(new PerThreadLifetimeManager());
            Container.RegisterType<DbContext2>(new PerThreadLifetimeManager());
            RegisterMultipleServices(new[] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), new ContainerControlledLifetimeManager());
            Container.RegisterType<Func<ITransientService>>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => (Func<ITransientService>)(() => c.Resolve<ITransientService>())));


            var config = BehaviorConfiguration.Default;
            config.Log = (mode, message) =>
            {
                if (mode == DiagnosticLevel.Warning)
                {
                    TestContext.WriteLine(message);
                }
            };
            config.ExcludedServices = new[] { typeof(ICommand) };

            var exclusion = new TypeExclusionTracker();
            exclusion.ExcludeService(typeof(ISingletonService), typeof(SingletonService));
            exclusion.ExcludeImplementation(typeof(DbConnection));
            exclusion.ExcludeService(typeof(IDataProvider), typeof(XmlDataProvider));
            exclusion.ExcludeImplementation(typeof(DbContext1));
            exclusion.ExcludeImplementation(typeof(DbContext2));
            exclusion.ExcludeImplementation(typeof(FileOperation), new[] { typeof(IFileRead), typeof(IFileWrite) });

            using (var builder = new ConventionBuilder<LifetimeManager>(new UnityContainerAdapter(), config, new TypeExclusionTracker()))
            {
                //builder.SearchForImplementations(typeof(IValidator<>), types =>
                //{
                //    Container.RegisterCollection(typeof(IValidator<>), types);
                //});
                //builder.SearchForImplementations(typeof(ICommandValidator<>), types =>
                //{
                //    Container.RegisterCollection(typeof(ICommandValidator<>), types);
                //});

                builder.Define(source => source
                         .FromAssemblyContaining<ITransientService>()
                         .SelectAllClasses()
                         .WithoutAttribute<ExcludeFromRegistrationAttribute>()
                         .Excluding(typeof(TransientService2), typeof(Repository<>), typeof(XmlDataProvider))
                         .UsingAllInterfacesStrategy()
                         .RegisterEach(entry =>
                         {
                             if (entry.ServicesTypes.Count() == 1)
                             {
                                 Container.RegisterType(entry.ServicesTypes.First(), entry.ImplementationType);
                             }
                             else
                             {
                                 foreach (var serviceType in entry.ServicesTypes)
                                 {
                                     Container.RegisterType(serviceType, entry.ImplementationType);
                                 }
                             }
                         }));
            }

            Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
          
            Container.RegisterType(typeof(ICommandHandler<>), typeof(TransactionCommandHandler<>), "TransactionCommandHandler",
                new InjectionConstructor(
                    new ResolvedParameter(typeof(ICommandHandler<>), "CommandHandler")));
            Container.RegisterType(typeof(ICommandHandler<>), typeof(LoggerCommandHandler<>), new InjectionConstructor(new ResolvedParameter(typeof(ICommandHandler<>), "TransactionCommandHandler")));

            //Container.RegisterConditional(typeof(IRepository<>), typeof(Repository<>), context => !context.Handled);
            //Container.Verify();
        }
    }

    
}
