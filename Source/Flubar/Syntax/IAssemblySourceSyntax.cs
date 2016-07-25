using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConventionalRegistration.Syntax
{
    /// <summary>
    /// Syntax to select assemblies.
    /// </summary>
    public interface IAssemblySourceSyntax
    {
        ISelectSyntax From(IEnumerable<Assembly> assemblies);
        ISelectSyntax From(IEnumerable<string> assemblies);
        ISelectSyntax From(params Assembly[] assemblies);
        ISelectSyntax From(params string[] assemblies);
        ISelectSyntax FromThisAssembly();
        ISelectSyntax FromAssemblyContaining<T>();
        ISelectSyntax FromAssemblyContaining(params Type[] types);
        ISelectSyntax FromAssemblyContaining(IEnumerable<Type> types);
        ISelectSyntax FromAssembliesMatching(params string[] regexPatterns);
    }
}
