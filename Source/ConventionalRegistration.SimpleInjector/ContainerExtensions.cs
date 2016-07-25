using System;
using ConventionalRegistration.Configuration;
using ConventionalRegistration.Diagnostics;
using ConventionalRegistration.TypeFiltering;
using SimpleInjector;

namespace ConventionalRegistration.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static void RegistrationByConvention(this Container container, Action<IConventionBuilderSyntax<Lifestyle, Container>> convention)
        {
            RegistrationByConvention(container, null, convention);
        }

        public static void RegistrationByConvention(this Container container, 
            BehaviorConfiguration configuration, 
            Action<IConventionBuilderSyntax<Lifestyle, Container>> convention)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }

            var logger = new DiagnosticLogger(configuration);
            var serviceMappingTracker = new ServiceMappingTracker(logger);
            var implementationFilter = new ImplementationFilter();
            var adapter = new SimpleInjectorContainerAdapter(container, serviceMappingTracker, implementationFilter);
            var configServiceFilter = ((IBehaviorConfiguration)configuration).GetServiceFilter();

            var asmSelector = new AssemblySelector();
            var serviceExtractor = new ServiceExtractor();
            var containerDecorator = new ContainerLogger<Lifestyle>(adapter, logger);
            var serviceFilterAggregator = new ServiceFilterAggregator(new IServiceFilter[] { configServiceFilter, implementationFilter, serviceExtractor, serviceMappingTracker });
            using (var builder = new ConventionBuilder<Lifestyle>(containerDecorator, 
                asmSelector,
                serviceFilterAggregator,
                serviceExtractor))
            {
                var importer = new ConventionBuilderSyntaxDecorator<Lifestyle, Container>(builder, container, implementationFilter, asmSelector);
                convention(importer);
            }
        }

        public static void ExplicitRegistration(this IConventionBuilder<Lifestyle> builder, Action<ISimpleInjectorContainerAdapter> explicitRegistrations)
        {
            ISimpleInjectorContainerAdapter container = GetContainerAdapter(builder);
            explicitRegistrations(container);
        }

        private static ISimpleInjectorContainerAdapter GetContainerAdapter(IConventionBuilder<Lifestyle> builder)
        {
            var container = builder.ContainerAdapter as ISimpleInjectorContainerAdapter;
            if (container == null)
            {
                var decorator = builder.ContainerAdapter as IDecorator;
                if (decorator == null)
                {
                    throw new Exception("ConventionBuilder with SimpleInjectorContainerAdapter was expected");
                }
                container = decorator.Decoratee as ISimpleInjectorContainerAdapter;
                if (container == null)
                {
                    throw new Exception("Not a compatible convention builder");
                    //todo: replace with appropriate exception
                }
            }

            return container;
        }
    }
}
