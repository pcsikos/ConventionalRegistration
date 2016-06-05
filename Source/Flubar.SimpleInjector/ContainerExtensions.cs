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
            RegistrationByConvention(container, configuration, (builder, filter) => convention(builder));
        }

        public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, Action<ExtendedConventionBuilder<Lifestyle>, IImplementationFilter> convention)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }
            
            var serviceMappingTracker = new ServiceMappingTracker();
            var implementationFilter = new TypeFilter();
            var adapter = new SimpleInjectorContainerAdapter(container, serviceMappingTracker, implementationFilter);
            var serviceFilter = ((IBehaviorConfiguration)configuration).GetServiceFilter();
            var logger = new DiagnosticLogger(configuration);

            var asmSelector = new AssemblySelector(serviceFilter, implementationFilter);
            var registrationEntryValidator = new RegistrationEntryValidator(serviceMappingTracker, logger);
            var serviceExtractor = new ServiceExtractor();
            var containerDecorator = new ContainerLogger<Lifestyle>(adapter, logger);

            using (var builder = new ExtendedConventionBuilder<Lifestyle>(containerDecorator, 
                serviceMappingTracker, 
                asmSelector,
                registrationEntryValidator,
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
