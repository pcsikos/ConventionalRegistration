using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IBehaviorConfiguration
    {
        IEnumerable<Type> ExcludedServices { get; }
        IEnumerable<Type> ExcludedBaseTypes { get; }
    }
}
