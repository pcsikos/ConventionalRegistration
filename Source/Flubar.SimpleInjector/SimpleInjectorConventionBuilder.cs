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
        private readonly ISimpleInjectorContainer containerAdapter;

        internal SimpleInjectorConventionBuilder(ISimpleInjectorContainer containerAdapter, BehaviorConfiguration behaviorConfiguration) 
            : base(containerAdapter, behaviorConfiguration)
        {
            this.containerAdapter = containerAdapter;
        }

        public void ExplicitRegistration(Action<ISimpleInjectorContainer> explicitRegistrations)
        {
            explicitRegistrations(containerAdapter);
        }
    }
}
