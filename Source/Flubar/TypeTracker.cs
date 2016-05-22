using System;
using System.Collections.Generic;

namespace Flubar
{
    public class ServiceMappingTracker : IServiceMappingTracker
    {
        private readonly IDictionary<Type, CustomRegistration> customRegistrations = new Dictionary<Type, CustomRegistration>();

        public IEnumerable<Type> RegisterMapping(IEnumerable<Type> services, Type implementationType)
        {
            foreach (var serviceType in services)
            {
                if (customRegistrations.ContainsKey(serviceType))
                {
                    var customRegistration = customRegistrations[serviceType];
                    customRegistration.AddImlementation(implementationType);
                    continue;
                }
                var genericType = serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : null;
                if (genericType != null && customRegistrations.ContainsKey(genericType))
                {
                    var customRegistration = customRegistrations[genericType];
                    customRegistration.AddImlementation(implementationType);
                    continue;
                }
                yield return serviceType;
            }
        }

        public void RegisterMonitoredType(Type serviceType, Action<IEnumerable<Type>> callback)
        {
            if (customRegistrations.ContainsKey(serviceType))
            {
                throw new ArgumentException();
            }
            customRegistrations.Add(serviceType, new CustomRegistration(serviceType, callback));
        }

        public void Resolve()
        {
            foreach (var customRegistration in customRegistrations.Values)
            {
                customRegistration.InvokeCallback();
            }
        }

        private class CustomRegistration
        {
            readonly Type serviceType;
            readonly Action<IEnumerable<Type>> callback;
            private readonly ISet<Type> implementations;

            public CustomRegistration(Type serviceType, Action<IEnumerable<Type>> callback)
            {
                this.callback = callback;
                this.serviceType = serviceType;
                implementations = new HashSet<Type>();
            }

            public void AddImlementation(Type implementationType)
            {
                if (!implementations.Contains(implementationType))
                {
                    implementations.Add(implementationType);
                }
            }

            public IEnumerable<Type> GetImplementations()
            {
                return implementations;
            }

            internal void InvokeCallback()
            {
                callback(GetImplementations());
            }
        }
    }
}
