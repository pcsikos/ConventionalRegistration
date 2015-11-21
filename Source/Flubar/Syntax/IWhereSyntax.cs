using System;

namespace Flubar.Syntax
{
    public interface IWhereSyntax
    {
        IFilterSyntax WithoutAttribute<T>() where T : Attribute;
        IFilterSyntax WithoutAttribute(Type attributeType);
        IFilterSyntax WithAttribute<T>() where T : Attribute;
        IFilterSyntax WithAttribute(Type attributeType);
        IFilterSyntax Where(Func<Type, bool> filter);
        IFilterSyntax IsImplementing<T>();
        IFilterSyntax IsImplementingGenericType(Type genericTypeDefinition);
    }
}
