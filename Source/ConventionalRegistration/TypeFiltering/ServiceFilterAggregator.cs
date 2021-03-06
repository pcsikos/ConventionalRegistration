﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ConventionalRegistration.TypeFiltering
{
    /// <summary>
    /// Chains a collection of <see cref="IServiceFilter"/> and hides them behind one <see cref="IServiceFilter"/>.
    /// </summary>
    public class ServiceFilterAggregator : IServiceFilter
    {
        readonly IEnumerable<IServiceFilter> filters;

        public ServiceFilterAggregator(IEnumerable<IServiceFilter> filters)
        {
            Check.NotEmpty(filters, nameof(filters));
            this.filters = filters;
        }

        public IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services)
        {
            Check.NotNull(implementation, nameof(implementation));
            Check.NotNull(services, nameof(services));
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
