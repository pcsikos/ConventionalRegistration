using System;
using System.Collections.Generic;
using Flubar.TypeFiltering;

namespace Flubar
{
    /// <summary>
    /// Provides methods to extract specific services from registration.
    /// </summary>
    public interface IServiceExtractor
    {
        void RegisterMonitoredType(Type serviceType);
        IEnumerable<ServiceImplementation> GetServiceImplementations();
    }
}