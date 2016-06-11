using System;

namespace Flubar.TypeFiltering
{
    public interface ITypeFilter
    {
        bool Contains(Type serviceType);
    }
}
