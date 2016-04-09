using SimpleInjector;
using System;

namespace Flubar.SimpleInjector
{
    public class SimpleInjectorConventionBuilder : ConventionBuilder<Lifestyle>
    {
        private readonly SimpleInjectorContainerAdapter containerAdapter;

        internal SimpleInjectorConventionBuilder(SimpleInjectorContainerAdapter containerAdapter, 
            BehaviorConfiguration behaviorConfiguration, 
            ITypeExclusionTracker exclusionTracker) 
            : base(containerAdapter, behaviorConfiguration, exclusionTracker)
        {
            this.containerAdapter = containerAdapter;
        }

        public void ExplicitRegistration(Action<ISimpleInjectorContainerAdapter> explicitRegistrations)
        {
            explicitRegistrations(containerAdapter);
        }

        public void RegisterAsCollection(Type serviceType)
        {
            SearchForImplementations(serviceType, types =>
            {
                containerAdapter.Container.RegisterCollection(serviceType, types);
            });
        }
    }
}
