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
        private readonly SimpleInjectorContainerAdapter _containerAdapterAdapter;

        internal SimpleInjectorConventionBuilder(SimpleInjectorContainerAdapter _containerAdapterAdapter, 
            BehaviorConfiguration behaviorConfiguration, 
            ITypeExclusionTracker exclusionTracker) 
            : base(_containerAdapterAdapter, behaviorConfiguration, exclusionTracker)
        {
            this._containerAdapterAdapter = _containerAdapterAdapter;
        }

        public void ExplicitRegistration(Action<ISimpleInjectorContainerAdapter> explicitRegistrations)
        {
            explicitRegistrations(_containerAdapterAdapter);
        }

        public void RegisterAsCollection(Type serviceType)
        {
            SearchForImplementations(serviceType, types =>
            {
                _containerAdapterAdapter.Container.RegisterCollection(serviceType, types);
            });
        }
    }
}
