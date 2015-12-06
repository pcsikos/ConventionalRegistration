using System;
using System.Linq;
using System.Collections.Generic;
using SimpleInjector;

namespace Flubar.SimpleInjector
{
    class SimpleInjectorContainerFacade : IContainerFacade<Lifestyle>
    {
        private readonly Container container;
        public SimpleInjectorContainerFacade(Container container)
        {
            this.container = container;
        }

        #region IContainerFacade<Lifestyle> Members

        public void Register(Type serviceType, Type implementation, Lifestyle lifetime)
        {
            container.Register(serviceType, implementation, lifetime ?? GetDefaultLifetime());
            //System.Diagnostics.Debug.WriteLine("{0} => {1} ({2})", serviceType, implementation, lifetime);
        }

        public void Register(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifetime)
        {
            if (lifetime == null)
            {
                lifetime = GetDefaultLifetime();
            }
            var registration = lifetime.CreateRegistration(implementation, container);
            foreach (var type in serviceTypes)
            {
                container.AddRegistration(type, registration);
            }
            //System.Diagnostics.Debug.WriteLine("{0} => {1} ({2})", string.Join(", ", serviceTypes.Select(x => x.Name).ToArray()), implementation, lifetime);
        }

        public void Register<TService>(Func<TService> instanceCreator, Lifestyle lifetime)
             where TService : class
        {
            container.Register(instanceCreator, lifetime ?? GetDefaultLifetime());
        }

        public Lifestyle GetSingletonLifetime()
        {
            return Lifestyle.Singleton;
        }

        public Lifestyle GetDefaultLifetime()
        {
            return Lifestyle.Transient;
        }


        public Container InnerContainer
        {
            get { return container; }
        }

        object IContainerFacade<Lifestyle>.InnerContainer
        {
            get { return container; }
        }

        #endregion
    }
}
