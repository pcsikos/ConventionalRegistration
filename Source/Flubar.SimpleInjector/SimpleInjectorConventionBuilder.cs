using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ExplicitRegistration(Action<ISimpleInjectorContainer> explicitRegistrations)
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
