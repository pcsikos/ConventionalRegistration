using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;

namespace Flubar
{
    public class ConventionBuilder<TLifetime> : /*IConventionBuilder<TLifetime>,*/  ITypeExclusion, IDisposable
        where TLifetime : class
    {
        private readonly IContainerFacade<TLifetime> containerFacade;
        private readonly IList<Type> registeredServices;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        private readonly BehaviorConfiguration behaviorConfiguration;
        //private IList<Type> registeredServices;

        public ConventionBuilder(IContainerFacade<TLifetime> container, BehaviorConfiguration behaviorConfiguration)
        {
            registeredServices = new List<Type>();
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            containerFacade = new ContainerDecorator<TLifetime>(container, type => Exclude(type));
            conventions = new List<Action<ISourceSyntax>>();
            this.behaviorConfiguration = behaviorConfiguration;
        }

        #region IConventionSyntax<TLifetime> Members

        public ConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null)
        {
            lifetimeSelection = GetDefaultLifetimeWhenNull(lifetimeSelection);
            return Define(syntax => rules(syntax).RegisterEach((registration) =>
                {
                    AutomaticRegistration(registration, lifetimeSelection(lifetimeSelector));
                    Exclude(registration);
                }));
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

        ITypeExclusion ITypeExclusion.Exclude(IRegistrationEntry registration)
        {
            Exclude(registration);
            return this;
        }

        ITypeExclusion ITypeExclusion.Exclude(IEnumerable<IRegistrationEntry> registrations)
        {
            throw new NotImplementedException();
        }

        protected void Exclude(Type serviceType)
        {
            registeredServices.Add(serviceType);
        }

        protected void Exclude(IEnumerable<Type> serviceTypes)
        {
            foreach (var serviceType in serviceTypes)
            {
                registeredServices.Add(serviceType);
            }
        }

        ITypeExclusion ITypeExclusion.Exclude(Type serviceType)
        {
            Exclude(serviceType);
            return this;
        }

        ITypeExclusion ITypeExclusion.Exclude(IEnumerable<Type> serviceTypes)
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
            foreach (var convention in conventions)
            {
                convention(new AssemblySelector());
            }
        }
    }
}
