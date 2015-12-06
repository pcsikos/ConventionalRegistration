using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.SimpleInjector
{
    public class SimpleInjectorConventionBuilder : ConventionBuilder<Lifestyle>
    {
        private readonly Container container;

        internal SimpleInjectorConventionBuilder(SimpleInjectorContainerFacade containerFacade, BehaviorConfiguration behaviorConfiguration) 
            : base(containerFacade, behaviorConfiguration)
        {
            container = containerFacade.InnerContainer;
        }

        public Container Container
        {
            get { return container; }
        }

        public void ExplicitRegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifestyle)
        {
            var registration = lifestyle.CreateRegistration(implementation, Container);
            foreach (var serviceType in serviceTypes.Where(t => t != typeof(IDisposable)))
            {
                Container.AddRegistration(serviceType, registration);
            }
            ExcludeService(serviceTypes, implementation);
        }

        public void ExplicitRegisterFunc<T>()
            where T : class
        {
            Container.RegisterSingleton<Func<T>>(() => Container.GetInstance<T>());
        }

        public void ExplicitRegisterDecorator(Type serviceType, Type decoratorType)
        {
            Container.RegisterDecorator(serviceType, decoratorType);
        }

        public void ExplicitRegisterCollection<TService>(IEnumerable<Type> serviceTypes)
            where TService : class
        {
            Container.RegisterCollection<TService>(serviceTypes);
            //ExcludeService(serviceTypes, implementation);
        }
    }
}
