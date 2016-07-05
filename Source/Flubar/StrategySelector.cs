using Flubar.RegistrationProducers;
using Flubar.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    public class StrategySelector : IStrategySyntax
    {
        public readonly IEnumerable<Type> types;

        public StrategySelector(IEnumerable<Type> types)
        {
            this.types = types;
        }

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

        public IRegisterSyntax UsingStrategy(IRegistrationProducer registrationProducer)
        {
            var registrations = types.Select(type => registrationProducer.CreateRegistrationEntry(type)).Where(x => x != null);
            return new RegistrationPerformer(registrations);
        }

        #endregion
    }
}
