using System;
using System.Linq;

namespace Flubar.RegistrationProducers
{
    public abstract class AbstractRegistrationProducer : IRegistrationProducer, IConfigurable
    {
        private readonly IRegistrationServiceSelector implementationServiceSelector;
        private IServiceFilter serviceFilter;

        protected AbstractRegistrationProducer(IRegistrationServiceSelector implementationServiceSelector)
        {
            this.implementationServiceSelector = implementationServiceSelector;
            serviceFilter = new NullServiceFilter();
        }

        IServiceFilter IConfigurable.ServiceFilter
        {
            get
            {
                return serviceFilter;
            }

            set
            {
                if (value != null)
                {
                    serviceFilter = value;
                }
            }
        }

        protected abstract bool IsApplicable(Type service, Type implementation);

        protected virtual bool IsServiceAllowed(Type service)
        {
            return !serviceFilter.IsServiceExcluded(service);
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
