using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar.TypeFiltering
{
    public class ImplementationFilter : IImplementationFilter, IServiceFilter
    {
        private readonly ISet<Type> implementations = new HashSet<Type>();

        public void ExcludeImplementation(Type implementation)//, IEnumerable<Type> services = null)
        {
            implementations.Add(implementation);
        }

        public IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services)
        {
            if (implementations.Contains(implementation))
            {
                return Enumerable.Empty<Type>();
            }
            return services;
        }
    }
}
