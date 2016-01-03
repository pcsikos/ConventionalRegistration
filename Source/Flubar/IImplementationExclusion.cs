using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IImplementationExclusion
    {
        void ExcludeImplementation(Type implementation, IEnumerable<Type> services = null);
    }
}
