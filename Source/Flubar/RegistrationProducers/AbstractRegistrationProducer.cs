using System;
using System.Linq;

namespace ConventionalRegistration.RegistrationProducers
{
    public abstract class AbstractRegistrationProducer : IRegistrationProducer
    {
        private readonly IRegistrationServiceSelector implementationServiceSelector;

        protected AbstractRegistrationProducer(IRegistrationServiceSelector implementationServiceSelector)
        {
            Check.NotNull(implementationServiceSelector, nameof(implementationServiceSelector));
            this.implementationServiceSelector = implementationServiceSelector;
        }

        protected abstract bool IsApplicable(Type service, Type implementation);

        protected virtual bool IsServiceAllowed(Type service)
        {
            return true;
        }

        #region IImplementationRegistrationProducer Members

        public IRegistrationEntry CreateRegistrationEntry(Type implementation)
        {
            var interfaces = implementationServiceSelector.GetServicesFrom(implementation).Where(face => IsServiceAllowed(face) && IsApplicable(face, implementation));
            if (interfaces.Any())
            {
                return new RegistrationEntry(implementation, interfaces);
            }
            return null;
        }

        #endregion
    }
}
