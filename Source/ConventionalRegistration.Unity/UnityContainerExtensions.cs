using System;
using ConventionalRegistration.Configuration;
using ConventionalRegistration.Diagnostics;
using ConventionalRegistration.TypeFiltering;
using Microsoft.Practices.Unity;

namespace ConventionalRegistration.Unity
{
    public static class UnityContainerExtensions
    {
        public static void RegistrationByConvention(this UnityContainer container, Action<IConventionBuilder<LifetimeManager>> convention)
        {
            RegistrationByConvention(container, null, convention);
        }

        public static void RegistrationByConvention(this UnityContainer container, BehaviorConfiguration configuration, Action<IConventionBuilder<LifetimeManager>> convention)
        {
            RegistrationByConvention(container, configuration, (builder, tracker) => convention(builder));
        }

        public static void RegistrationByConvention(this UnityContainer container, BehaviorConfiguration configuration, Action<IConventionBuilder<LifetimeManager>, IImplementationFilter> convention)//, IEnumerable<Type> serviceExclusions = null)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }

            var logger = new DiagnosticLogger(configuration);
            var serviceMappingTracker = new ServiceMappingTracker(logger);
            var implementationFilter = new ImplementationFilter();
            var configServiceFilter = ((IBehaviorConfiguration)configuration).GetServiceFilter();

            var adapter = new UnityContainerAdapter(container, serviceMappingTracker);
            var asmSelector = new AssemblySelector();
            var serviceExtractor = new ServiceExtractor();
            var serviceFilterAggregator = new ServiceFilterAggregator(new IServiceFilter[] { configServiceFilter, implementationFilter, serviceExtractor, serviceMappingTracker });


            using (var builder = new ConventionBuilder<LifetimeManager>(adapter, asmSelector, serviceFilterAggregator, serviceExtractor))
            {
                convention(builder, implementationFilter);
            }
        }
    }
}
