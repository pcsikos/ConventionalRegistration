using System;
using Flubar.Configuration;
using Flubar.Diagnostics;
using SimpleInjector;
using Flubar.TypeFiltering;
using System.Collections.Generic;
using System.Linq;

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
            RegistrationByConvention(container, configuration, (builder, filter) => convention(builder));
        }

        public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, Action<ExtendedConventionBuilder<Lifestyle>, IImplementationFilter> convention)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }

            var logger = new DiagnosticLogger(configuration);
            var serviceMappingTracker = new ServiceMappingTracker(logger);
            var implementationFilter = new ImplementationFilter();
            var adapter = new SimpleInjectorContainerAdapter(container, serviceMappingTracker, implementationFilter);
            var typeFilter = ((IBehaviorConfiguration)configuration).GetTypeFilter();

            var asmSelector = new AssemblySelector(typeFilter);
            var serviceExtractor = new ServiceExtractor();
            var containerDecorator = new ContainerLogger<Lifestyle>(adapter, logger);
            var serviceFilter = new ServiceFilterAggregator(new IServiceFilter[] { implementationFilter, serviceExtractor, serviceMappingTracker });

            using (var builder = new ExtendedConventionBuilder<Lifestyle>(containerDecorator, 
                asmSelector,
                serviceFilter,
                serviceExtractor))
            {
                convention(builder, implementationFilter);
            }
        }

        public static void ExplicitRegistration(this ConventionBuilder<Lifestyle> builder, Action<ISimpleInjectorContainerAdapter> explicitRegistrations)
        {
            var container = builder.Container as ISimpleInjectorContainerAdapter;
            if (container == null)
            {
                var decorator = builder.Container as IDecorator;
                if (decorator == null)
                {
                    throw new Exception();
                }
                container = decorator.Decoratee as ISimpleInjectorContainerAdapter;
                if (container == null)
                {
                    throw new Exception();//todo: replace with appropriate exception
                }
            }
            explicitRegistrations(container);
        }
    }
}
