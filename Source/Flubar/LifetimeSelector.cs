using Flubar.Syntax;

namespace Flubar
{
    /// <summary>
    /// Exposes lifetime members of <see cref="IContainerAdapter{TLifetime}" /> as a facade over <see cref="ILifetimeSyntax{TLifetime}"/>.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    internal class LifetimeSelector<TLifetime> : ILifetimeSyntax<TLifetime>
        where TLifetime : class
    {
        private readonly IContainerAdapter<TLifetime> container;
        
        public LifetimeSelector(IContainerAdapter<TLifetime> container)
        {
            this.container = container;
        }

        #region ILifetimeSyntax<TLifetime> Members

        public TLifetime Singleton => container.GetSingletonLifetime();

        public TLifetime Transient => container.GetDefaultLifetime();

        #endregion
    }
}
