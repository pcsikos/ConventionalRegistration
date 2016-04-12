using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    public class TypeExclusionTracker : ITypeExclusionTracker
    {
        private readonly IDictionary<Type, RegisteredService> registeredServices;
        private readonly IDictionary<Type, ExcludedImplementation> excludedImplementations;

        public TypeExclusionTracker()
        {
            registeredServices = new Dictionary<Type, RegisteredService>();
            excludedImplementations = new Dictionary<Type, ExcludedImplementation>();
        }

        public void ExcludeImplementation(Type implementation, IEnumerable<Type> services = null)
        {
            if (!excludedImplementations.ContainsKey(implementation))
            {
                excludedImplementations.Add(implementation, new ExcludedImplementation(implementation, services));
                return;
            }
            var excluded = excludedImplementations[implementation];
            excluded.AddServices(services);
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

        public bool ContainsImplementation(Type implementation)
        {
            return excludedImplementations.ContainsKey(implementation);
        }

        public IEnumerable<Type> GetImplemetationServices(Type implementation)
        {
            return excludedImplementations[implementation].Services;
        }

        public bool ContainsService(Type serviceType)
        {
            return registeredServices.ContainsKey(serviceType);
        }

        public Type GetServiceImplementation(Type serviceType)
        {
            return registeredServices[serviceType].Implementation;
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

        class ExcludedImplementation
        {
            readonly Type implementation;
            readonly IList<Type> excludedServices;

            public ExcludedImplementation(Type implementation, IEnumerable<Type> services)
            {
                this.excludedServices = new List<Type>(services ?? new Type[0]);
                this.implementation = implementation;
            }

            public Type Implementation => implementation;

            public IEnumerable<Type> Services => excludedServices;

            public void AddServices(IEnumerable<Type> services)
            {
                foreach (var service in services)
                {
                    if (!excludedServices.Contains(service))
                    {
                        excludedServices.Add(service);
                    }
                }
            }
        }
    }

    /// <summary>
    /// todo: split query API from update
    /// </summary>
    public interface ITypeExclusionTracker : IServiceExclusion, IImplementationExclusion
    {
        bool ContainsImplementation(Type implementation);
        IEnumerable<Type> GetImplemetationServices(Type implementation);
        bool ContainsService(Type serviceType);
        Type GetServiceImplementation(Type serviceType);
    }
}
