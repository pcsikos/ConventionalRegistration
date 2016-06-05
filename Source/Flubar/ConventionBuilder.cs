using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;
using Flubar.TypeFiltering;

namespace Flubar
{
    public class ConventionBuilder<TLifetime> : IDisposable
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> container;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        //private readonly IBehaviorConfiguration behaviorConfiguration;
        private readonly RegistrationEntryValidator registrationEntryValidator;
        private readonly ILog logger;
        //private readonly IServiceExtractor serviceExtractor;
        //readonly IImplementationFilter implementationFilter;
        readonly AssemblySelector assemblySelector;

        public ConventionBuilder(IContainer<TLifetime> container, 
            //IBehaviorConfiguration behaviorConfiguration,
            IServiceMappingTracker serviceMappings,
            //IServiceExtractor serviceExtractor,
            //IImplementationFilter implementationFilter,
            AssemblySelector assemblySelector,
            ILog logger)
        {
            this.assemblySelector = assemblySelector;
            //this.implementationFilter = implementationFilter;
            this.container = container;
            this.logger = logger;
            //this.behaviorConfiguration = behaviorConfiguration;
           // this.serviceExtractor = serviceExtractor;

            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            conventions = new List<Action<ISourceSyntax>>();
            //logger = new DiagnosticLogger(behaviorConfiguration);
            registrationEntryValidator = new RegistrationEntryValidator(serviceMappings, logger);
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
                var filteredServices = FilterServices(services, registration.ImplementationType);
                if (!filteredServices.Any())
                {
                    return;
                }

                AutomaticRegistration(registration.ImplementationType, services, lifetimeSelection(lifetimeSelector));
                WriteAboutRegistration(registration.ImplementationType, services);
            }));
        }

        protected virtual IEnumerable<Type> FilterServices(IEnumerable<Type> services, Type implementationType)
        {
            return services;
            //return serviceExtractor.RegisterMapping(services, implementationType);
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            conventions.Add(convention);
            return this;
        }

        //public void RegisterAsCollection(Type serviceType)
        //{
        //    SearchForImplementations(serviceType, types =>
        //    {
        //        container.RegisterMultipleImplementations(serviceType, types);
        //    });
        //}

        //protected void SearchForImplementations(Type serviceType, Action<IEnumerable<Type>> callback)
        //{
        //    serviceExtractor.RegisterMonitoredType(serviceType, callback);
        //}

        protected virtual void ApplyConventions()
        {
            //ITypeFilter serviceFilter = GetServiceFilterFromConfiguration();
            //var implementationFilter = new TypeFilter();
            //var asmSelector = new AssemblySelector(serviceFilter, implementationFilter);
            foreach (var convention in conventions)
            {
                convention(assemblySelector);
            }

            //serviceExtractor.Resolve();
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

        //private ITypeFilter GetServiceFilterFromConfiguration()
        //{
        //    var configurationServiceFilter = ((IBehaviorConfiguration)behaviorConfiguration).GetServiceFilter();
        //    return configurationServiceFilter;
        //}

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
            ApplyConventions();
        }

        #endregion
    }
}
