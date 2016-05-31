using System;

namespace Flubar
{
    public interface IServiceFilter
    {
        bool IsServiceExcluded(Type serviceType);
    }
}
