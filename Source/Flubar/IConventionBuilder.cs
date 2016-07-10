using System;
using Flubar.Syntax;

namespace Flubar
{
    /// <summary>
    /// Provides methods to define conventions to map services to implementations.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    public interface IConventionBuilder<TLifetime> where TLifetime : class
    {
        IContainerAdapter<TLifetime> ContainerAdapter { get; }

        IConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention);
        IConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null);

        void RegisterAsCollection(Type serviceType);
    }
}