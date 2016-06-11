using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar.Unity
{
    public class UnityContainerAdapter : IContainer<LifetimeManager>
    {
        private readonly UnityContainer container;
        readonly IServiceMappingTracker typeExclusionTracker;

        public UnityContainerAdapter(UnityContainer container, IServiceMappingTracker typeExclusionTracker)
        {
            this.typeExclusionTracker = typeExclusionTracker;
            this.container = container;
        }

        public LifetimeManager GetDefaultLifetime()
        {
            return new TransientLifetimeManager();
        }

        public LifetimeManager GetSingletonLifetime()
        {
            return new ContainerControlledLifetimeManager();
        }

        public void RegisterMultipleImplementations(Type serviceType, IEnumerable<Type> implementations)
        {
            foreach (var impl in implementations)
            {
                var concreteServiceType = serviceType;
                if (!serviceType.IsGenericTypeDefinition)
                {
                    container.RegisterType(concreteServiceType, impl, impl.Name);
                    continue;
                }

                foreach(var genericServiceType in impl.GetInterfaces().Where(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == serviceType))
                {
                    container.RegisterType(genericServiceType, impl, impl.Name);
                }
            }
        }

        public void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, LifetimeManager lifetime = null)
        {
            container.RegisterType(implementation, lifetime);
            foreach (var type in serviceTypes)
            {
                container.RegisterType(type, implementation);
            }
            typeExclusionTracker.ExcludeServices(serviceTypes, implementation);
        }

        public void RegisterService(Type serviceType, Type implementation, LifetimeManager lifetime = null)
        {
            container.RegisterType(serviceType, implementation, lifetime);
            typeExclusionTracker.ExcludeService(serviceType, implementation);
        }
    }
}
