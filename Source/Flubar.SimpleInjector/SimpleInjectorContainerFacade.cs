using System;
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
        }

        public Lifestyle GetSingletonLifetime()
        {
            return Lifestyle.Singleton;
        }

        public Lifestyle GetDefaultLifetime()
        {
            return Lifestyle.Transient;
        }

        public object InnerContainer
        {
            get { return container; }
        }

        #endregion
    }
}
