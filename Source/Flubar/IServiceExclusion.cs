using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IServiceExclusion
    {
        void ExcludeServices(IEnumerable<Type> serviceTypes, Type implementation);
        void ExcludeService(Type serviceType, Type implementation = null);
        void ExcludeRegistration(IRegistrationEntry registration);
    }
}
