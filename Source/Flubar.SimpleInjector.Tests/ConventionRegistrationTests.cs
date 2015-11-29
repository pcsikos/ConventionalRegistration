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
            
            Container.RegistrationByConvention(builder =>
            {
                builder.Register<ISingletonService, SingletonService>(Lifestyle.Singleton);
                builder.Register(() => new DbConnection("Datasource=flubar"), Lifestyle.Scoped);
                builder.Register<DbContext1>(Lifestyle.Scoped);
                builder.Register<DbContext2>(Lifestyle.Scoped);

                builder.RegisterMultipleServices(new[] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), Lifestyle.Singleton);
                builder.RegisterFunc<ITransientService>();

                builder.RegisterDecorator(typeof(ICommand), typeof(TransactionCommand));
                builder.RegisterDecorator(typeof(ICommand), typeof(LoggerCommand));

                builder.Define(source => source
                     .FromAssemblyContaining<ITransientService>()
                     .SelectAllClasses()
                     .UsingAllInterfacesStrategy());
            });
        }
    }
}
