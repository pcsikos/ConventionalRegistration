using System;

namespace Flubar.RegistrationProducers
{
    public class DefaultInterfaceRegistrationProducer : AbstractRegistrationProducer
    {
        public DefaultInterfaceRegistrationProducer(IRegistrationServiceSelector registrationServiceSelector,
            IServiceFilter serviceFilter)
            : base(registrationServiceSelector, serviceFilter)
        {
        }

        protected override bool IsApplicable(Type service, Type implementation)
        {
            var interfaceName = GetInterfaceName(service);
            var implementationName = GetImplementationName(implementation);

            return interfaceName == implementationName;
        }

        private static string GetImplementationName(Type implementation)
        {
            if (implementation.IsGenericTypeDefinition)
            {
                return implementation.Name.Substring(0, implementation.Name.IndexOf('`'));
            }
            return implementation.Name;
        }

        private static string GetInterfaceName(Type service)
        {
            var name = GetImplementationName(service);
            if (name.StartsWith("I", StringComparison.Ordinal))
            {
                return name.Substring(1);
            }
            return name;
        }
    }
}
