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

            container.RegistrationByConvention(builder =>
            {
                //builder.DefineGlobal(types => types.)
                builder.Register(() => new Usage());

                builder.Define(source => source
                     .FromThisAssembly()
                     .SelectAllClasses()
                     //.NotInNamespaceStartingWith("System")
                     .UsingDefaultInterfaceStrategy()
                     .RegisterEach(registration =>
                         container.Register(registration.ServicesTypes.First(), registration.ImplementationType))
                     );
                //.Notify.OnRegistration(new RegistrationEntry(null))
            });

            container.RegistrationByConvention(builder =>
                builder.Define(x => x.ExplicitlySpecifyTypes(typeof(Usage))
                            .UsingDefaultInterfaceStrategy(), x => x.Singleton)
            );

        }
    }
}
