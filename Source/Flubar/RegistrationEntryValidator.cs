using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar
{
    class RegistrationEntryValidator
    {
        readonly IServiceMappingTracker exclusionTracker;
        readonly ILog logger;

        public RegistrationEntryValidator(IServiceMappingTracker exclusionTracker,
            ILog logger)
        {
            this.logger = logger;
            this.exclusionTracker = exclusionTracker;
        }

        public IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services)
        {
            //var allowedServices = GetAllowedServicesForImplementation(implementation, services);
            //var excludedServices = GetExcludedServices(services, allowedServices);
            //if (excludedServices.Any())
            //{
            //    WriteAboutExcludedImplementation(implementation, excludedServices);
            //}
            if (!services.Any())
            {
                return Enumerable.Empty<Type>();
            }

            var filteredServices = FilterUsedServices(services);
            if (filteredServices.Count() != services.Count())
            {
                WriteAboutExcludedServices(implementation, services, filteredServices);
            }
            return filteredServices;
        }

        private IEnumerable<Type> GetExcludedServices(IEnumerable<Type> servicesTypes, IEnumerable<Type> allowedServices)
        {
            return servicesTypes.Where(service => !allowedServices.Any(x => x == service));
        }

        private IEnumerable<Type> FilterUsedServices(IEnumerable<Type> services)
        {
            var allowedServices = services.Where(serviceType => !exclusionTracker.ContainsService(serviceType)).ToArray();
            if (!allowedServices.Any())
            {
                return Enumerable.Empty<Type>();
            }
            return allowedServices;
        }

        //private IEnumerable<Type> GetAllowedServicesForImplementation(Type implementation, IEnumerable<Type> services)
        //{
        //    if (exclusionTracker.ContainsImplementation(implementation))
        //    {
        //        var implementationServices = exclusionTracker.GetImplemetationServices(implementation);
        //        bool hasServices = implementationServices.Any();
        //        if (hasServices && implementationServices.Any(excludedService => services.Any(service => service == excludedService)))
        //        {
        //            var allowedServices = services.Where(service => !implementationServices.Any(excludedService => excludedService == service));
        //            hasServices = allowedServices.Any();
        //            if (hasServices)
        //            {
        //                return allowedServices;
        //            }
        //        }
        //        if (!hasServices)//exclude every service
        //        {
        //            return Enumerable.Empty<Type>();
        //        }
        //    }

        //    return services;
        //}

        //private void WriteAboutExcludedImplementation(Type implementation, IEnumerable<Type> excludedServices)
        //{
        //    if (excludedServices == null || !excludedServices.Any())
        //    {
        //        logger.Info("Excluded Implementation {0}.", implementation.FullName);
        //    }

        //    foreach (var serviceType in excludedServices)
        //    {
        //        logger.Info("Excluded Implementation {0} for service {1}.", implementation.FullName, serviceType.FullName);
        //    }
        //}

        private void WriteAboutExcludedServices(Type implementation, IEnumerable<Type> originalServices, IEnumerable<Type> filteredServices)
        {
            foreach (var serviceType in originalServices)
            {
                if (filteredServices.Any(type => type == serviceType))
                {
                    continue;
                }
                var implementationType = exclusionTracker.GetServiceImplementation(serviceType);
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

        private static string GetRegisteredServiceImplementationName(Type implementationType)
        {
            return implementationType == null ? "[Unknown]" : implementationType.FullName;
        }
    }

    public class ValidationResult
    {
        public IEnumerable<Type> Services { get; set; }
        public string MyProperty { get; set; }
    }
}
