using System;
using System.Collections.Generic;
using ConventionalRegistration.TypeFiltering;

namespace ConventionalRegistration
{
    /// <summary>
    /// Provides methods to extract specific services and exclude them from registration.
    /// </summary>
    public interface IServiceExtractor
    {
        void RegisterMonitoredType(Type serviceType);
        IEnumerable<ServiceImplementation> GetServiceImplementations();
    }
}