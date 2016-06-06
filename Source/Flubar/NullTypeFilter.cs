using System;

namespace Flubar
{
    public sealed class NullTypeFilter : ITypeFilter
    {
        public bool Contains(Type serviceType) => false;
    }
}
