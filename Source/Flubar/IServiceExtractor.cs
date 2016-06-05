using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IServiceExtractor
    {
        void RegisterMonitoredType(Type serviceType);
        IEnumerable<ServiceImplementation> GetServiceImplementations();
    }
}