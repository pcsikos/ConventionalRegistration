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
        private readonly ITypeTracker typeTracker;

        public ConventionBuilder(IContainer<TLifetime> container, 
            BehaviorConfiguration behaviorConfiguration,
            ITypeExclusionTracker exclusionTracker,
            ITypeTracker typeTracker)
        {
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            this.container = container;
            conventions = new List<Action<ISourceSyntax>>();
            this.behaviorConfiguration = behaviorConfiguration;
            logger = new DiagnosticLogger(behaviorConfiguration);
            registrationEntryValidator = new RegistrationEntryValidator(exclusionTracker, logger);
            this.typeTracker = typeTracker;
        }

        public IContainer<TLifetime> Container => container;

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
                if (typeTracker.AddToCustomRegistrationIfApplicable(services, registration.ImplementationType))
                {
                    return;
                }

                AutomaticRegistration(registration.ImplementationType, services, lifetimeSelection(lifetimeSelector));
                WriteAboutRegistration(registration.ImplementationType, services);
            }));
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            conventions.Add(convention);
            return this;
        }

        public void RegisterAsCollection(Type serviceType)
        {
            SearchForImplementations(serviceType, types =>
            {
                container.RegisterMultipleImplementations(serviceType, types);
            });
        }

        protected void SearchForImplementations(Type serviceType, Action<IEnumerable<Type>> callback)
        {
            typeTracker.RegisterMonitoredType(serviceType, callback);
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
                container.RegisterService(services.First(), implementation, lifetime);
            }
            else
            {
                container.RegisterMultipleServices(services, implementation, lifetime);
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

            typeTracker.Resolve();
        }

        #endregion

       
    }
}
