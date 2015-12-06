using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;

namespace Flubar
{
    public class ConventionBuilder<TLifetime> : /*IConventionBuilder<TLifetime>,*/  IConfigurationServiceExclusion, IDisposable
        where TLifetime : class
    {
        private readonly IContainerFacade<TLifetime> containerFacade;
        private readonly ISet<Type> registeredServices;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        private readonly BehaviorConfiguration behaviorConfiguration;
        //private IList<Type> registeredServices;

        public ConventionBuilder(IContainerFacade<TLifetime> container, BehaviorConfiguration behaviorConfiguration)
        {
            registeredServices = new HashSet<Type>();
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            containerFacade = new ContainerDecorator<TLifetime>(container, type => ExcludeService(type));
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
            if (behaviorConfiguration.ExcludeRegisteredServices)
            {
                var allowedServices = oldRegistration.ServicesTypes.Where(serviceType => !registeredServices.Contains(serviceType)).ToArray();
                if (allowedServices.Length == 0)
                {
                    return null;
                }
                if (allowedServices.Length != oldRegistration.ServicesTypes.Count())
                {
                    return new RegistrationEntry(oldRegistration.ImplementationType, allowedServices);
                }
            }
            return oldRegistration;
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            conventions.Add(convention);
            return this;
        }

        public void Register<TService>(Func<TService> instanceCreator, TLifetime lifetime = null)
            where TService : class
        {
            containerFacade.Register(instanceCreator, lifetime);
        }

        public void Register<TService, TImplementation>(TLifetime lifetime = null)
            where TService : class
            where TImplementation : class, TService
        {
            containerFacade.Register(typeof(TService), typeof(TImplementation), lifetime);
        }

        public void Register<TConcrete>(TLifetime lifetime)
            where TConcrete : class
        {
            Register<TConcrete, TConcrete>(lifetime);
        }

        #endregion

        #region ITypeExclusion Members

        void Exclude(IRegistrationEntry registration)
        {
            Exclude(registration.ServicesTypes);
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

        protected void ExcludeService(Type serviceType)
        {
            if (!serviceType.IsInterface)
            {
                return;
            }

            if (!registeredServices.Contains(serviceType))
            {
                registeredServices.Add(serviceType);
            }
        }

        protected void Exclude(IEnumerable<Type> serviceTypes)
        {
            foreach (var serviceType in serviceTypes)
            {
                ExcludeService(serviceType);
            }
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(Type serviceType)
        {
            ExcludeService(serviceType);
            return this;
        }

        IConfigurationServiceExclusion IConfigurationServiceExclusion.Exclude(IEnumerable<Type> serviceTypes)
        {
            Exclude(serviceTypes);
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

    }
}
