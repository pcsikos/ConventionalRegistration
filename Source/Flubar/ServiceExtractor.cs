using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar
{
    public class ServiceExtractor : IServiceExtractor, IServiceFilter
    {
        private readonly IDictionary<Type, ServiceImplementation> customRegistrations = new Dictionary<Type, ServiceImplementation>();

        public IEnumerable<Type> GetAllowedServices(Type implementationType, IEnumerable<Type> services)
        {
            foreach (var serviceType in services)
            {
                if (customRegistrations.ContainsKey(serviceType))
                {
                    AddImplementation(implementationType, serviceType);
                    continue;
                }
                var genericType = serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : null;
                if (genericType != null && customRegistrations.ContainsKey(genericType))
                {
                    AddImplementation(implementationType, genericType);
                    continue;
                }
                yield return serviceType;
            }
        }
      
        private void AddImplementation(Type implementationType, Type serviceType)
        {
            var customRegistration = customRegistrations[serviceType];
            customRegistration.AddImlementation(implementationType);
        }

        public void RegisterMonitoredType(Type serviceType)
        {
            if (customRegistrations.ContainsKey(serviceType))
            {
                throw new ArgumentException();
            }
            customRegistrations.Add(serviceType, new ServiceImplementation(serviceType));
        }

        public IEnumerable<ServiceImplementation> GetServiceImplementations()
        {
            return customRegistrations.Values;
        }
    }

    public class ServiceImplementation
    {
        private readonly Type serviceType;
        private readonly ISet<Type> implementations;

        public ServiceImplementation(Type serviceType)
        {
            this.serviceType = serviceType;
            implementations = new HashSet<Type>();
        }

        public Type ServiceType
        {
            get
            {
                return serviceType;
            }
        }

        public void AddImlementation(Type implementationType)
        {
            if (!Validate(implementationType))
            {
                throw new Exception();
            }

            if (!implementations.Contains(implementationType))
            {
                implementations.Add(implementationType);
            }
        }

        private bool Validate(Type implementationType)
        {
            if (serviceType.IsInterface)
            {
                return implementationType.GetInterfaces().Any(face => face == serviceType || (face.IsGenericType && face.GetGenericTypeDefinition() == serviceType));
            }
            throw new NotImplementedException();
        }

        public IEnumerable<Type> GetImplementations()
        {
            return implementations;
        }
    }
}
