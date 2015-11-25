using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.RegistrationProducers
{
    public class AllNonSystemInterfaceRegistrationProducer : AbstractRegistrationProducer
    {
        public AllNonSystemInterfaceRegistrationProducer(IRegistrationServiceSelector registrationServiceSelector)
            : base(registrationServiceSelector)
        {
        }

        protected override bool IsApplicable(Type service, Type implementation)
        {
            return !service.Namespace.StartsWith("System", StringComparison.Ordinal);
        }
    }
}
