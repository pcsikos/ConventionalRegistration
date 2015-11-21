using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;

namespace Flubar.SimpleInjector.Tests
{
    [TestClass]
    public class Usage
    {
        [TestMethod]
        public void Run()
        {
            var container = new Container();

            container.RegistrationByConvention()
                //.GlobalRule(types => 1)
                .Define(source => source
                    .FromThisAssembly()
                    .SelectAllClasses()
                    //.NotInNamespaceStartingWith("System")
                    .UsingDefaultInterfaceStrategy()
                    .RegisterEach(registration =>
                        container.Register(registration.ServicesTypes.First(), registration.ImplementationType))
                    )
                .Register(() => new Usage())
                //.Notify.OnRegistration(new AssemblyCrawler.RegistrationEntry(null))
                .Apply();

            container.RegistrationByConvention()
                .Define(x => x.ExplicitlySpecifyTypes(typeof(Usage))
                            .UsingDefaultInterfaceStrategy(), x => x.Singleton);

        }
    }
}
