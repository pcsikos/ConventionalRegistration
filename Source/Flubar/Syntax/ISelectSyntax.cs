using System;

namespace Flubar.Syntax
{
    /// <summary>
    /// Provides methods to filter the collection of types.
    /// </summary>
    public interface ISelectSyntax
    {
        ITypeSelector WithoutAttribute<T>() where T : Attribute;
        ITypeSelector WithoutAttribute(Type attributeType);
        ITypeSelector WithAttribute<T>() where T : Attribute;
        ITypeSelector WithAttribute(Type attributeType);
        ITypeSelector IsImplementing<T>();
        ITypeSelector IsImplementingGenericType(Type genericTypeDefinition);
        ITypeSelector SelectAllClasses();
        ITypeSelector Select(Func<Type, bool> filter);
    }
}
