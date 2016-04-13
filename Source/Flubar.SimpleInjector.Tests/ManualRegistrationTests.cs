using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;
using SimpleInjector.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAssembly;
using TestAssembly.Data;

namespace Flubar.SimpleInjector.Tests
{
    [TestClass]
    public class ManualRegistrationTests : InstanceResolverTests
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            Container.Register<ITransientService, TransientService>();
            Container.Register<ISingletonService, SingletonService>(Lifestyle.Singleton);
            Container.Register(() => new DbConnection("Datasource=flubar"), Lifestyle.Scoped);
            Container.Register<DbContext1>(Lifestyle.Scoped);
            Container.Register<DbContext2>(Lifestyle.Scoped);
            RegisterMultipleServices(new [] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), Lifestyle.Singleton);
            RegisterFunc<ITransientService>();
            Container.Register(typeof(IRepository<>), typeof(Repository<>));
            Container.Register<ICommandHandler<CustomCommand>, CustomCommandHandler>();
            Container.Register<IDataProvider>(() => new XmlDataProvider("flubar:\\path"));
            Container.RegisterCollection(typeof(IValidator<>), new[] { typeof(CustomerLocationValidator), typeof(CustomerCreditValidator), typeof(OrderValidator) });
            Container.RegisterCollection(typeof(ICommandValidator<>), new[] { typeof(PlaceOrderCommandValidator), typeof(CancelOrderCommandValidator), typeof(AddressCommandValidator) });

            Container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionCommandHandler<>));
            Container.RegisterDecorator(typeof(ICommandHandler<>), typeof(LoggerCommandHandler<>));

            Container.Verify();
        }

        public void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifestyle)
        {
            var registration = lifestyle.CreateRegistration(implementation, Container);
            foreach (var type in serviceTypes.Where(t => t != typeof(IDisposable)))
            {
                Container.AddRegistration(type, registration);
            }
        }

        public void RegisterFunc<T>()
            where T : class
        {
            Container.RegisterSingleton<Func<T>>(() => Container.GetInstance<T>());
        }
    }
}
