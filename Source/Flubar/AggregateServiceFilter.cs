using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar
{
    class AggregateServiceFilter : IServiceFilter
    {
        readonly IEnumerable<IServiceFilter> serviceFilters;

        public AggregateServiceFilter(IEnumerable<IServiceFilter> serviceFilters)
        {
            this.serviceFilters = serviceFilters;
        }

        public bool IsServiceExcluded(Type serviceType)
        {
            return serviceFilters.Any(x => x.IsServiceExcluded(serviceType));
        }
    }
}
