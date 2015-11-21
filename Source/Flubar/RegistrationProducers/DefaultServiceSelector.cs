using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Infrastructure;

namespace Flubar.RegistrationProducers
{
    public class DefaultServiceSelector : IRegistrationServiceSelector
    {
        private readonly Func<Type, bool> filter;

        public DefaultServiceSelector()
        {
        }

        public DefaultServiceSelector(Func<Type, bool> filter)
        {
            Check.NotNull(filter, "filter");

            this.filter = filter;
        }

        #region IImplementationServiceSelector Members

        public IEnumerable<Type> GetServicesFrom(Type implementation)
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
            return implementation.GetInterfaces()
                .Where(x => x.IsGenericTypeDefinition && x.GetGenericArguments().SequenceEqual(implementation.GetGenericArguments()));
        }

        #endregion
    }
}
