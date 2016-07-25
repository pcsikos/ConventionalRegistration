using System;
using System.Collections.Generic;

namespace ConventionalRegistration.RegistrationProducers
{
    public interface IRegistrationServiceSelector
    {
        IEnumerable<Type> GetServicesFrom(Type implementation);
    }
}
