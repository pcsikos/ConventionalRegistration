using System;
using System.Collections.Generic;

namespace Flubar.TypeFiltering
{
    //todo: rename
    public interface IServiceFilter
    {
        IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services);
    }
}