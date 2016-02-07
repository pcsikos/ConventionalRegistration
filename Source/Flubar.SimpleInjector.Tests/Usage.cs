using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using TestAssembly;

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
                //builder.ExplicitRegistration(() => new Usage());

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

        [TestMethod]
        public void Run2()
        {
            var container = new Container();

            container.Register<ITransientService, TransientService>();
            container.GetRegistration(typeof(ITransientService));
            container.Register<ISingletonService, SingletonService>(Lifestyle.Singleton);

        }
    }
}
