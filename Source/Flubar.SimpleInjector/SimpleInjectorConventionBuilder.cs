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

        public void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifestyle)
        {
            var registration = lifestyle.CreateRegistration(implementation, Container);
            foreach (var serviceType in serviceTypes.Where(t => t != typeof(IDisposable)))
            {
                Container.AddRegistration(serviceType, registration);
                Exclude(serviceType);
            }
        }

        public void RegisterFunc<T>()
            where T : class
        {
            Container.RegisterSingleton<Func<T>>(() => Container.GetInstance<T>());
            var type = typeof(T);
            if (type.IsInterface)
            {
                Exclude(type);
            }
        }

        public void RegisterDecorator(Type serviceType, Type decoratorType)
        {
            Container.RegisterDecorator(serviceType, decoratorType);
            Exclude(serviceType);
        }
    }
}
