using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface ITypeExclusionTracker : IServiceExclusion, IImplementationExclusion
    {
        bool ContainsImplementation(Type implementation);
        IEnumerable<Type> GetImplemetationServices(Type implementation);
        bool ContainsService(Type serviceType);
        Type GetServiceImplementation(Type serviceType);
    }
}
