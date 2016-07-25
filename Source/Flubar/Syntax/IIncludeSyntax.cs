using System;
using System.Collections.Generic;

namespace ConventionalRegistration.Syntax
{
    /// <summary>
    /// Syntax to explicitly include types.
    /// </summary>
    public interface IIncludeSyntax
    {
        ITypeSelector Including<T>();
        ITypeSelector Including(IEnumerable<Type> types);
        ITypeSelector Including(params Type[] types);
    }
}
