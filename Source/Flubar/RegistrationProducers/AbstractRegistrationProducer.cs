using System;
using System.Linq;
using Flubar.TypeFiltering;

namespace Flubar.RegistrationProducers
{
    public abstract class AbstractRegistrationProducer : IRegistrationProducer
    {
        private readonly IRegistrationServiceSelector implementationServiceSelector;
        private ITypeFilter typeFilter;

        protected AbstractRegistrationProducer(IRegistrationServiceSelector implementationServiceSelector,
            ITypeFilter typeFilter)
        {
            this.implementationServiceSelector = implementationServiceSelector;
            this.typeFilter = typeFilter;
        }

        //protected AbstractRegistrationProducer(IRegistrationServiceSelector implementationServiceSelector)
        //    : this(implementationServiceSelector, new NullServiceFilter())
        //{
        //}

        protected abstract bool IsApplicable(Type service, Type implementation);

        protected virtual bool IsServiceAllowed(Type service)
        {
            return !typeFilter.Contains(service);
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
