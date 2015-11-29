using System;
using System.Collections.Generic;
using Flubar.RegistrationProducers;

namespace Flubar.Syntax
{
    public interface IStrategySyntax
    {
        IRegisterSyntax UsingSelfRegistrationStrategy();
        IRegisterSyntax UsingSingleInterfaceStrategy();
        IRegisterSyntax UsingSingleInterfaceStrategy(IEnumerable<Type> excluding);
        IRegisterSyntax UsingDefaultInterfaceStrategy();
        IRegisterSyntax UsingAllInterfacesStrategy();
        IRegisterSyntax UsingAllInterfacesStrategy(IEnumerable<Type> excluding);
        IRegisterSyntax UsingStrategy<T>() where T : IRegistrationProducer, new();
        IRegisterSyntax UsingStrategy(IRegistrationProducer registrationProducer);
    }
}
