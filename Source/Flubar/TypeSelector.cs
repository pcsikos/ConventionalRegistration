﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Flubar.Infrastructure;
using Flubar.RegistrationProducers;
using Flubar.Syntax;

namespace Flubar
{
    class TypeSelector : IFilterSyntax, ISelectSyntax
    {
        private IEnumerable<Type> filteredTypes;
        readonly IServiceFilter serviceFilter;

        public TypeSelector(IEnumerable<Type> types, IServiceFilter serviceFilter)
        {
            Check.NotNull(types, "types");
            filteredTypes = types;
            this.serviceFilter = serviceFilter;
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
            return Including(types);
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

        #region IStrategySyntax Members

        public IRegisterSyntax UsingSelfRegistrationStrategy()
        {
            throw new NotImplementedException();
        }

        public IRegisterSyntax UsingSingleInterfaceStrategy()
        {
            throw new NotImplementedException();
        }

        public IRegisterSyntax UsingSingleInterfaceStrategy(IEnumerable<Type> excluding)
        {
            throw new NotImplementedException();
        }

        public IRegisterSyntax UsingDefaultInterfaceStrategy()
        {
            return UsingStrategy(new DefaultInterfaceRegistrationProducer(new CompatibleServiceLookup()));
        }

        public IRegisterSyntax UsingAllInterfacesStrategy()
        {
            return UsingStrategy(new MultipleInterfaceRegistrationProducer(new CompatibleServiceLookup()));
        }

        public IRegisterSyntax UsingAllInterfacesStrategy(IEnumerable<Type> excluding)
        {
            throw new NotImplementedException();
        }

        public IRegisterSyntax UsingStrategy<T>() where T : IRegistrationProducer, new()
        {
            return UsingStrategy(new T());
        }

        public IRegisterSyntax UsingStrategy(IRegistrationProducer registrationProducer)
        {
            var configurable = registrationProducer as IConfigurable;
            if (configurable != null)
            {
                configurable.ServiceFilter = serviceFilter;
            }

            var registrations = filteredTypes.Select(type => registrationProducer.CreateRegistrationEntry(type)).Where(x => x != null);
            return new RegistrationHandler(registrations, null);
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

        public IFilterSyntax SelectAllInterfaces()
        {
            return Select(x => x.IsInterface);
        }

        #endregion
    }
}
