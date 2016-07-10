using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar.TypeFiltering
{
    /// <summary>
    /// Represents a service with available implementations.
    /// </summary>
    public class ServiceImplementation
    {
        private readonly Type serviceType;
        private readonly ISet<Type> implementations;

        public ServiceImplementation(Type serviceType)
        {
            this.serviceType = serviceType;
            implementations = new HashSet<Type>();
        }

        public Type ServiceType => serviceType;

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

        public IEnumerable<Type> GetImplementations() => implementations;
    }
}