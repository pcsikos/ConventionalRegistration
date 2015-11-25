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

            var facade = new SimpleInjectorContainerFacade(container);
            using (var builder = new SimpleInjectorConventionBuilder(facade, configuration))
            {
                convention(builder);
            }
        }

       

        //public static IConventionSyntax<Lifestyle> Register(this IConventionSyntax<Lifestyle> syntax, Action<INotifySyntax> notify)
        //{
        //    notify(syntax.Notify);
        //    return syntax;
        //}
    }
}
