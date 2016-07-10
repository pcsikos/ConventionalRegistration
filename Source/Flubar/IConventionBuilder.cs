using System;
using Flubar.Syntax;

namespace Flubar
{
    public interface IConventionBuilder<TLifetime> where TLifetime : class
    {
        IContainerAdapter<TLifetime> ContainerAdapter { get; }

        IConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention);
        IConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null);

        void RegisterAsCollection(Type serviceType);
    }
}