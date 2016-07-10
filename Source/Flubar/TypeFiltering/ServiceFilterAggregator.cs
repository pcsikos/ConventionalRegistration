using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar.TypeFiltering
{
    /// <summary>
    /// Chains a collection of <see cref="IServiceFilter"/> and hides them as an <see cref="IServiceFilter"/>.
    /// </summary>
    public class ServiceFilterAggregator : IServiceFilter
    {
        readonly IEnumerable<IServiceFilter> filters;

        public ServiceFilterAggregator(IEnumerable<IServiceFilter> filters)
        {
            this.filters = filters;
        }

        public IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services)
        {
            var allowedServices = services;
            foreach (var filter in filters)
            {
                if (!allowedServices.Any())
                {
                    return allowedServices;
                }
                allowedServices = filter.GetAllowedServices(implementation, allowedServices);
            }
            return allowedServices;
        }
    }
}
