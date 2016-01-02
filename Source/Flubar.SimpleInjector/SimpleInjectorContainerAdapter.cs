using System;
using System.Collections.Generic;
using SimpleInjector;

namespace Flubar.SimpleInjector
{
    class SimpleInjectorContainerAdapter : AbstractContainer<Lifestyle>, ISimpleInjectorContainer
    {
        private readonly Container container;
        public SimpleInjectorContainerAdapter(Container container)
        {
            this.container = container;
        }

        #region IContainer<Lifestyle> Members

        public override void Register(Type serviceType, Type implementation, Lifestyle lifetime)
        {
            container.Register(serviceType, implementation, lifetime ?? GetDefaultLifetime());
            OnServiceRegistered(new RegistrationEventArgs(serviceType, implementation));
            //System.Diagnostics.Debug.WriteLine("{0} => {1} ({2})", serviceType, implementation, lifetime);
        }

        public override void Register(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifetime)
        {
            if (lifetime == null)
            {
                lifetime = GetDefaultLifetime();
            }
            var registration = lifetime.CreateRegistration(implementation, container);
            foreach (var type in serviceTypes)
            {
                container.AddRegistration(type, registration);
            }
            OnServiceRegistered(new RegistrationEventArgs(serviceTypes, implementation));
            //System.Diagnostics.Debug.WriteLine("{0} => {1} ({2})", string.Join(", ", serviceTypes.Select(x => x.Name).ToArray()), implementation, lifetime);
        }

        public override void Register<TService>(Func<TService> instanceCreator, Lifestyle lifetime = null)
        {
            container.Register<TService>(instanceCreator, lifetime ?? GetDefaultLifetime());
            if (typeof(TService).IsInterface)
            {
                OnServiceRegistered(new RegistrationEventArgs(typeof(TService)));
            }
            //todo: if tservice is interface exclude future registration for this service

        }

        public override Lifestyle GetSingletonLifetime()
        {
            return Lifestyle.Singleton;
        }

        public override Lifestyle GetDefaultLifetime()
        {
            return Lifestyle.Transient;
        }

        //public Container InnerContainer
        //{
        //    get { return container; }
        //}

        //object IContainerFacade<Lifestyle>.InnerContainer
        //{
        //    get { return container; }
        //}

        #endregion

        #region ISimpleInjectorContainer Members

        //public void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifestyle)
        //{
        //    var registration = lifestyle.CreateRegistration(implementation, container);
        //    foreach (var serviceType in serviceTypes.Where(t => t != typeof(IDisposable)))
        //    {
        //        container.AddRegistration(serviceType, registration);
        //    }
        //    OnServiceRegistered(new RegistrationEventArgs(serviceTypes, implementation));
        //    //todo: ExcludeService(serviceTypes, implementation);
        //}

        public void RegisterFunc<T>()
            where T : class
        {
            container.RegisterSingleton<Func<T>>(() => container.GetInstance<T>());
        }

        public void RegisterDecorator(Type serviceType, Type decoratorType)
        {
            container.RegisterDecorator(serviceType, decoratorType);
            OnImplementationExcluded(new ImplementationExcludedEventArgs(decoratorType, serviceType));
        }

        public void RegisterCollection<TService>(IEnumerable<Type> implementationTypes)
            where TService : class
        {
            container.RegisterCollection<TService>(implementationTypes);
            //foreach (var implementationType in implementationTypes)
            //{
            //    OnServiceRegistered(new RegistrationEventArgs(typeof(TService), implementationType));
            //}
        }

        #endregion

    }
}
