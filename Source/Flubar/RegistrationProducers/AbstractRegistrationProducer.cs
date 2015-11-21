using System;
using System.Linq;

namespace Flubar.RegistrationProducers
{
    public abstract class AbstractRegistrationProducer : IRegistrationProducer
    {
        //private readonly Func<Type, Type, bool> typeFilter;
        private readonly IRegistrationServiceSelector implementationServiceSelector;

        protected AbstractRegistrationProducer(IRegistrationServiceSelector implementationServiceSelector)
        {
            this.implementationServiceSelector = implementationServiceSelector;
        }

        protected abstract bool IsApplicable(Type service, Type implementation);

        #region IImplementationRegistrationProducer Members

        public IRegistrationEntry CreateRegistrationEntry(Type implementation)
        {
            var interfaces = implementationServiceSelector.GetServicesFrom(implementation).Where(face => IsApplicable(face, implementation));
            if (interfaces.Any())
            {
                return new RegistrationEntry(implementation, interfaces);
            }
            return null;
        }

        #endregion
    }
}
