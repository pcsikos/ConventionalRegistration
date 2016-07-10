using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flubar.Syntax;
using Flubar.TypeFiltering;

namespace Flubar
{
    public class ConventionBuilderImportDecorator<TLifetime, TContainer> : IConventionBuilder<TLifetime>, IPackageImporter<TLifetime, TContainer>
        where TLifetime : class
    {
        readonly IConventionBuilder<TLifetime> conventionBuilder;
        readonly TContainer container;
        readonly IImplementationFilter implementationFilter;
        readonly ISourceSyntax sourceSyntax;

        public ConventionBuilderImportDecorator(IConventionBuilder<TLifetime> conventionBuilder,
            TContainer container,
            IImplementationFilter implementationFilter,
            ISourceSyntax sourceSyntax)
        {
            this.sourceSyntax = sourceSyntax;
            this.implementationFilter = implementationFilter;
            this.container = container;
            this.conventionBuilder = conventionBuilder;
        }

        public IContainer<TLifetime> Container
        {
            get
            {
                return conventionBuilder.Container;
            }
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            return conventionBuilder.Define(convention);
        }

        public ConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null)
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
                package.RegisterByConvention(container, this, implementationFilter);
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
    }
}
