using System;
using System.Collections.Generic;

namespace ConventionalRegistration
{
    /// <summary>
    /// Provides methods to register implementations to services over an IoC container as an adapter.
    /// </summary>
    /// <typeparam name="TContainerLifetime"></typeparam>
    public interface IContainerAdapter<TContainerLifetime>
        where TContainerLifetime : class
    {
        void RegisterService(Type serviceType, Type implementation, TContainerLifetime lifetime = null);
        void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, TContainerLifetime lifetime = null);
        void RegisterMultipleImplementations(Type serviceType, IEnumerable<Type> implementations);
        TContainerLifetime GetSingletonLifetime();
        TContainerLifetime GetDefaultLifetime();
    }
}
