using System;
using System.Collections.Generic;

namespace Flubar
{
    //class ContainerDecorator<TLifetime> : IContainer<TLifetime>
    //    where TLifetime : class
    //{
    //    private readonly IContainer<TLifetime> decoratedContainer;

    //    private readonly Action<IEnumerable<Type>, Type> implementationLogging;

    //    public ContainerDecorator(IContainer<TLifetime> container, Action<IEnumerable<Type>, Type> implementationLogging)
    //    {
    //        this.decoratedContainer = container;
    //        this.implementationLogging = implementationLogging;
    //    }

    //    #region IContainerFacade<TLifetime> Members

    //    public void Register(Type serviceType, Type implementation, TLifetime lifetime)
    //    {
    //        decoratedContainer.Register(serviceType, implementation, lifetime);
    //        implementationLogging(new[] { serviceType }, implementation);
    //    }

    //    public void Register(IEnumerable<Type> serviceTypes, Type implementation, TLifetime lifetime)
    //    {
    //        decoratedContainer.Register(serviceTypes, implementation, lifetime);
    //        implementationLogging(serviceTypes, implementation);
    //    }

    //    public void Register<TService>(Func<TService> instanceCreator, TLifetime lifetime = null) where TService : class
    //    {
    //        decoratedContainer.Register(instanceCreator, lifetime);
    //        implementationLogging(new[] { typeof(TService) }, null);
    //    }

    //    public TLifetime GetSingletonLifetime()
    //    {
    //        return decoratedContainer.GetDefaultLifetime();
    //    }

    //    public TLifetime GetDefaultLifetime()
    //    {
    //        return decoratedContainer.GetDefaultLifetime();
    //    }

    //    //public object InnerContainer
    //    //{
    //    //    get { return decoratedContainer.InnerContainer; }
    //    //}

    //    #endregion
    //}
}
