using System;
using System.Collections.Generic;

namespace Flubar
{
    /// <summary>
    /// Container adapter
    /// </summary>
    /// <typeparam name="TContainerLifetime"></typeparam>
    public interface IContainer<TContainerLifetime>
        where TContainerLifetime : class
    {
        void Register(Type serviceType, Type implementation, TContainerLifetime lifetime = null);
        void Register(IEnumerable<Type> serviceTypes, Type implementation, TContainerLifetime lifetime = null);
        void Register<TService>(Func<TService> instanceCreator, TContainerLifetime lifetime = null) where TService : class;
        TContainerLifetime GetSingletonLifetime();
        TContainerLifetime GetDefaultLifetime();
        event EventHandler<RegistrationEventArgs> RegistrationCreated;
        event EventHandler<ImplementationExcludedEventArgs> ImplementationExcluded;
    }
}
