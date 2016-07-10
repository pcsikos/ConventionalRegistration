using Flubar.TypeFiltering;

namespace Flubar
{
    /// <summary>
    /// Provides methods to define conventions, import packages and exclude implementations.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    /// <typeparam name="TContainer"></typeparam>
    public interface IConventionBuilderSyntax<TLifetime, TContainer> : IConventionBuilder<TLifetime>, IPackageImporter<TLifetime, TContainer>, IImplementationFilter
        where TLifetime : class
    {
        TContainer Container { get; }
    }
}
