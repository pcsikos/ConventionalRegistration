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
            IServiceFilter serviceFilter,
            IServiceExtractor serviceExtractor) 
            : base(container, assemblySelector, serviceFilter)
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
