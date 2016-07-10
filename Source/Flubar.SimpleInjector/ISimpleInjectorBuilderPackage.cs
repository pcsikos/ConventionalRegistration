using Flubar;
using Flubar.Syntax;
using SimpleInjector;

namespace Flubar.SimpleInjector
{
    public interface ISimpleInjectorBuilderPackage : IConventionBuilderPackage<Container, ConventionBuilder<Lifestyle>>
    {
    }
}
