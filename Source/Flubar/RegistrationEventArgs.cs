using System;
using System.Collections.Generic;

namespace Flubar
{
    public class RegistrationEventArgs : EventArgs
    {
        private readonly Type implementation;
        private readonly IEnumerable<Type> services;

        public RegistrationEventArgs(Type service, Type implementation)
            : this(service == null ? new Type[0] : new[] { service }, implementation)
        {
        }

        public RegistrationEventArgs(Type service)
            : this(service, null)
        {
        }

        public RegistrationEventArgs(IEnumerable<Type> services, Type implementation)
        {
            this.services = services;
            this.implementation = implementation;
        }

        public Type Implementation => implementation;
        public IEnumerable<Type> Services => services;
    }
}
