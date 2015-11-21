using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IContainerRegistration
    {
        IEnumerable<Type> GetRegisteredServices();
    }
}
