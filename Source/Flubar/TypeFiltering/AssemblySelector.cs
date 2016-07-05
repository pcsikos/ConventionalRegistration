﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Flubar.Syntax;

namespace Flubar.TypeFiltering
{
    public class AssemblySelector : ISourceSyntax
    {
        #region IFromSyntax Members

        public ISelectSyntax From(IEnumerable<Assembly> assemblies)
        {
            return GetTypeSelector(assemblies);
        }

        public ISelectSyntax From(IEnumerable<string> assemblies)
        {
            return From(GetAssemblies().Where(asm => assemblies.Contains(asm.FullName)));
        }

        public ISelectSyntax From(params Assembly[] assemblies)
        {
            return From((IEnumerable<Assembly>)assemblies);
        }

        public ISelectSyntax From(params string[] assemblies)
        {
            return From((IEnumerable<string>)assemblies);
        }

        public ISelectSyntax FromThisAssembly()
        {
            return From(Assembly.GetCallingAssembly());
        }

        public ISelectSyntax FromAssemblyContaining<T>()
        {
            return From(typeof(T).Assembly);
        }

        public ISelectSyntax FromAssemblyContaining(params Type[] types)
        {
            return FromAssemblyContaining((IEnumerable<Type>)types);
        }

        public ISelectSyntax FromAssemblyContaining(IEnumerable<Type> types)
        {
            return From(types.Select(x => x.Assembly));
        }

        public ISelectSyntax FromAssembliesMatching(params string[] regexPatterns)
        {
            return From(GetAssemblies().Where(asm => regexPatterns.Any(pattern => System.Text.RegularExpressions.Regex.IsMatch(asm.FullName, pattern))));
        }

        #endregion

        private Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        private ISelectSyntax GetTypeSelector(IEnumerable<Assembly> assemblies)
        {
            return new TypeSelector(assemblies.SelectMany(x => x.GetExportedTypes()));
        }
    }
}
