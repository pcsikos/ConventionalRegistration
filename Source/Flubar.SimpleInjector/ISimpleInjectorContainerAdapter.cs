using SimpleInjector;

namespace Flubar.SimpleInjector
{
    public interface ISimpleInjectorContainerAdapter : IContainer<Lifestyle>, IContainer
    {
    }
}
