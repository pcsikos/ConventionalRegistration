using System;
using System.Collections.Generic;

namespace Flubar.Syntax
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
