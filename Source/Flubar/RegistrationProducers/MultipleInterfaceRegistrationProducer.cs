using System;
using Flubar.TypeFiltering;

namespace Flubar.RegistrationProducers
{
    public class MultipleInterfaceRegistrationProducer : AbstractRegistrationProducer
    {
        public MultipleInterfaceRegistrationProducer(IRegistrationServiceSelector registrationServiceSelector,
            ITypeFilter typeFilter)
            : base(registrationServiceSelector, typeFilter)
        {
        }

        protected override bool IsApplicable(Type service, Type implementation)
        {
            return true;
        }
    }
}
