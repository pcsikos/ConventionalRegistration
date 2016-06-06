using System;

namespace Flubar
{
    /// <summary>
    /// Particularly used to exclude implementations which should not be used as for example decorators.
    /// </summary>
    public interface IImplementationFilter : ITypeFilter
    {
        void ExcludeImplementation(Type implementation);//, IEnumerable<Type> services = null);
    }
}
