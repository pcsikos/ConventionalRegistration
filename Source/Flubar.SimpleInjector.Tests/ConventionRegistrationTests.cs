using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using TestAssembly;
using TestAssembly.Data;
using System;

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
            config.ExcludedServices = new[] { typeof(ICommand) };
            Container.RegistrationByConvention(config, builder =>
            {
                builder.ExplicitRegistration(c =>
                {
                    c.RegisterSingleton<ISingletonService, SingletonService>();
                    c.Register(() => new DbConnection("Datasource=flubar"), Lifestyle.Scoped);
                    c.Register<IDataProvider>(() => new XmlDataProvider("flubar:\\path"));
                    c.Register<DbContext1>(Lifestyle.Scoped);
                    c.Register<DbContext2>(Lifestyle.Scoped);
                    c.RegisterAll(new[] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), Lifestyle.Singleton);
                    c.RegisterSingleton<Func<ITransientService>>(() => Container.GetInstance<ITransientService>());

                    c.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionCommandHandler<>));
                    c.RegisterDecorator(typeof(ICommandHandler<>), typeof(LoggerCommandHandler<>));
                });

                builder.RegisterAsCollection(typeof(IValidator<>));
                builder.RegisterAsCollection(typeof(ICommandValidator<>));

                builder.Define(source => source
                     .FromAssemblyContaining<ITransientService>()
                     .SelectAllClasses()
                     .WithoutAttribute<ExcludeFromRegistrationAttribute>()
                     .Excluding(typeof(TransientService2), typeof(Repository<>), typeof(XmlDataProvider))
                     .UsingAllInterfacesStrategy());
            });
            Container.RegisterConditional(typeof(IRepository<>), typeof(Repository<>), context => !context.Handled);
            Container.Verify();
        }
    }
}
