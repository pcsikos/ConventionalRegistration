﻿using Flubar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar
{
    public class BehaviorConfiguration : IBehaviorConfiguration
    {
        private IEnumerable<Type> excludedServices;
        private IEnumerable<Type> excludedBaseTypes;
        private Func<Type, bool> filter;

        public BehaviorConfiguration()
            : this(testc => false)
        {
        }

        public BehaviorConfiguration(Func<Type, bool> filter)
        {
            this.filter = filter;
            excludedServices = new Type[0];
            Diagnostic = DiagnosticMode.Disabled;
            log = (mode, message) => { };
        }

        public static BehaviorConfiguration Default
        {
            get
            {
                return new BehaviorConfiguration(t => t.Namespace.StartsWith("System", StringComparison.Ordinal)
                    //new[] { typeof(IDisposable), typeof(System.Collections.IEnumerable), typeof(IEnumerable<>) },
                    //new[] { typeof(Exception) }
                    )
                {
                    ExcludeRegisteredServices = true
                };
            }
        }

        /// <summary>
        /// Gets or sets a value, indicating whether future registrations of same service should be excluded.
        /// </summary>
        public bool ExcludeRegisteredServices { get; set; }

        public DiagnosticMode Diagnostic { get; set; }

        private Action<DiagnosticMode, string> log;

        public Action<DiagnosticMode, string> Log
        {
            get { return log; }
            set
            {
                if (value == null)
                {
                    return;
                }
                log = value;
            }
        }


        public IEnumerable<Type> ExcludedServices
        {
            get
            {
                return excludedServices;
            }

            set
            {
                if (value == null)
                {
                    return;
                }
                excludedServices = value;
            }
        }

        public BehaviorConfiguration ExcludeServices(Func<Type, bool> filter)
        {
            this.filter = filter;
            return this;
        }

        public BehaviorConfiguration ExcludeServices(params Type[] serviceTypesToExclude)
        {
            ExcludedServices = serviceTypesToExclude;
            return this;
        }

        //public BehaviorConfiguration ExcludeTypeInheriting(params Type[] baseTypesToExclude)
        //{
        //    excludedBaseTypes = baseTypesToExclude;
        //    return this;
        //}

        #region IBehaviorConfiguration Members

        IServiceFilter IBehaviorConfiguration.GetServiceFilter()
        {
            return new ExcludedServiceFilter(excludedServices, filter);
        }

        #endregion
    }
}
