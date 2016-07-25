using SimpleInjector;

namespace ConventionalRegistration.SimpleInjector
{
    public interface ISimpleInjectorContainerAdapter : IContainerAdapter<Lifestyle>, IContainer
    {
    }
}
