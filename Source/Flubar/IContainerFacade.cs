using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IContainerFacade<TContainerLifetime>
        where TContainerLifetime : class
    {
        void Register(Type serviceType, Type implementation, TContainerLifetime lifetime = null);
        void Register(IEnumerable<Type> serviceTypes, Type implementation, TContainerLifetime lifetime = null);
        TContainerLifetime GetSingletonLifetime();
        TContainerLifetime GetDefaultLifetime();
        object InnerContainer { get; }
    }
}
