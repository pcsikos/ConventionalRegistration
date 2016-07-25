using System;

namespace ConventionalRegistration.Syntax
{
    public static class SelectSyntaxExtensions
    {
        public static ITypeSelector InNamespaceOf<T>(this ISelectSyntax syntax)
        {
            throw new NotImplementedException();
        }

        //IFilterSyntax InNamespaceOf(params Type[] types);
        //IFilterSyntax InNamespaces(IEnumerable<string> namespaces);
        //IFilterSyntax InNamespaces(params string[] namespaces);
        //IFilterSyntax InNamespaceStartingWith(string namespaceFragment);
        //IFilterSyntax NotInNamespaceOf<T>();
        //IFilterSyntax NotInNamespaceOf(params Type[] types);
        //IFilterSyntax NotInNamespaces(IEnumerable<string> namespaces);
        //IFilterSyntax NotInNamespaces(params string[] namespaces);
        //IFilterSyntax NotInNamespacesContaining(params string[] namespaces);
        //IFilterSyntax NotInNamespaceStartingWith(string namespaceFragment);

    }
}
