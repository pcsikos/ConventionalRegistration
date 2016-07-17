using System;
using System.Collections.Generic;

namespace Flubar.Syntax
{
    /// <summary>
    /// Provides methods to manipulate the collection of types.
    /// </summary>
    public interface ITypeSelector : IIncludeSyntax, IExcludeSyntax, ISelectSyntax, IEnumerable<Type>
    {
    }
}
