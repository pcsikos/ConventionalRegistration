using System;
using SimpleInjector;

namespace Flubar.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static ConventionBuilder<Lifestyle> RegistrationByConvention(this Container container, BehaviorConfiguration configuration = null)//, IEnumerable<Type> exclusions = null)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }

            var facade = new SimpleInjectorContainerFacade(container);
            var syntax = new ConventionBuilder<Lifestyle>(facade, configuration);
            return syntax;
        }

        public static ConventionBuilder<Lifestyle> Register<TService>(this ConventionBuilder<Lifestyle> builder, Func<TService> instanceCreator)
            where TService : class
        {
            ((Container)builder.Container).Register(instanceCreator);
            ((ITypeExclusion)builder).Exclude(typeof(TService));
            return builder;
        }

        public static ConventionBuilder<Lifestyle> Register<TService, TImplementation>(this ConventionBuilder<Lifestyle> builder)
            where TService : class
            where TImplementation : class, TService
        {
            ((Container)builder.Container).Register<TService, TImplementation>();
            ITypeExclusion typeExlusion = builder;
            typeExlusion.Exclude(typeof(TImplementation));
            return builder;
        }

        //public static IConventionSyntax<Lifestyle> Register(this IConventionSyntax<Lifestyle> syntax, Action<INotifySyntax> notify)
        //{
        //    notify(syntax.Notify);
        //    return syntax;
        //}
    }
}
