using System;

namespace ConventionalRegistration.RegistrationProducers
{
    public static class TypeExtensions
    {
        public static string GetImplementationName(this Type implementation)
        {
            if (implementation.IsGenericTypeDefinition)
            {
                return implementation.Name.Substring(0, implementation.Name.IndexOf('`'));
            }
            return implementation.Name;
        }

        public static string GetInterfaceName(this Type service)
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
