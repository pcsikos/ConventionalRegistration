using System;
using System.Collections.Generic;

namespace Flubar
{
    public class BehaviorConfiguration : IBehaviorConfiguration
    {
        private IEnumerable<Type> excludedServices;
        private IEnumerable<Type> excludedBaseTypes;

        public BehaviorConfiguration()
        {

        }

        public BehaviorConfiguration(Type[] serviceTypesToExclude, Type[] baseTypesToExclude)
        {
            excludedServices = serviceTypesToExclude;
            excludedBaseTypes = baseTypesToExclude;
        }

        public static BehaviorConfiguration Default
        {
            get
            {
                return new BehaviorConfiguration(
                    new[] { typeof(IDisposable), typeof(System.Collections.IEnumerable), typeof(IEnumerable<>) },
                    new[] { typeof(Exception) }
                    );
            }
        }

        public BehaviorConfiguration ExcludeServices(params Type[] serviceTypesToExclude)
        {
            excludedServices = serviceTypesToExclude;
            return this;
        }

        public BehaviorConfiguration ExcludeTypeInheriting(params Type[] baseTypesToExclude)
        {
            excludedBaseTypes = baseTypesToExclude;
            return this;
        }

        #region IBehaviorConfiguration Members

        IEnumerable<Type> IBehaviorConfiguration.ExcludedServices
        {
            get { return excludedServices; }
        }

        IEnumerable<Type> IBehaviorConfiguration.ExcludedBaseTypes
        {
            get { return excludedBaseTypes; }
        }

        #endregion
    }
}
