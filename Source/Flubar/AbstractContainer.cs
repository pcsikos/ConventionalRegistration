using System;
using System.Collections.Generic;

namespace Flubar
{
    public abstract class AbstractContainer<TLifetime> : IContainer<TLifetime>
        where TLifetime : class
    {
        public event EventHandler<RegistrationEventArgs> RegistrationCreated;
        public event EventHandler<ImplementationExcludedEventArgs> ImplementationExcluded;

        public void Register<TService, TImplementation>(TLifetime lifetime = null)
            where TService : class
            where TImplementation : class, TService
        {
            Register(typeof(TService), typeof(TImplementation), lifetime);
        }

        public void Register<TConcrete>(TLifetime lifetime)
            where TConcrete : class
        {
            Register<TConcrete, TConcrete>(lifetime);
        }

        public abstract void Register<TService>(Func<TService> instanceCreator, TLifetime lifetime = null) where TService : class;
        public abstract void Register(Type serviceType, Type implementation, TLifetime lifetime = null);
        public abstract void Register(IEnumerable<Type> serviceTypes, Type implementation, TLifetime lifetime = null);
        public abstract TLifetime GetSingletonLifetime();
        public abstract TLifetime GetDefaultLifetime();

        protected virtual void OnServiceRegistered(RegistrationEventArgs e)
        {
            if (RegistrationCreated != null)
            {
                RegistrationCreated(this, e);
            }
        }

        protected virtual void OnImplementationExcluded(ImplementationExcludedEventArgs e)
        {
            if (ImplementationExcluded != null)
            {
                ImplementationExcluded(this, e);
            }
        }
    }
}
