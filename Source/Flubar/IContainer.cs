using System;
using System.Collections.Generic;

namespace Flubar
{
    /// <summary>
    /// Provides basic functionality to register implementations to services using an IoC container.
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
