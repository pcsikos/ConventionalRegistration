using System;

namespace Flubar
{
    public sealed class NullServiceFilter : IServiceFilter
    {
        public bool IsServiceExcluded(Type serviceType)
        {
            return false;
        }
    }
}
