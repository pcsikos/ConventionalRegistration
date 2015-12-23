using Flubar.Syntax;

namespace Flubar
{
    internal class LifetimeSelector<TLifetime> : ILifetimeSyntax<TLifetime>
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> container;
        
        public LifetimeSelector(IContainer<TLifetime> container)
        {
            this.container = container;
        }

        #region ILifetimeSyntax<TLifetime> Members

        public TLifetime Singleton
        {
            get { return container.GetSingletonLifetime(); }
        }

        public TLifetime Transient
        {
            get { return container.GetDefaultLifetime(); }
        }

        #endregion
    }
}
