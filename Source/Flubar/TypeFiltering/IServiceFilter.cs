using System;
using System.Collections.Generic;

namespace Flubar.TypeFiltering
{
    /// <summary>
    /// Provides methods to exclude service which fulfill the implementation's conditions.
    /// </summary>
    public interface IServiceFilter
    {
        IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services);
    }
}