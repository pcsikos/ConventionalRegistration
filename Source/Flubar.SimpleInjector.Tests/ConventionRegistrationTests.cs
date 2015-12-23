using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using TestAssembly;
using TestAssembly.Data;

namespace Flubar.SimpleInjector.Tests
{
    [TestClass]
    public class ConventionRegistrationTests : InstanceResolverTests
    {
        public ConventionRegistrationTests()
        {
            var config = BehaviorConfiguration.Default;
            config.Log = (mode, message) =>
            {
                if (mode == DiagnosticMode.Warning)
                System.Diagnostics.Debug.WriteLine(message);
            };
            Container.RegistrationByConvention(config, builder =>
            {
                builder.ExplicitRegistration(c =>
                {
                    c.Register<ISingletonService, SingletonService>(Lifestyle.Singleton);
                    c.Register(() => new DbConnection("Datasource=flubar"), Lifestyle.Scoped);
                    c.Register<DbContext1>(Lifestyle.Scoped);
                    c.Register<DbContext2>(Lifestyle.Scoped);
                    c.Register(new[] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), Lifestyle.Singleton);
                    c.RegisterFunc<ITransientService>();

                    c.RegisterDecorator(typeof(ICommand), typeof(TransactionCommand));
                    c.RegisterDecorator(typeof(ICommand), typeof(LoggerCommand));
                });

                builder.Define(source => source
                     .FromAssemblyContaining<ITransientService>()
                     .SelectAllClasses()
                     .UsingAllInterfacesStrategy());
            });
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var container = new Container();
            container.RegisterCollection<IHandler>(new [] { typeof(MailHandler), typeof(SaveHandler) });
            container.Register<IHandler, MailHandler>();

            var handlers = container.GetAllInstances<IHandler>().ToArray();
            var handler = container.GetInstance<IHandler>();
        }
    }
}
