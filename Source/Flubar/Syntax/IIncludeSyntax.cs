using System;
using System.Collections.Generic;

namespace Flubar.Syntax
{
    public interface IIncludeSyntax
    {
        IFilterSyntax Including<T>();
        IFilterSyntax Including(IEnumerable<Type> types);
        IFilterSyntax Including(params Type[] types);
    }
}
