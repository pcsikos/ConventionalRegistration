using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;

namespace Flubar
{
    //TODO: exclude logger
    public class ConventionBuilder<TLifetime> : IConfigurationServiceExclusion, IDisposable
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> container;
        private readonly IDictionary<Type, RegisteredService> registeredServices;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        private readonly BehaviorConfiguration behaviorConfiguration;
        private readonly IDictionary<Type, ExcludedImplementation> excludedImplementations;
        //private IList<Type> registeredServices;

        public ConventionBuilder(IContainer<TLifetime> container, BehaviorConfiguration behaviorConfiguration)
        {
            registeredServices = new Dictionary<Type, RegisteredService>();
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            this.container = container;
            container.RegistrationCreated += ContainerRegistrationCreated;
            container.ImplementationExcluded += ContainerImplementationExcluded;
            //containerFacade = new ContainerDecorator<TLifetime>(container, (services, implementation) => ExcludeService(services, implementation));
            conventions = new List<Action<ISourceSyntax>>();
            this.behaviorConfiguration = behaviorConfiguration;
            excludedImplementations = new Dictionary<Type, ExcludedImplementation>();
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

        private void ExcludeService(Type serviceType, Type implementation)
        {
            if (!serviceType.IsInterface)
            {
                return;
            }

            if (!registeredServices.ContainsKey(serviceType))
            {
                var registeredService = new RegisteredService(serviceType, implementation);
                registeredServices.Add(serviceType, registeredService);
            }
        }

        private void ExcludeServices(IEnumerable<Type> serviceTypes, Type implementation)
        {
            foreach (var serviceType in serviceTypes)
            {
                ExcludeService(serviceType, implementation);
            }
        }

        private void ExcludeRegistration(IRegistrationEntry registration)
        {
            ExcludeServices(registration.ServicesTypes, registration.ImplementationType);
        }

        private void ContainerImplementationExcluded(object sender, ImplementationExcludedEventArgs e)
        {
            ExcludeImplementation(e.Implementation, e.Services);
        }

        private void ContainerRegistrationCreated(object sender, RegistrationEventArgs e)
        {
            ExcludeServices(e.Services, e.Implementation);
        }

        private void ExcludeImplementation(Type implementation, IEnumerable<Type> services)
        {
            if (!excludedImplementations.ContainsKey(implementation))
            {
                excludedImplementations.Add(implementation, new ExcludedImplementation(implementation, services));
                return;
            }
            var excluded = excludedImplementations[implementation];
            excluded.AddServices(services);
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
            if (excludedImplementations.ContainsKey(oldRegistration.ImplementationType))
            {
                var excludedImplementation = excludedImplementations[oldRegistration.ImplementationType];
                bool excludeAll = !excludedImplementation.Services.Any();
                if (!excludeAll && excludedImplementation.Services.Any(excludedService => oldRegistration.ServicesTypes.Any(service => service == excludedService)))
                {
                    var allowedServices = oldRegistration.ServicesTypes.Where(service => !excludedImplementation.Services.Any(excludedService => excludedService == service));
                    excludeAll = !allowedServices.Any();
                    if (!excludeAll)
                    {
                        registration = new RegistrationEntry(oldRegistration.ImplementationType, allowedServices);
                        WriteAboutExcludedImplementation(excludedImplementation.Implementation, excludedImplementation.Services);
                    }
                }
                if (excludeAll)//exclude every service
                {
                    WriteAboutExcludedImplementation(excludedImplementation.Implementation);
                    return null;
                }
            }
            if (behaviorConfiguration.ExcludeRegisteredServices)
            {
                var allowedServices = registration.ServicesTypes.Where(serviceType => !registeredServices.ContainsKey(serviceType)).ToArray();
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
                var registeredService = registeredServices[serviceType];
                var registeredImplementationName = GetRegisteredServiceImplementationName(registeredService);
                var skippingMessage = string.Format("Skipping {0} to {1}, because {2} is already registered to this service.",
                        serviceType.FullName, registration.ImplementationType.FullName, registeredImplementationName);
                if (registration.ImplementationType == registeredService.Implementation)
                {
                    WriteInfo(skippingMessage);
                }
                else
                {
                    WriteWarning(skippingMessage);
                }
            }
        }

        private static string GetRegisteredServiceImplementationName(RegisteredService registeredService)
        {
            return registeredService.Implementation == null ? "[Unknown]" : registeredService.Implementation.FullName;
        }

        private void WriteWarning(string message)
        {
            behaviorConfiguration.Log(DiagnosticLevel.Warning, message);
        }

        #region ITypeExclusion Members

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(IRegistrationEntry registration)
        {
            ExcludeRegistration(registration);
            return this;
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(IEnumerable<IRegistrationEntry> registrations)
        {
            throw new NotImplementedException();
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(Type serviceType, Type implementation)
        {
            ExcludeService(serviceType, implementation);
            return this;
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(IEnumerable<Type> serviceTypes, Type implementation)
        {
            ExcludeServices(serviceTypes, implementation);
            return this;
        }

        #endregion

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

        class RegisteredService
        {
            private readonly Type implementation;
            private readonly Type serviceType;

            public RegisteredService(Type serviceType, Type implementation)
            {
                this.serviceType = serviceType;
                this.implementation = implementation;
            }

            public Type Implementation => implementation;

            public Type ServiceType => serviceType;
        }

        class ExcludedImplementation
        {
            readonly Type implementation;
            readonly IList<Type> excludedServices;

            public ExcludedImplementation(Type implementation, IEnumerable<Type> services)
            {
                this.excludedServices = new List<Type>(services);
                this.implementation = implementation;
            }

            public Type Implementation => implementation;

            public IEnumerable<Type> Services => excludedServices;

            public void AddServices(IEnumerable<Type> services)
            {
                foreach (var service in services)
                {
                    if (!excludedServices.Contains(service))
                    {
                        excludedServices.Add(service);
                    }
                }
            }
        }
    }
}
