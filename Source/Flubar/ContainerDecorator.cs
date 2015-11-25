using System;
using System.Collections.Generic;

namespace Flubar
{
    class ContainerDecorator<TLifetime> : IContainerFacade<TLifetime>
        where TLifetime : class
    {
        private readonly IContainerFacade<TLifetime> decoratedContainer;

        private readonly Action<Type> implementationLogging;

        public ContainerDecorator(IContainerFacade<TLifetime> container, Action<Type> implementationLogging)
        {
            this.decoratedContainer = container;
            this.implementationLogging = implementationLogging;
        }

        #region IContainerFacade<TLifetime> Members

        public void Register(Type serviceType, Type implementation, TLifetime lifetime)
        {
            decoratedContainer.Register(serviceType, implementation, lifetime);
            implementationLogging(implementation);
        }

        public void Register(IEnumerable<Type> serviceTypes, Type implementation, TLifetime lifetime)
        {
            decoratedContainer.Register(serviceTypes, implementation, lifetime);
            implementationLogging(implementation);
        }

        public void Register<TService>(Func<TService> instanceCreator, TLifetime lifetime = null) where TService : class
        {
            decoratedContainer.Register(instanceCreator, lifetime);
            implementationLogging(typeof(TService));
        }

        public TLifetime GetSingletonLifetime()
        {
            return decoratedContainer.GetDefaultLifetime();
        }

        public TLifetime GetDefaultLifetime()
        {
            return decoratedContainer.GetDefaultLifetime();
        }

        public object InnerContainer
        {
            get { return decoratedContainer.InnerContainer; }
        }

        #endregion
    }
}
