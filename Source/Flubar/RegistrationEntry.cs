using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Infrastructure;

namespace Flubar
{
    /// <summary>
    /// Represents a registration entry with implementation and related services.
    /// </summary>
    public class RegistrationEntry : IRegistrationEntry
    {
        private readonly Type[] serviceTypes;
        private Type implementationType;

        public RegistrationEntry(Type implementationType)
            : this(implementationType, implementationType)
        {
        }

        public RegistrationEntry(Type implementationType, Type serviceType)
            : this(implementationType, new[] { serviceType })
        {
            Check.NotNull(implementationType, nameof(implementationType));
            Check.NotNull(serviceType, nameof(serviceType));
        }

        public RegistrationEntry(Type implementationType, IEnumerable<Type> serviceTypes)
        {
            Check.NotEmpty(serviceTypes, "serviceTypes");
            Check.NotNull(implementationType, "implementationType");

            this.serviceTypes = serviceTypes.ToArray();
            this.implementationType = implementationType;
        }

        public IEnumerable<Type> ServicesTypes => serviceTypes;

        public Type ImplementationType => implementationType;
    }
}
