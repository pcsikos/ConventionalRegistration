using System;
using System.Collections.Generic;
using System.Linq;

namespace ConventionalRegistration.TypeFiltering
{
    /// <summary>
    /// Represents a collection of excluded implementations and implements <see cref="IImplementationFilter"/> and <see cref="IServiceFilter"/> to access them.
    /// </summary>
    public class ImplementationFilter : IImplementationFilter, IServiceFilter
    {
        private readonly ISet<Type> implementations = new HashSet<Type>();

        public void ExcludeImplementation(Type implementation)
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
