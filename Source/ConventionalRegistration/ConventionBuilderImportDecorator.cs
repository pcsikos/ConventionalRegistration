using System;
using System.Linq;
using ConventionalRegistration.Syntax;
using ConventionalRegistration.TypeFiltering;

namespace ConventionalRegistration
{
    /// <summary>
    /// Provides a decorator over <see cref="IConventionBuilder{TLifetime}"/> to expose builder syntax in one place.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    /// <typeparam name="TContainer"></typeparam>
    public class ConventionBuilderSyntaxDecorator<TLifetime, TContainer> : IConventionBuilderSyntax<TLifetime, TContainer>
        where TLifetime : class
    {
        readonly IConventionBuilder<TLifetime> conventionBuilder;
        readonly TContainer container;
        readonly IImplementationFilter implementationFilter;
        readonly ISourceSyntax sourceSyntax;

        public ConventionBuilderSyntaxDecorator(IConventionBuilder<TLifetime> conventionBuilder,
            TContainer container,
            IImplementationFilter implementationFilter,
            ISourceSyntax sourceSyntax)
        {
            this.sourceSyntax = sourceSyntax;
            this.implementationFilter = implementationFilter;
            this.container = container;
            this.conventionBuilder = conventionBuilder;
        }

        public IContainerAdapter<TLifetime> ContainerAdapter
        {
            get
            {
                return conventionBuilder.ContainerAdapter;
            }
        }

        public TContainer Container
        {
            get
            {
                return container;
            }
        }

        public IConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            return conventionBuilder.Define(convention);
        }

        public IConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null)
        {
            return conventionBuilder.Define(rules, lifetimeSelection);
        }

        public void RegisterAsCollection(Type serviceType)
        {
            conventionBuilder.RegisterAsCollection(serviceType);
        }

        public void ImportPackages(params IConventionBuilderPackage<TContainer, TLifetime>[] packages)
        {
            foreach (var package in packages)
            {
                package.RegisterByConvention(this);
            }
        }

        public void ImportPackages(Func<ISourceSyntax, ISelectSyntax> assemblySelector)
        {
            var packages = assemblySelector(sourceSyntax)
                .SelectAllClasses()
                .IsImplementing<IConventionBuilderPackage<TContainer, TLifetime>>()
                .Select(type => Activator.CreateInstance<IConventionBuilderPackage<TContainer, TLifetime>>())
                .ToArray();
            ImportPackages(packages);
        }

        public void ExcludeImplementation(Type implementation)
        {
            implementationFilter.ExcludeImplementation(implementation);
        }
    }
}
