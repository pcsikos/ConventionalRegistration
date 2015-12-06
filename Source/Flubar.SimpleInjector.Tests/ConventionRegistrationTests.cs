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
                builder.ExplicitRegister<ISingletonService, SingletonService>(Lifestyle.Singleton);
                builder.ExplicitRegister(() => new DbConnection("Datasource=flubar"), Lifestyle.Scoped);
                builder.ExplicitRegister<DbContext1>(Lifestyle.Scoped);
                builder.ExplicitRegister<DbContext2>(Lifestyle.Scoped);

                builder.ExplicitRegisterMultipleServices(new[] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), Lifestyle.Singleton);
                builder.ExplicitRegisterFunc<ITransientService>();

                builder.ExplicitRegisterDecorator(typeof(ICommand), typeof(TransactionCommand));
                builder.ExplicitRegisterDecorator(typeof(ICommand), typeof(LoggerCommand));



                builder.Define(source => source
                     .FromAssemblyContaining<ITransientService>()
                     .SelectAllClasses()
                     .UsingAllInterfacesStrategy());
            });
        }
    }
}
