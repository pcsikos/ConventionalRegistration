using System;

namespace ConventionalRegistration.RegistrationProducers
{
    public class MultipleInterfaceRegistrationProducer : AbstractRegistrationProducer
    {
        public MultipleInterfaceRegistrationProducer(IRegistrationServiceSelector registrationServiceSelector)
            : base(registrationServiceSelector)
        {
        }

        protected override bool IsApplicable(Type service, Type implementation)
        {
            return true;
        }
    }
}
