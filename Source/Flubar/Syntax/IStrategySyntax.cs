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
        IRegisterSyntax UsingStrategy(Func<ITypeFilter, IRegistrationProducer> producerFactory);
        IRegisterSyntax UsingStrategy(IRegistrationProducer registrationProducer);
    }
}
