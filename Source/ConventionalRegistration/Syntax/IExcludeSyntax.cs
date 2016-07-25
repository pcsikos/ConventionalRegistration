using System;
using System.Collections.Generic;

namespace ConventionalRegistration.Syntax
{
    /// <summary>
    /// Syntax to exclude types.
    /// </summary>
    public interface IExcludeSyntax
    {
        ITypeSelector Excluding<T>();
        ITypeSelector Excluding(IEnumerable<Type> types);
        ITypeSelector Excluding(params Type[] types);
        ITypeSelector ExcludingGenericTypes();
    }
}
