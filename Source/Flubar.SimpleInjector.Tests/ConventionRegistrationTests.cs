using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using TestAssembly;
using TestAssembly.Data;
using System.Diagnostics;
using System;
using SimpleInjector.Extensions.LifetimeScoping;

namespace Flubar.SimpleInjector.Tests
{
    [TestClass]
    public class ConventionRegistrationTests : InstanceResolverTests
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
            Container.RegistrationByConvention(config, builder =>
            {
                builder.ExplicitRegistration(c =>
                {
                    c.Register<ISingletonService, SingletonService>(Lifestyle.Singleton);
                    c.Register(() => new DbConnection("Datasource=flubar"), Lifestyle.Scoped);
                    c.Register<IDataProvider>(() => new XmlDataProvider("flubar:\\path"));
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
                     .WithoutAttribute<ExcludeFromRegistrationAttribute>()
                     .Excluding(typeof(TransientService2))//, typeof(XmlDataProvider))
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
