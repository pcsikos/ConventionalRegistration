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
        void RegisterAll(IEnumerable<Type> serviceTypes, Type implementation, TContainerLifetime lifetime = null);
        void Register<TService>(Func<TService> instanceCreator, TContainerLifetime lifetime = null) where TService : class;
        void Register(Type serviceType, Func<object> instanceCreator, TContainerLifetime lifetime = null);
        TContainerLifetime GetSingletonLifetime();
        TContainerLifetime GetDefaultLifetime();
    }
}
