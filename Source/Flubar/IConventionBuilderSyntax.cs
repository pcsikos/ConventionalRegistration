using Flubar.TypeFiltering;

namespace Flubar
{
    public interface IConventionBuilderSyntax<TLifetime, TContainer> : IConventionBuilder<TLifetime>, IPackageImporter<TLifetime, TContainer>, IImplementationFilter
        where TLifetime : class
    {
        TContainer Container { get; }
    }
}
