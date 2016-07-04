using System;
using Flubar.TypeFiltering;

namespace Flubar.RegistrationProducers
{
    public class DefaultInterfaceRegistrationProducer : AbstractRegistrationProducer
    {
        public DefaultInterfaceRegistrationProducer(IRegistrationServiceSelector registrationServiceSelector)
            : base(registrationServiceSelector)
        {
        }

        protected override bool IsApplicable(Type service, Type implementation)
        {
            var interfaceName = service.GetInterfaceName();
            var implementationName = implementation.GetImplementationName();

            return interfaceName == implementationName;
        }
    }
}
