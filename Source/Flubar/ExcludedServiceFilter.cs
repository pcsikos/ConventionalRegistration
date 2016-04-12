using Flubar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar
{
    class ExcludedServiceFilter : IServiceFilter
    {
        readonly IEnumerable<Type> excludedServices;
        readonly Func<Type, bool> serviceExclusionfilter;

        public ExcludedServiceFilter(IEnumerable<Type> excludedServices, Func<Type, bool> serviceExclusionfilter)
        {
            Check.NotNull(excludedServices, nameof(excludedServices));
            Check.NotNull(serviceExclusionfilter, nameof(serviceExclusionfilter));
            this.serviceExclusionfilter = serviceExclusionfilter;
            this.excludedServices = excludedServices;
        }

        public bool IsServiceExcluded(Type serviceType)
        {
            return serviceExclusionfilter(serviceType) || excludedServices.Contains(serviceType);
        }
    }
}
