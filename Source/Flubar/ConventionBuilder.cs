using System;
using System.Collections.Generic;
using System.Linq;
using Flubar.Syntax;

namespace Flubar
{
    public sealed class ConventionBuilder<TLifetime> : /*IConventionBuilder<TLifetime>,*/  ITypeExclusion
        where TLifetime : class
    {
        private readonly IContainerFacade<TLifetime> containerFacade;
        private readonly IList<Type> registeredImplementations;
        private readonly LifetimeSelector<TLifetime> lifetimeSelector;
        private readonly IList<Action<ISourceSyntax>> conventions;
        private readonly BehaviorConfiguration behaviorConfiguration;
        //private IList<Type> registeredServices;

        public ConventionBuilder(IContainerFacade<TLifetime> container, BehaviorConfiguration behaviorConfiguration)
        {
            registeredImplementations = new List<Type>();
            lifetimeSelector = new LifetimeSelector<TLifetime>(container);
            this.containerFacade = new ContainerDecorator<TLifetime>(container, type => registeredImplementations.Add(type));
            conventions = new List<Action<ISourceSyntax>>();
            this.behaviorConfiguration = behaviorConfiguration;
        }

        #region IConventionSyntax<TLifetime> Members

        public ConventionBuilder<TLifetime> DefineGlobal(Func<IEnumerable<Type>, Syntax.IRegisterSyntax> rules, Func<Syntax.ILifetimeSyntax<TLifetime>, TLifetime> lifetime = null)
        {
            throw new NotImplementedException();
        }

        public ConventionBuilder<TLifetime> DefineGlobal(Action<IEnumerable<Type>> rules, Func<Syntax.ILifetimeSyntax<TLifetime>, TLifetime> lifetime = null)
        {
            throw new NotImplementedException();
        }

        public ConventionBuilder<TLifetime> Define(Func<ISourceSyntax, IRegisterSyntax> rules, Func<ILifetimeSyntax<TLifetime>, TLifetime> lifetimeSelection = null)
        {
            lifetimeSelection = GetDefaultLifetimeWhenNull(lifetimeSelection);
            return Define(syntax => rules(syntax).RegisterEach((registration, notify) =>
                {
                    AutomaticRegistration(registration, lifetimeSelection(lifetimeSelector));
                    notify.Exclude(registration);
                }));
        }

        public ConventionBuilder<TLifetime> Define(Action<ISourceSyntax> convention)
        {

            conventions.Add(convention);
            return this;
        }

        public void Apply()
        {
            foreach (var convention in conventions)
            {
                convention(new AssemblySelector());
            }
        }

        public object Container
        {
            get { return containerFacade.InnerContainer; }
        }

        #endregion

        #region IConfigureConvention Members

        ITypeExclusion ITypeExclusion.Exclude(IRegistrationEntry registration)
        {
            throw new NotImplementedException();
        }

        ITypeExclusion ITypeExclusion.Exclude(IEnumerable<IRegistrationEntry> registrations)
        {
            throw new NotImplementedException();
        }

        ITypeExclusion ITypeExclusion.Exclude(Type serviceType, Type implementationType)
        {
            throw new NotImplementedException();
        }

        ITypeExclusion ITypeExclusion.Exclude(IEnumerable<Type> serviceTypes, Type implementationType)
        {
            throw new NotImplementedException();
        }

        ITypeExclusion ITypeExclusion.Exclude(Type implementationType)
        {
            throw new NotImplementedException();
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
    }
}
