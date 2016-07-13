using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Infrastructure;

namespace Flubar.TypeFiltering
{
    public class ImplementationFilter : IImplementationFilter, IServiceFilter
    {
        private readonly ISet<Type> implementations = new HashSet<Type>();

        public void ExcludeImplementation(Type implementation)//, IEnumerable<Type> services = null)
        {
            Check.NotNull(implementation, nameof(implementation));
            implementations.Add(implementation);
        }

        public IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services)
        {
            Check.NotNull(implementation, nameof(implementation));
            Check.NotNull(services, nameof(services));

            if (implementations.Contains(implementation))
            {
                return Enumerable.Empty<Type>();
            }
            return services;
        }
    }
}
