using Flubar.RegistrationProducers;
using Flubar.Syntax;
using System;
using System.Collections.Generic;

namespace Flubar
{
    /// <summary>
    /// Provides a set of extension method over <see cref="IEnumerable{Type}"/>
    /// </summary>
    public static class TypeExtensions
    {
        public static IRegisterSyntax UsingSelfRegistrationStrategy(this IEnumerable<Type> types)
        {
            return new StrategySelector(types).UsingSelfRegistrationStrategy();
        }

        public static IRegisterSyntax UsingSingleInterfaceStrategy(this IEnumerable<Type> types)
        {
            return new StrategySelector(types).UsingSingleInterfaceStrategy();
        }

        public static IRegisterSyntax UsingSingleInterfaceStrategy(this IEnumerable<Type> types, IEnumerable<Type> excluding)
        {
            return new StrategySelector(types).UsingSingleInterfaceStrategy(excluding);
        }

        public static IRegisterSyntax UsingDefaultInterfaceStrategy(this IEnumerable<Type> types)
        {
            return new StrategySelector(types).UsingDefaultInterfaceStrategy();
        }

        public static IRegisterSyntax UsingAllInterfacesStrategy(this IEnumerable<Type> types)
        {
            return new StrategySelector(types).UsingAllInterfacesStrategy();
        }

        public static IRegisterSyntax UsingAllInterfacesStrategy(this IEnumerable<Type> types, IEnumerable<Type> excluding)
        {
            return new StrategySelector(types).UsingAllInterfacesStrategy(excluding);
        }

        public static IRegisterSyntax UsingStrategy(this IEnumerable<Type> types, IRegistrationProducer registrationProducer)
        {
            return new StrategySelector(types).UsingStrategy(registrationProducer);
        }
    }
}
