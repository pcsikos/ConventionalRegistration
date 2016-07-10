using Flubar.Syntax;
using System;

namespace Flubar
{
    public interface IPackageImporter<TLifetime, TContainer> 
        where TLifetime : class
    {
        void ImportPackages(params IConventionBuilderPackage<TContainer, TLifetime>[] packages);
        void ImportPackages(Func<ISourceSyntax, ISelectSyntax> assemblySelector);
    }
}