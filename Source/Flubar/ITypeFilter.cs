using System;

namespace Flubar
{
    public interface ITypeFilter
    {
        bool Contains(Type serviceType);
    }
}
