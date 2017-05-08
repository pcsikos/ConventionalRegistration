using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConventionalRegistration.Syntax;

namespace ConventionalRegistration.TypeFiltering
{
    /// <summary>
    /// Represents a collection of <see cref="Type"/> and provides methods to manipulate with it.
    /// </summary>
    public class TypeSelector : ITypeSelector
    {
        private IEnumerable<Type> filteredTypes;

        public TypeSelector(IEnumerable<Type> types)
        {
            Check.NotNull(types, "types");
            filteredTypes = types;
        }

        #region IIncludeSyntax Members

        public ITypeSelector Including<T>()
        {
            return Including(typeof(T));
        }

        public ITypeSelector Including(IEnumerable<Type> types)
        {
            filteredTypes = filteredTypes.Concat(types);
            return this;
        }

        public ITypeSelector Including(params Type[] types)
        {
            return Including((IEnumerable<Type>)types);
        }

        #endregion

        #region IExcludeSyntax Members

        public ITypeSelector Excluding<T>()
        {
            return Excluding(typeof(T));
        }

        public ITypeSelector Excluding(IEnumerable<Type> types)
        {
            Check.NotNull(types, nameof(types));
            filteredTypes = filteredTypes.Where(t => !types.Contains(t));
            return this;
        }

        public ITypeSelector Excluding(params Type[] types)
        {
            return Excluding((IEnumerable<Type>)types);
        }

        public ITypeSelector ExcludingGenericTypes()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWhereSyntax Members

        public ITypeSelector WithoutAttribute<T>() where T : Attribute
        {
            return WithoutAttribute(typeof(T));
        }

        public ITypeSelector WithoutAttribute(Type attributeType)
        {
            Check.NotNull(attributeType, nameof(attributeType));
            return Where(t => !t.GetCustomAttributes(attributeType, true).Any());
        }

        public ITypeSelector WithAttribute<T>() where T : Attribute
        {
            return WithAttribute(typeof(T));
        }

        public ITypeSelector WithAttribute(Type attributeType)
        {
            Check.NotNull(attributeType, nameof(attributeType));
            return Where(t => t.GetCustomAttributes(attributeType, true).Any());
        }

        public ITypeSelector Where(Func<Type, bool> filter)
        {
            Check.NotNull(filter, nameof(filter));
            filteredTypes = filteredTypes.Where(filter);
            return this;
        }

        public ITypeSelector IsImplementing<T>()
        {
            filteredTypes = filteredTypes.Where(x => typeof(T).IsAssignableFrom(typeof(T)));
            return this;
        }

        public ITypeSelector IsImplementingGenericTypes(params Type[] genericTypeDefinitions)
        {
            Check.NotNull(genericTypeDefinitions, nameof(genericTypeDefinitions));
            filteredTypes = filteredTypes.Where(x => x.GetInterfaces().Any(i => i.IsGenericType && genericTypeDefinitions.Any(genericType => i.GetGenericTypeDefinition() == genericType)));
            return this;
        }

        public ITypeSelector IsNotImplementingGenericTypes(params Type[] genericTypeDefinitions)
        {
            Check.NotNull(genericTypeDefinitions, nameof(genericTypeDefinitions));
            filteredTypes = filteredTypes.Where(x => !x.GetInterfaces().Any(i => i.IsGenericType && genericTypeDefinitions.Any(genericType => i.GetGenericTypeDefinition() == genericType)));
            return this;
        }

        #endregion



        #region ISelectSyntax Members

        public ITypeSelector Select(Func<Type, bool> filter)
        {
            filteredTypes = filteredTypes.Where(filter);
            return this;
        }

        public ITypeSelector SelectAllClasses()
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
