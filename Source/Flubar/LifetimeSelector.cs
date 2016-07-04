using Flubar.Syntax;

namespace Flubar
{
    /// <summary>
    /// Exposes lifetime members of <see cref="IContainer{TLifetime}" /> using <see cref="ILifetimeSyntax{TLifetime}"/>.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    internal class LifetimeSelector<TLifetime> : ILifetimeSyntax<TLifetime>
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> container;
        
        public LifetimeSelector(IContainer<TLifetime> container)
        {
            this.container = container;
        }

        #region ILifetimeSyntax<TLifetime> Members

        public TLifetime Singleton => container.GetSingletonLifetime();

        public TLifetime Transient => container.GetDefaultLifetime();

        #endregion
    }
}
