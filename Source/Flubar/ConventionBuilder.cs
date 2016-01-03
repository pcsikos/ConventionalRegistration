using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;

namespace Flubar
{
    //TODO: exclude logger
    public class ConventionBuilder<TLifetime> : IDisposable
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> container;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        private readonly BehaviorConfiguration behaviorConfiguration;
        private readonly ITypeExclusionTracker exclusionTracker;

        public ConventionBuilder(IContainer<TLifetime> container, BehaviorConfiguration behaviorConfiguration)
        {
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            this.container = container;
            container.RegistrationCreated += ContainerRegistrationCreated;
            container.ImplementationExcluded += ContainerImplementationExcluded;
            //containerFacade = new ContainerDecorator<TLifetime>(container, (services, implementation) => ExcludeService(services, implementation));
            conventions = new List<Action<ISourceSyntax>>();
            this.behaviorConfiguration = behaviorConfiguration;
            exclusionTracker = new TypeExclusionTracker();
        }

        public ConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null)
        {
            lifetimeSelection = GetDefaultLifetimeWhenNull(lifetimeSelection);
            return Define(syntax => rules(syntax).RegisterEach((registration) =>
            {
                var newRegistration = ValidateRegistration(registration);
                if (newRegistration == null)
                {
                    return;
                }
                AutomaticRegistration(newRegistration, lifetimeSelection(lifetimeSelector));
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

        private void AutomaticRegistration(IRegistrationEntry registration, TLifetime lifetime)
        {
            var count = registration.ServicesTypes.Count();
            if (count == 0)
            {
                throw new InvalidOperationException();
            }

            if (count == 1)//one to one
            {
                container.Register(registration.ServicesTypes.First(), registration.ImplementationType, lifetime);
            }
            else
            {
                container.Register(registration.ServicesTypes, registration.ImplementationType, lifetime);
            }
        }

        private IServiceFilter GetServiceFilterFromConfiguration()
        {
            var configurationServiceFilter = ((IBehaviorConfiguration)behaviorConfiguration).GetServiceFilter();
            return configurationServiceFilter;
        }

        private IRegistrationEntry ValidateRegistration(IRegistrationEntry oldRegistration)
        {
            IRegistrationEntry registration = oldRegistration;
            if (exclusionTracker.ContainsImplementation(oldRegistration.ImplementationType))
            {
                var implementationServices = exclusionTracker.GetImplemetationServices(oldRegistration.ImplementationType);
                bool excludeAll = !implementationServices.Any();
                if (!excludeAll && implementationServices.Any(excludedService => oldRegistration.ServicesTypes.Any(service => service == excludedService)))
                {
                    var allowedServices = oldRegistration.ServicesTypes.Where(service => !implementationServices.Any(excludedService => excludedService == service));
                    excludeAll = !allowedServices.Any();
                    if (!excludeAll)
                    {
                        registration = new RegistrationEntry(oldRegistration.ImplementationType, allowedServices);
                        WriteAboutExcludedImplementation(oldRegistration.ImplementationType, implementationServices);
                    }
                }
                if (excludeAll)//exclude every service
                {
                    WriteAboutExcludedImplementation(oldRegistration.ImplementationType);
                    return null;
                }
            }
            if (behaviorConfiguration.ExcludeRegisteredServices)
            {
                var allowedServices = registration.ServicesTypes.Where(serviceType => !exclusionTracker.ContainsService(serviceType)).ToArray();
                if (allowedServices.Length == 0)
                {
                    WriteAboutExcludedServices(registration, allowedServices);
                    return null;
                }
                if (allowedServices.Length != registration.ServicesTypes.Count())
                {
                    WriteAboutExcludedServices(registration, allowedServices);
                    registration = new RegistrationEntry(registration.ImplementationType, allowedServices);
                }
            }
            //registration = registration ?? oldRegistration;
            WriteAboutRegistration(registration);
            return registration;
        }

        private void WriteAboutExcludedImplementation(Type implementation)
        {
            WriteInfo("Excluded Implementation {0}.", implementation.FullName);
        }

        private void WriteAboutExcludedImplementation(Type implementation, IEnumerable<Type> excludedServices)
        {
            foreach (var serviceType in excludedServices)
            {
                WriteInfo("Excluded Implementation {0} for service {1}.", implementation.FullName, serviceType.FullName);
            }
        }

        private void WriteAboutRegistration(IRegistrationEntry registration)
        {
            foreach (var serviceType in registration.ServicesTypes)
            {
                WriteInfo("Registration {0} to {1}", serviceType.FullName, registration.ImplementationType.FullName);
            }
        }

        private void WriteInfo(string message)
        {
            behaviorConfiguration.Log(DiagnosticLevel.Info, message);
        }

        private void WriteInfo(string format, params object[] args)
        {
            behaviorConfiguration.Log(DiagnosticLevel.Info, string.Format(format, args));
        }

        private void WriteAboutExcludedServices(IRegistrationEntry registration, Type[] allowedServices)
        {
            foreach (var serviceType in registration.ServicesTypes)
            {
                if (allowedServices.Any(type => type == serviceType))
                {
                    continue;
                }
                var implementationType = exclusionTracker.GetServiceImplementation(serviceType);
                var registeredImplementationName = GetRegisteredServiceImplementationName(implementationType);
                var skippingMessage = string.Format("Skipping {0} to {1}, because {2} is already registered to this service.",
                        serviceType.FullName, registration.ImplementationType.FullName, registeredImplementationName);
                if (registration.ImplementationType == implementationType)
                {
                    WriteInfo(skippingMessage);
                }
                else
                {
                    WriteWarning(skippingMessage);
                }
            }
        }

        private void WriteWarning(string message)
        {
            behaviorConfiguration.Log(DiagnosticLevel.Warning, message);
        }

        private static string GetRegisteredServiceImplementationName(Type implementationType)
        {
            return implementationType == null ? "[Unknown]" : implementationType.FullName;
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
