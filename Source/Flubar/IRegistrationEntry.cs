using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IRegistrationEntry
    {
        IEnumerable<Type> ServicesTypes { get; }
        Type ImplementationType { get; }
    }
}
