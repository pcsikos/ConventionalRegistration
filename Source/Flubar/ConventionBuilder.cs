﻿using System;
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
        private readonly IServiceFilter registrationEntryValidator;
        private readonly AssemblySelector assemblySelector;

        public ConventionBuilder(IContainer<TLifetime> container, 
            AssemblySelector assemblySelector,
            IServiceFilter registrationEntryValidator)
        {
            this.assemblySelector = assemblySelector;
            this.container = container;
            this.registrationEntryValidator = registrationEntryValidator;

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

        protected virtual void ApplyConventions()
        {
            foreach (var convention in conventions)
            {
                convention(assemblySelector);
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
