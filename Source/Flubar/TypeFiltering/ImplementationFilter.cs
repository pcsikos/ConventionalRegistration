using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar.TypeFiltering
{
    public class ImplementationFilter : TypeFilter, IImplementationFilter, IServiceFilter
    {
        public void ExcludeImplementation(Type implementation)//, IEnumerable<Type> services = null)
        {
            ExcludeType(implementation);
        }

        public IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services)
        {
            if (Contains(implementation))
            {
                return Enumerable.Empty<Type>();
            }
            return services;
        }
    }
}
