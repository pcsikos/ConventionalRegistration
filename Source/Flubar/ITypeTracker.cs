﻿using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface ITypeTracker
    {
        bool AddToCustomRegistrationIfApplicable(IEnumerable<Type> services, Type implementationType);
        void RegisterMonitoredType(Type serviceType, Action<IEnumerable<Type>> callback);
        void Resolve();
    }
}