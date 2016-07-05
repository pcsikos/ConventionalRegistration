using System;
using System.Collections.Generic;

namespace Flubar.Syntax
{
    public interface IFilterSyntax : IIncludeSyntax, IExcludeSyntax, IWhereSyntax, IEnumerable<Type>
    {
    }
}
