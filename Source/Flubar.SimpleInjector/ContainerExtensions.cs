using System;
using Flubar.Configuration;
using Flubar.Diagnostics;
using SimpleInjector;
using Flubar.TypeFiltering;
using Flubar.Syntax;

namespace Flubar.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static void RegistrationByConvention(this Container container, Action<IConventionBuilder<Lifestyle>> convention)
        {
            RegistrationByConvention(container, null, convention);
        }

        public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, Action<IConventionBuilder<Lifestyle>> convention)
        {
            RegistrationByConvention(container, configuration, (builder, filter) => convention(builder));
        }

        public static void RegistrationByConvention(this Container container, 
            BehaviorConfiguration configuration, 
            Action<IConventionBuilder<Lifestyle>, IImplementationFilter> convention)
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
                var importer = new ConventionBuilderImportDecorator<Lifestyle, Container>(builder, container, implementationFilter, asmSelector);
                convention(importer, implementationFilter);
            }
        }

        public static void ExplicitRegistration(this IConventionBuilder<Lifestyle> builder, Action<ISimpleInjectorContainerAdapter> explicitRegistrations)
        {
            ISimpleInjectorContainerAdapter container = GetContainerAdapter(builder);
            explicitRegistrations(container);
        }

        public static void ImportPackages<TLifetime>(this IConventionBuilder<TLifetime> builder,
           params IConventionBuilderPackage<Container, TLifetime>[] packages)
            where TLifetime : class
        {
            IPackageImporter<TLifetime, Container> importer = GetPackageImporter(builder);
            importer.ImportPackages(packages);
        }

        public static void ImportPackages<TLifetime>(this IConventionBuilder<TLifetime> builder,
            Func<ISourceSyntax, ISelectSyntax> assemblySelector)
            where TLifetime : class
        {
            IPackageImporter<TLifetime, Container> importer = GetPackageImporter(builder);
            importer.ImportPackages(assemblySelector);
        }

        private static IPackageImporter<TLifetime, Container> GetPackageImporter<TLifetime>(IConventionBuilder<TLifetime> builder) where TLifetime : class
        {
            var importer = builder as IPackageImporter<TLifetime, Container>;
            if (importer == null)
            {
                throw new Exception("Package import not supported");
            }
            return importer;
        }

        private static ISimpleInjectorContainerAdapter GetContainerAdapter(IConventionBuilder<Lifestyle> builder)
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

            return container;
        }
    }
}
