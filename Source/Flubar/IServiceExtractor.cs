using System;
using System.Collections.Generic;
using Flubar.TypeFiltering;

namespace Flubar
{
    public interface IServiceExtractor
    {
        void RegisterMonitoredType(Type serviceType);
        IEnumerable<ServiceImplementation> GetServiceImplementations();
    }
}