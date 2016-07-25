using System;

namespace ConventionalRegistration.TypeFiltering
{
    /// <summary>
    /// Provides methods to exclude specific implementations.
    /// </summary>
    public interface IImplementationFilter
    {
        void ExcludeImplementation(Type implementation);
    }
}
