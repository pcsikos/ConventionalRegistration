﻿using System;
using System.Collections.Generic;

namespace Flubar
{
    /// <summary>
    /// Provides methods to exclude already registered services.
    /// </summary>
    public interface IServiceMappingTracker
    {
        void ExcludeServices(IEnumerable<Type> serviceTypes, Type implementation);
        void ExcludeService(Type serviceType, Type implementation = null);
    }
}