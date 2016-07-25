using System;
using System.Collections.Generic;

namespace ConventionalRegistration.Syntax
{
    /// <summary>
    /// Provides methods to manipulate the collection of types.
    /// </summary>
    public interface ITypeSelector : IIncludeSyntax, IExcludeSyntax, ISelectSyntax, IEnumerable<Type>
    {
    }
}
