using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface ITypeExclusion
    {
        ITypeExclusion Exclude(IRegistrationEntry registration);
        ITypeExclusion Exclude(IEnumerable<IRegistrationEntry> registrations);
        ITypeExclusion Exclude(Type serviceType, Type implementationType);
        ITypeExclusion Exclude(IEnumerable<Type> serviceTypes, Type implementationType);
        ITypeExclusion Exclude(Type implementationType);
    }
}
