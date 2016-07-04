using System;
using System.Collections.Generic;

namespace Flubar
{
    /// <summary>
    /// Represents a registration entry with implementation and related service or services.
    /// </summary>
    public interface IRegistrationEntry
    {
        IEnumerable<Type> ServicesTypes { get; }
        Type ImplementationType { get; }
    }
}
