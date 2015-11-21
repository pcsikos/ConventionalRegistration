using System;
using System.Collections.Generic;

namespace Flubar.RegistrationProducers
{
    public interface IRegistrationServiceSelector
    {
        IEnumerable<Type> GetServicesFrom(Type implementation);
    }
}
