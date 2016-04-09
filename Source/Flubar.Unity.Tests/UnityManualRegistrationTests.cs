using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TestAssembly;
using TestAssembly.Data;

namespace Flubar.Unity.Tests
{
    [TestClass]
    public class UnityManualRegistrationTests : InstanceResolverTests
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            Container.RegisterType<ITransientService, TransientService>();
            Container.RegisterType<ISingletonService, SingletonService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<DbConnection>(new PerThreadLifetimeManager(), new InjectionFactory(c => new DbConnection("Datasource=flubar")));
            Container.RegisterType<DbContext1>(new PerThreadLifetimeManager());
            Container.RegisterType<DbContext2>(new PerThreadLifetimeManager());
            RegisterMultipleServices(new[] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), new ContainerControlledLifetimeManager());

            Container.RegisterType<Func<ITransientService>>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => (Func<ITransientService>)(() => c.Resolve<ITransientService>())));
            Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            Container.RegisterType<ICommandHandler<CustomCommand>, CustomCommandHandler>("CommandHandler");
            Container.RegisterType<IDataProvider>(new InjectionFactory(c => new XmlDataProvider("flubar:\\path")));
            Container.RegisterType<IValidator<Customer>, CustomerLocationValidator>("CustomerLocationValidator");
            Container.RegisterType<IValidator<Customer>, CustomerCreditValidator>("CustomerCreditValidator");
            Container.RegisterType<IValidator<Order>, OrderValidator>("OrderValidator");
            Container.RegisterType<ICommandValidator<PlaceOrderCommand>, PlaceOrderCommandValidator>("PlaceOrderCommandValidator");
            Container.RegisterType<ICommandValidator<CancelOrderCommand>, CancelOrderCommandValidator>("CancelOrderCommandValidator");

            Container.RegisterType(typeof(ICommandHandler<>), typeof(TransactionCommandHandler<>), "TransactionCommandHandler",
                new InjectionConstructor(
                    new ResolvedParameter(typeof(ICommandHandler<>), "CommandHandler")));
            Container.RegisterType(typeof(ICommandHandler<>), typeof(LoggerCommandHandler<>), new InjectionConstructor(new ResolvedParameter(typeof(ICommandHandler<>), "TransactionCommandHandler")));

            //Container.Verify();
        }

       

        //public void RegisterFunc<T>()
        //    where T : class
        //{
        //    Container.RegisterSingleton<Func<T>>(() => Container.GetInstance<T>());
        //}
    }
}
