using System;
using Flubar.Syntax;

namespace Flubar
{
    public interface IConventionBuilder<TLifetime> where TLifetime : class
    {
        IContainer<TLifetime> Container { get; }

        ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention);
        ConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null);

        void RegisterAsCollection(Type serviceType);
    }
}