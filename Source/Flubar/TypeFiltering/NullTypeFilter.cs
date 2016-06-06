using System;

namespace Flubar.TypeFiltering
{
    public sealed class NullTypeFilter : ITypeFilter
    {
        public bool Contains(Type serviceType) => false;
    }
}
