using System;
using System.Collections.Generic;

namespace Flubar.TypeFiltering
{
    /// <summary>
    /// Represents a collection of excluded <see cref="Type"/>. 
    /// </summary>
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
}
