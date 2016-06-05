using System;
using SimpleInjector;
using Flubar.TypeFiltering;

namespace Flubar.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static void RegistrationByConvention(this Container container, Action<ExtendedConventionBuilder<Lifestyle>> convention)
        {
            RegistrationByConvention(container, null, convention);
        }

        public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, Action<ExtendedConventionBuilder<Lifestyle>> convention)
        {
            RegistrationByConvention(container, configuration, (builder, tracker) => convention(builder));
        }

        public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, Action<ExtendedConventionBuilder<Lifestyle>, IServiceMappingTracker> convention)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }
            
            var serviceMappingTracker = new ServiceMappingTracker();
            var implementationFilter = new TypeFilter();
            var adapter = new SimpleInjectorContainerAdapter(container, serviceMappingTracker, implementationFilter);
            var serviceFilter = ((IBehaviorConfiguration)configuration).GetServiceFilter();
            var asmSelector = new AssemblySelector(serviceFilter, implementationFilter);
            var logger = new DiagnosticLogger(configuration);
            var serviceExtractor = new ServiceExtractor();

            using (var builder = new ExtendedConventionBuilder<Lifestyle>(adapter, 
                //configuration, 
                serviceMappingTracker, 
                //new ServiceExtractor(), 
                asmSelector,
                logger,
                serviceExtractor))
            {
                convention(builder, serviceMappingTracker);
            }
        }

        public static void ExplicitRegistration(this ConventionBuilder<Lifestyle> builder, Action<ISimpleInjectorContainerAdapter> explicitRegistrations)
        {
            var container = (ISimpleInjectorContainerAdapter)builder.Container;
            explicitRegistrations(container);
        }
    }
}
