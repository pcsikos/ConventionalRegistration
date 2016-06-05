using Flubar.TypeFiltering;
using System;
using System.Collections.Generic;

namespace Flubar
{
    public class ExtendedConventionBuilder<TLifetime> : ConventionBuilder<TLifetime>
        where TLifetime : class
    {
        private readonly IServiceExtractor serviceExtractor;

        public ExtendedConventionBuilder(IContainer<TLifetime> container,
            AssemblySelector assemblySelector,
            RegistrationEntryValidator registrationEntryValidator,
            IServiceExtractor serviceExtractor) 
            : base(container, assemblySelector, registrationEntryValidator)
        {
            this.serviceExtractor = serviceExtractor;
        }

        public void RegisterAsCollection(Type serviceType)
        {
            SearchForImplementations(serviceType, types =>
            {
                Container.RegisterMultipleImplementations(serviceType, types);
            });
        }

        protected override IEnumerable<Type> FilterServices(IEnumerable<Type> services, Type implementationType)
        {
            return serviceExtractor.RegisterMapping(services, implementationType);
        }

        protected override void ApplyConventions()
        {
            base.ApplyConventions();
            serviceExtractor.Resolve();
        }

        private void SearchForImplementations(Type serviceType, Action<IEnumerable<Type>> callback)
        {
            serviceExtractor.RegisterMonitoredType(serviceType, callback);
        }
    }
}
