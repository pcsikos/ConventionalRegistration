using System;
using SimpleInjector;

namespace Flubar.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static void RegistrationByConvention(this Container container, Action<ConventionBuilder<Lifestyle>> convention)
        {
            RegistrationByConvention(container, null, convention);
        }

        public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, Action<ConventionBuilder<Lifestyle>> convention)
        {
            RegistrationByConvention(container, configuration, (builder, tracker) => convention(builder));
        }

        public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, Action<ConventionBuilder<Lifestyle>, ITypeExclusionTracker> convention)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }
            
            var typeExclusionTracker = new TypeExclusionTracker();
            var adapter = new SimpleInjectorContainerAdapter(container, typeExclusionTracker);
            using (var builder = new ConventionBuilder<Lifestyle>(adapter, configuration, typeExclusionTracker, new ServiceMappingTracker()))
            {
                convention(builder, typeExclusionTracker);
            }
        }

        public static void ExplicitRegistration(this ConventionBuilder<Lifestyle> builder, Action<ISimpleInjectorContainerAdapter> explicitRegistrations)
        {
            var container = (ISimpleInjectorContainerAdapter)builder.Container;
            explicitRegistrations(container);
        }
    }
}
