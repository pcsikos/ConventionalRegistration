using System;

namespace Flubar.Syntax
{
    public interface ISelectSyntax
    {
        IFilterSyntax Select(Func<Type, bool> filter);
        IFilterSyntax SelectAllClasses();
    }
}
