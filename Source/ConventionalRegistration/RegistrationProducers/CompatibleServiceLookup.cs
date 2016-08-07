using System;
using System.Collections.Generic;
using System.Linq;

namespace ConventionalRegistration.RegistrationProducers
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
            Check.NotNull(implementation, nameof(implementation));
            if (!implementation.IsClass || implementation.IsAbstract)
            {
                return Enumerable.Empty<Type>();
            }

            IEnumerable<Type> interfaces = implementation.IsGenericTypeDefinition ? implementation.GetGenericInterfacesMatching() : implementation.GetInterfaces();
            if (filter == null)
            {
                return interfaces;
            }
            return interfaces.Where(filter);
        }

        #endregion
    }
}
