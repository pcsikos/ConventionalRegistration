using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    public interface IServiceExclusion
    {
        void ExcludeServices(IEnumerable<Type> serviceTypes, Type implementation);
        void ExcludeService(Type serviceType, Type implementation);
        void ExcludeRegistration(IRegistrationEntry registration);
    }
}
