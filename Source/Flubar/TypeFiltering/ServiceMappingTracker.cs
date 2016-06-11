using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Diagnostics;

namespace Flubar.TypeFiltering
{
    public class ServiceMappingTracker : IServiceMappingTracker, IServiceFilter
    {
        private readonly IDictionary<Type, RegisteredService> registeredServices;
        private readonly ILog logger;

        public ServiceMappingTracker(ILog logger)
        {
            this.logger = logger;
            registeredServices = new Dictionary<Type, RegisteredService>();
        }

        public void ExcludeRegistration(IRegistrationEntry registration)
        {
            ExcludeServices(registration.ServicesTypes, registration.ImplementationType);
        }

        public void ExcludeService(Type serviceType, Type implementation)
        {
            if (!serviceType.IsInterface)
            {
                return;
            }

            if (!registeredServices.ContainsKey(serviceType))
            {
                var registeredService = new RegisteredService(serviceType, implementation);
                registeredServices.Add(serviceType, registeredService);
            }
        }

        public void ExcludeServices(IEnumerable<Type> serviceTypes, Type implementation)
        {
            foreach (var serviceType in serviceTypes)
            {
                ExcludeService(serviceType, implementation);
            }
        }

        public IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services)
        {
            var notUsedServices = FilterUsedServices(services);
            if (notUsedServices.Count() != services.Count())
            {
                WriteAboutExcludedServices(implementation, services, notUsedServices);
            }
            return notUsedServices;
        }

        private IEnumerable<Type> FilterUsedServices(IEnumerable<Type> services)
        {
            var allowedServices = services.Where(serviceType => !registeredServices.ContainsKey(serviceType)).ToArray();
            if (!allowedServices.Any())
            {
                return Enumerable.Empty<Type>();
            }
            return allowedServices;
        }

        private void WriteAboutExcludedServices(Type implementation, IEnumerable<Type> originalServices, IEnumerable<Type> filteredServices)
        {
            foreach (var serviceType in originalServices)
            {
                if (filteredServices.Any(type => type == serviceType))
                {
                    continue;
                }
                var implementationType = GetServiceImplementation(serviceType);
                var registeredImplementationName = GetRegisteredServiceImplementationName(implementationType);
                var skippingMessage = string.Format("Skipping {0} to {1}, because {2} is already registered to this service.",
                        serviceType.FullName, implementation.FullName, registeredImplementationName);
                if (implementation == implementationType)
                {
                    logger.Info(skippingMessage);
                }
                else
                {
                    logger.Warning(skippingMessage);
                }
            }
        }

        private Type GetServiceImplementation(Type serviceType)
        {
            return registeredServices[serviceType].Implementation;
        }

        private static string GetRegisteredServiceImplementationName(Type implementationType)
        {
            return implementationType == null ? "[Unknown]" : implementationType.FullName;
        }

        class RegisteredService
        {
            private readonly Type implementation;
            private readonly Type serviceType;

            public RegisteredService(Type serviceType, Type implementation)
            {
                this.serviceType = serviceType;
                this.implementation = implementation;
            }

            public Type Implementation => implementation;

            public Type ServiceType => serviceType;
        }
    }
}
