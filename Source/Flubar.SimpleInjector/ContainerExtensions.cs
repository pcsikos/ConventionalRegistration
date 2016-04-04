using System;
using SimpleInjector;
using Flubar.Syntax;

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
            var adapter = new SimpleInjectorContainerAdapterAdapter(container, typeExclusionTracker);
            using (var builder = new SimpleInjectorConventionBuilder(adapter, configuration, typeExclusionTracker))
            {
                convention(builder);
            }
        }

        //public static void RegistrationByConvention(this Container container, BehaviorConfiguration configuration, params ISimpleInjectorBuilderPackage[] packages)
        //{
        //    RegistrationByConvention(container, configuration, builder =>
        //    {
        //        foreach (var package in packages)
        //        {
        //            package.RegisterByConvention(container, builder);
        //        }

        //        foreach (var package in packages)
        //        {
        //            package.PostRegistrations(container);
        //        }
        //    });
        //}
    }
}
