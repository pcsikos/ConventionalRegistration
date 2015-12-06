using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;

namespace Flubar
{
    public class ConventionBuilder<TLifetime> : IConfigurationServiceExclusion, IDisposable
        where TLifetime : class
    {
        private readonly IContainerFacade<TLifetime> containerFacade;
        private readonly IDictionary<Type, RegisteredService> registeredServices;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        private readonly BehaviorConfiguration behaviorConfiguration;
        //private IList<Type> registeredServices;

        public ConventionBuilder(IContainerFacade<TLifetime> container, BehaviorConfiguration behaviorConfiguration)
        {
            registeredServices = new Dictionary<Type, RegisteredService>();
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            containerFacade = new ContainerDecorator<TLifetime>(container, (services, implementation) => ExcludeService(services, implementation));
            conventions = new List<Action<ISourceSyntax>>();
            this.behaviorConfiguration = behaviorConfiguration;
        }

        #region IConventionSyntax<TLifetime> Members

        public ConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null)
        {
            lifetimeSelection = GetDefaultLifetimeWhenNull(lifetimeSelection);
            return Define(syntax => rules(syntax).RegisterEach((registration) =>
            {
                registration = ValidateRegistration(registration);
                if (registration == null)
                {
                    return;
                }
                AutomaticRegistration(registration, lifetimeSelection(lifetimeSelector));
                Exclude(registration);
            }));
        }

        private IRegistrationEntry ValidateRegistration(IRegistrationEntry oldRegistration)
        {
            IRegistrationEntry registration = null;
            if (behaviorConfiguration.ExcludeRegisteredServices)
            {
                var allowedServices = oldRegistration.ServicesTypes.Where(serviceType => !registeredServices.ContainsKey(serviceType)).ToArray();
                if (allowedServices.Length == 0)
                {
                    WriteAboutExcludedServices(oldRegistration, allowedServices);
                    return null;
                }
                if (allowedServices.Length != oldRegistration.ServicesTypes.Count())
                {
                    WriteAboutExcludedServices(oldRegistration, allowedServices);
                    registration = new RegistrationEntry(oldRegistration.ImplementationType, allowedServices);
                }
            }
            registration = registration ?? oldRegistration;
            WriteAboutRegistration(registration);
            return registration;
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
            behaviorConfiguration.Log(DiagnosticMode.Info, message);
        }

        private void WriteInfo(string format, params object[] args)
        {
            behaviorConfiguration.Log(DiagnosticMode.Info, string.Format(format, args));
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
                var skippingMessage = string.Format("Skipping {0} to {1}, because {2} is already registered to this type.", 
                        serviceType.FullName, registration.ImplementationType.FullName, registeredService.Implementation.FullName);
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

        private void WriteWarning(string message)
        {
            behaviorConfiguration.Log(DiagnosticMode.Warning, message);
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            conventions.Add(convention);
            return this;
        }

        public void ExplicitRegister<TService>(Func<TService> instanceCreator, TLifetime lifetime = null)
            where TService : class
        {
            containerFacade.Register(instanceCreator, lifetime);
        }

        public void ExplicitRegister<TService, TImplementation>(TLifetime lifetime = null)
            where TService : class
            where TImplementation : class, TService
        {
            containerFacade.Register(typeof(TService), typeof(TImplementation), lifetime);
        }

        public void ExplicitRegister<TConcrete>(TLifetime lifetime)
            where TConcrete : class
        {
            ExplicitRegister<TConcrete, TConcrete>(lifetime);
        }

        #endregion

        #region ITypeExclusion Members

        void Exclude(IRegistrationEntry registration)
        {
            ExcludeService(registration.ServicesTypes, registration.ImplementationType);
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(IRegistrationEntry registration)
        {
            Exclude(registration);
            return this;
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(IEnumerable<IRegistrationEntry> registrations)
        {
            throw new NotImplementedException();
        }

        protected void ExcludeService(Type serviceType, Type implementation)
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

        protected void ExcludeService(IEnumerable<Type> serviceTypes, Type implementation)
        {
            foreach (var serviceType in serviceTypes)
            {
                ExcludeService(serviceType, implementation);
            }
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(Type serviceType, Type implementation)
        {
            ExcludeService(serviceType, implementation);
            return this;
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(IEnumerable<Type> serviceTypes, Type implementation)
        {
            ExcludeService(serviceTypes, implementation);
            return this;
        }

        #endregion

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
                containerFacade.Register(registration.ServicesTypes.First(), registration.ImplementationType, lifetime);
            }
            else
            {
                containerFacade.Register(registration.ServicesTypes, registration.ImplementationType, lifetime);
            }
        }

        public void Dispose()
        {
            IServiceFilter serviceFilter = GetServiceFilterFromConfiguration();
            var asmSelector = new AssemblySelector(serviceFilter);
            foreach (var convention in conventions)
            {
                convention(asmSelector);
            }
        }

        private IServiceFilter GetServiceFilterFromConfiguration()
        {
            var configurationServiceFilter = ((IBehaviorConfiguration)behaviorConfiguration).GetServiceFilter();
            //var serviceFilter = behaviorConfiguration.ExcludeRegisteredServices && behaviorConfiguration.Diagnostic == DiagnosticMode.Disabled
            //    ? new AggregateServiceFilter(new[] { configurationServiceFilter, new ExcludedServiceFilter(registeredServices, t => false) }) : configurationServiceFilter;
            return configurationServiceFilter;
        }

        class RegisteredService
        {
            private readonly Type implementation;
            private readonly Type serviceType;

            public RegisteredService(Type serviceType, Type implementation)
            {
                this.serviceType = serviceType;
                this.implementation = implementation;
            }

            public Type Implementation
            {
                get
                {
                    return implementation;
                }
            }

            public Type ServiceType
            {
                get
                {
                    return serviceType;
                }
            }
        }
    }
}
