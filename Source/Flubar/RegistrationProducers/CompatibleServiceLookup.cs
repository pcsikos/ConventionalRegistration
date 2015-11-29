using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Infrastructure;

namespace Flubar.RegistrationProducers
{
    public class CompatibleServiceLookup : IRegistrationServiceSelector
    {
        private readonly Func<Type, bool> filter;

        public CompatibleServiceLookup()
        {
        }

        public CompatibleServiceLookup(Func<Type, bool> filter)
        {
            Check.NotNull(filter, "filter");

            this.filter = filter;
        }

        #region IImplementationServiceSelector Members

        public virtual IEnumerable<Type> GetServicesFrom(Type implementation)
        {
            Check.NotNull(implementation, "implementation");
            if (!implementation.IsClass || implementation.IsAbstract)
            {
                return Enumerable.Empty<Type>();
            }

            IEnumerable<Type> interfaces = (implementation.IsGenericTypeDefinition) ? GetGenericInterfacesMatching(implementation) : implementation.GetInterfaces();
            if (filter == null)
            {
                return interfaces;
            }
            return interfaces.Where(filter);
        }

        private IEnumerable<Type> GetGenericInterfacesMatching(Type implementation)
        {
            var interfaces = implementation.GetInterfaces().ToArray();
            interfaces = interfaces.Where(x => x.IsConstructedGenericType && x.GetGenericArguments().SequenceEqual(implementation.GetGenericArguments()))
                .Select(x => x.IsConstructedGenericType ? x.GetGenericTypeDefinition() : x).ToArray();
            return interfaces;
        }

        #endregion
    }
}
