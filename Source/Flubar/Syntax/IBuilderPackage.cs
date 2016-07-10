using Flubar.TypeFiltering;

namespace Flubar.Syntax
{
    public interface IConventionBuilderPackage<TContainer, TLifetime>
        where TLifetime : class
    {
        void RegisterByConvention(IConventionBuilderSyntax<TLifetime, TContainer> builder);
    }
}
