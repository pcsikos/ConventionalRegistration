using Flubar.Syntax;
using System;

namespace Flubar
{
    /// <summary>
    /// Provides methods to register conventions as a package or a module.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    /// <typeparam name="TContainer"></typeparam>
    public interface IPackageImporter<TLifetime, TContainer> 
        where TLifetime : class
    {
        void ImportPackages(params IConventionBuilderPackage<TContainer, TLifetime>[] packages);
        void ImportPackages(Func<ISourceSyntax, ISelectSyntax> assemblySelector);
    }
}