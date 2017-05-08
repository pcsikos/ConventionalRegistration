using System;

namespace ConventionalRegistration.Syntax
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
        ITypeSelector IsImplementingGenericTypes(params Type[] genericTypeDefinitions);
        ITypeSelector IsNotImplementingGenericTypes(params Type[] genericTypeDefinitions);
        ITypeSelector SelectAllClasses();
        ITypeSelector Select(Func<Type, bool> filter);
    }
}
