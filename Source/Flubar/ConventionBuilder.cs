using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;

namespace Flubar
{
    public class ConventionBuilder<TLifetime> : IDisposable
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> container;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        private readonly BehaviorConfiguration behaviorConfiguration;
        private readonly RegistrationEntryValidator registrationEntryValidator;
        private readonly ILog logger;
        private readonly IDictionary<Type, CustomRegistration> customRegistrations = new Dictionary<Type, CustomRegistration>();

        public ConventionBuilder(IContainer<TLifetime> container, 
            BehaviorConfiguration behaviorConfiguration,
            ITypeExclusionTracker exclusionTracker)
        {
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            this.container = container;
            conventions = new List<Action<ISourceSyntax>>();
            this.behaviorConfiguration = behaviorConfiguration;
            logger = new DiagnosticLogger(behaviorConfiguration);
            registrationEntryValidator = new RegistrationEntryValidator(exclusionTracker, logger);
        }

        public ConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null)
        {
            lifetimeSelection = GetDefaultLifetimeWhenNull(lifetimeSelection);
            return Define(syntax => rules(syntax).RegisterEach((registration) =>
            {
                var services = registrationEntryValidator.GetAllowedServices(registration.ImplementationType, registration.ServicesTypes);
                if (!services.Any())
                {
                    return;
                }
                if (AddToCustomRegistrationIfApplicable(services, registration.ImplementationType))
                {
                    return;
                }

                AutomaticRegistration(registration.ImplementationType, services, lifetimeSelection(lifetimeSelector));
                WriteAboutRegistration(registration.ImplementationType, services);
            }));
        }

        private bool AddToCustomRegistrationIfApplicable(IEnumerable<Type> services, Type implementationType)
        {
            foreach (var serviceType in services)
            {
                if (customRegistrations.ContainsKey(serviceType))
                {
                    var customRegistration = customRegistrations[serviceType];
                    customRegistration.AddImlementation(implementationType);
                    return true;
                }
                var genericType = serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : null;
                if (genericType != null && customRegistrations.ContainsKey(genericType))
                {
                    var customRegistration = customRegistrations[genericType];
                    customRegistration.AddImlementation(implementationType);
                    return true;
                }
            }
            return false;
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            conventions.Add(convention);
            return this;
        }

        protected void SearchForImplementations(Type serviceType, Action<IEnumerable<Type>> callback)
        {
            if (customRegistrations.ContainsKey(serviceType))
            {
                throw new ArgumentException();
            }
            customRegistrations.Add(serviceType, new CustomRegistration(serviceType, callback));
        }

        private Func<ILifetimeSyntax<TLifetime>, TLifetime> GetDefaultLifetimeWhenNull(Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection)
        {
            return lifetimeSelection ?? (x => x.Transient);
        }

        private void AutomaticRegistration(Type implementation, IEnumerable<Type> services, TLifetime lifetime)
        {
            var count = services.Count();
            if (count == 1)//one to one
            {
                container.RegisterType(services.First(), implementation, lifetime);
            }
            else
            {
                container.RegisterAll(services, implementation, lifetime);
            }
        }

        private IServiceFilter GetServiceFilterFromConfiguration()
        {
            var configurationServiceFilter = ((IBehaviorConfiguration)behaviorConfiguration).GetServiceFilter();
            return configurationServiceFilter;
        }

        private void WriteAboutRegistration(Type implementation, IEnumerable<Type> services)
        {
            foreach (var serviceType in services)
            {
                logger.Info("Registration {0} to {1}.", serviceType.FullName, implementation.FullName);
            }
        }

        #region IDispose Members

        public void Dispose()
        {
            IServiceFilter serviceFilter = GetServiceFilterFromConfiguration();
            var asmSelector = new AssemblySelector(serviceFilter);
            foreach (var convention in conventions)
            {
                convention(asmSelector);
            }

            foreach (var customRegistration in customRegistrations.Values)
            {
                customRegistration.InvokeCallback();
            }
        }

        #endregion

        private class CustomRegistration
        {
            readonly Type serviceType;
            readonly Action<IEnumerable<Type>> callback;
            private readonly ISet<Type> implementations;

            public CustomRegistration(Type serviceType, Action<IEnumerable<Type>> callback)
            {
                this.callback = callback;
                this.serviceType = serviceType;
                implementations = new HashSet<Type>();
            }

            public void AddImlementation(Type implementationType)
            {
                if (!implementations.Contains(implementationType))
                {
                    implementations.Add(implementationType);
                }
            }

            public IEnumerable<Type> GetImplementations()
            {
                return implementations;
            }

            internal void InvokeCallback()
            {
                callback(GetImplementations());
            }
        }
    }
}
