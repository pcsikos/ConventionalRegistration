using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.Unity
{
    public static class UnityContainerExtensions
    {
        public static void RegistrationByConvention(this UnityContainer container, Action<ConventionBuilder<LifetimeManager>> convention)
        {
            RegistrationByConvention(container, null, convention);
        }

        public static void RegistrationByConvention(this UnityContainer container, BehaviorConfiguration configuration, Action<ConventionBuilder<LifetimeManager>> convention)
        {
            RegistrationByConvention(container, configuration, (builder, tracker) => convention(builder));
        }

        public static void RegistrationByConvention(this UnityContainer container, BehaviorConfiguration configuration, Action<ConventionBuilder<LifetimeManager>, ITypeExclusionTracker> convention)//, IEnumerable<Type> serviceExclusions = null)
        {
            if (configuration == null)
            {
                configuration = BehaviorConfiguration.Default;
            }

            var typeExclusionTracker = new TypeExclusionTracker();
            var adapter = new UnityContainerAdapter(container, typeExclusionTracker);
            using (var builder = new ConventionBuilder<LifetimeManager>(adapter, configuration, typeExclusionTracker, new TypeTracker()))
            {
                convention(builder, typeExclusionTracker);
            }
        }
    }
}
