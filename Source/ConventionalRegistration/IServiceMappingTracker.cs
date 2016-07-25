using System;
using System.Collections.Generic;

namespace ConventionalRegistration
{
    /// <summary>
    /// Provides methods to exclude registered services.
    /// </summary>
    public interface IServiceMappingTracker
    {
        void ExcludeServices(IEnumerable<Type> serviceTypes, Type implementation = null);
        void ExcludeService(Type serviceType, Type implementation = null);
    }
}
