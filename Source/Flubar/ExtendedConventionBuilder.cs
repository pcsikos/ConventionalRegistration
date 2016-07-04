using Flubar.TypeFiltering;
using System;

namespace Flubar
{
    /// <summary>
    /// Extends the basic functionality of <see cref="ConventionBuilder{TLifetime}"/>. Provides a method to register collection of implementation.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
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
            serviceExtractor.RegisterMonitoredType(serviceType);
        }

        protected override void ApplyConventions()
        {
            base.ApplyConventions();
            foreach (var service in serviceExtractor.GetServiceImplementations())
            {
                Container.RegisterMultipleImplementations(service.ServiceType, service.GetImplementations());
            }
        }
    }
}
