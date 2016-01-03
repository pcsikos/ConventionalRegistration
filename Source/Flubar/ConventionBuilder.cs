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
        private readonly ITypeExclusionTracker exclusionTracker;
        private readonly RegistrationEntryValidator registrationEntryValidator;
        private readonly ILog logger;

        public ConventionBuilder(IContainer<TLifetime> container, 
            BehaviorConfiguration behaviorConfiguration)
            //RegistrationEntryValidator registrationEntryValidator,
            //ILog logger)
        {
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            this.container = container;
            container.RegistrationCreated += ContainerRegistrationCreated;
            container.ImplementationExcluded += ContainerImplementationExcluded;
            //containerFacade = new ContainerDecorator<TLifetime>(container, (services, implementation) => ExcludeService(services, implementation));
            conventions = new List<Action<ISourceSyntax>>();
            this.behaviorConfiguration = behaviorConfiguration;
            exclusionTracker = new TypeExclusionTracker();
            //this.logger = logger;
            //this.registrationEntryValidator = registrationEntryValidator;
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
                AutomaticRegistration(registration.ImplementationType, services, lifetimeSelection(lifetimeSelector));
                WriteAboutRegistration(registration.ImplementationType, services);
            }));
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            conventions.Add(convention);
            return this;
        }

        private void ContainerImplementationExcluded(object sender, ImplementationExcludedEventArgs e)
        {
            exclusionTracker.ExcludeImplementation(e.Implementation, e.Services);
        }

        private void ContainerRegistrationCreated(object sender, RegistrationEventArgs e)
        {
            exclusionTracker.ExcludeServices(e.Services, e.Implementation);
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
                container.Register(services.First(), implementation, lifetime);
            }
            else
            {
                container.Register(services, implementation, lifetime);
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
        }

        #endregion
    }
}
