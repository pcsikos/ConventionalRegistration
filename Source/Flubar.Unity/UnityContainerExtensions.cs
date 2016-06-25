using Flubar.Configuration;
using Flubar.Diagnostics;
using Flubar.TypeFiltering;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.Unity
{
    public static class UnityContainerExtensions
    {
        public static void RegistrationByConvention(this UnityContainer container, Action<ConventionBuilder<LifetimeManager>> convention)
        {
            RegistrationByConvention(container, null, convention);
        }

        public static void RegistrationByConvention(this UnityContainer container, BehaviorConfiguration configuration, Action<ExtendedConventionBuilder<LifetimeManager>> convention)
        {
            RegistrationByConvention(container, configuration, (builder, tracker) => convention(builder));
        }

        public static void RegistrationByConvention(this UnityContainer container, BehaviorConfiguration configuration, Action<ExtendedConventionBuilder<LifetimeManager>, IImplementationFilter> convention)//, IEnumerable<Type> serviceExclusions = null)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }

            var logger = new DiagnosticLogger(configuration);
            var serviceMappingTracker = new ServiceMappingTracker(logger);
            var implementationFilter = new ImplementationFilter();
            var typeFilter = ((IBehaviorConfiguration)configuration).GetTypeFilter();

            var adapter = new UnityContainerAdapter(container, serviceMappingTracker);
            var asmSelector = new AssemblySelector(typeFilter);
            var serviceExtractor = new ServiceExtractor();
            var serviceFilter = new ServiceFilterAggregator(new IServiceFilter[] { implementationFilter, serviceExtractor, serviceMappingTracker });


            using (var builder = new ExtendedConventionBuilder<LifetimeManager>(adapter, asmSelector, serviceFilter, serviceExtractor))
            {
                convention(builder, implementationFilter);
            }
        }
    }
}
