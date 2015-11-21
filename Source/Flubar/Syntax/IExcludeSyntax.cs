using System;
using System.Collections.Generic;

namespace Flubar.Syntax
{
    public interface IExcludeSyntax
    {
        IFilterSyntax Excluding<T>();
        IFilterSyntax Excluding(IEnumerable<Type> types);
        IFilterSyntax Excluding(params Type[] types);
        IFilterSyntax ExcludingGenericTypes();
    }
}
