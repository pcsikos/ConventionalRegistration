using System;
using SimpleInjector;

namespace Flubar.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static void RegistrationByConvention(this Container container, Action<SimpleInjectorConventionBuilder> convention)
        {
            RegistrationByConvention(container, null, convention);
        }

        public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, Action<SimpleInjectorConventionBuilder> convention)//, IEnumerable<Type> exclusions = null)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }

            var typeExclusionTracker = new TypeExclusionTracker();
            var adapter = new SimpleInjectorContainerAdapter(container, typeExclusionTracker);
            using (var builder = new SimpleInjectorConventionBuilder(adapter, configuration, typeExclusionTracker))
            {
                convention(builder);
            }
        }
    }
}
