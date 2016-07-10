using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Flubar.Infrastructure;
using Flubar.Syntax;

namespace Flubar.TypeFiltering
{
    /// <summary>
    /// Represents a collection of <see cref="Type"/> and provides methods to manipulate with it.
    /// </summary>
    class TypeSelector : IFilterSyntax, ISelectSyntax
    {
        private IEnumerable<Type> filteredTypes;

        public TypeSelector(IEnumerable<Type> types)
        {
            Check.NotNull(types, "types");
            filteredTypes = types;
        }

        #region IIncludeSyntax Members

        public IFilterSyntax Including<T>()
        {
            return Including(typeof(T));
        }

        public IFilterSyntax Including(IEnumerable<Type> types)
        {
            filteredTypes = filteredTypes.Concat(types);
            return this;
        }

        public IFilterSyntax Including(params Type[] types)
        {
            return Including((IEnumerable<Type>)types);
        }

        #endregion

        #region IExcludeSyntax Members

        public IFilterSyntax Excluding<T>()
        {
            return Excluding(typeof(T));
        }

        public IFilterSyntax Excluding(IEnumerable<Type> types)
        {
            filteredTypes = filteredTypes.Where(t => !types.Contains(t));
            return this;
        }

        public IFilterSyntax Excluding(params Type[] types)
        {
            return Excluding((IEnumerable<Type>)types);
        }

        public IFilterSyntax ExcludingGenericTypes()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWhereSyntax Members

        public IFilterSyntax WithoutAttribute<T>() where T : Attribute
        {
            return WithoutAttribute(typeof(T));
        }

        public IFilterSyntax WithoutAttribute(Type attributeType)
        {
            return Where(t => !t.GetCustomAttributes(attributeType, true).Any());
        }

        public IFilterSyntax WithAttribute<T>() where T : Attribute
        {
            return WithAttribute(typeof(T));
        }

        public IFilterSyntax WithAttribute(Type attributeType)
        {
            return Where(t => t.GetCustomAttributes(attributeType, true).Any());
        }

        public IFilterSyntax Where(Func<Type, bool> filter)
        {
            filteredTypes = filteredTypes.Where(filter);
            return this;
        }

        public IFilterSyntax IsImplementing<T>()
        {
            filteredTypes = filteredTypes.Where(x => typeof(T).IsAssignableFrom(typeof(T)));
            return this;
        }

        public IFilterSyntax IsImplementingGenericType(Type genericTypeDefinition)
        {
            filteredTypes = filteredTypes.Where(x => x.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericTypeDefinition));
            return this;
        }

        #endregion

       

        #region ISelectSyntax Members

        public IFilterSyntax Select(Func<Type, bool> filter)
        {
            filteredTypes = filteredTypes.Where(filter);
            return this;
        }

        public IFilterSyntax SelectAllClasses()
        {
            return Select(x => x.IsClass && !x.IsAbstract);
        }

        #endregion

        public IEnumerator<Type> GetEnumerator()
        {
            return filteredTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return filteredTypes.GetEnumerator();
        }

    }
}
