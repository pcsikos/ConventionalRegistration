using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;
using Flubar.TypeFiltering;
using System.Linq.Expressions;
using System.Reflection;

namespace Flubar
{
    /// <summary>
    /// Provides the basic functionality for convention based registration.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    public class ConventionBuilder<TLifetime> : IDisposable, IConventionBuilder<TLifetime> 
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> container;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        private readonly IServiceFilter registrationEntryValidator;
        private readonly ISourceSyntax sourceSyntax;
        private readonly IServiceExtractor serviceExtractor;

        public ConventionBuilder(IContainer<TLifetime> container, 
            ISourceSyntax sourceSyntax,
            IServiceFilter registrationEntryValidator,
            IServiceExtractor serviceExtractor)
        {
            this.sourceSyntax = sourceSyntax;
            this.container = container;
            this.registrationEntryValidator = registrationEntryValidator;
            this.serviceExtractor = serviceExtractor;

            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            conventions = new List<Action<ISourceSyntax>>();
        }

        public IContainer<TLifetime> Container => container;

        public ConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null)
        {
            lifetimeSelection = lifetimeSelection ?? (x => x.Transient);
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
            }));
        }

        protected virtual IEnumerable<Type> FilterServices(IEnumerable<Type> services, Type implementationType)
        {
            return services;
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {
            conventions.Add(convention);
            return this;
        }

        public void RegisterAsCollection(Type serviceType)
        {
            serviceExtractor.RegisterMonitoredType(serviceType);
        }

        protected virtual void ApplyConventions()
        {
            foreach (var convention in conventions)
            {
                convention(sourceSyntax);
            }

            foreach (var service in serviceExtractor.GetServiceImplementations())
            {
                Container.RegisterMultipleImplementations(service.ServiceType, service.GetImplementations());
            }
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

      
           
        #region IDispose Members

        public void Dispose()
        {
            ApplyConventions();
        }

        #endregion
    }
}
