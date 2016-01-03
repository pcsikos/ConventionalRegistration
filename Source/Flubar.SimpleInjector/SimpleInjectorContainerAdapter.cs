using System;
using System.Collections.Generic;
using SimpleInjector;

namespace Flubar.SimpleInjector
{
    class SimpleInjectorContainerAdapter : ISimpleInjectorContainer
    {
        private readonly Container container;
        private readonly ITypeExclusionTracker typeExclusionTracker;

        public SimpleInjectorContainerAdapter(Container container, ITypeExclusionTracker typeExclusionTracker)
        {
            this.container = container;
            this.typeExclusionTracker = typeExclusionTracker;
        }

        internal Container Container
        {
            get
            {
                return container;
            }
        }

        #region IContainer<Lifestyle> Members

        public void Register<TService, TImplementation>(Lifestyle lifetime = null)
            where TService : class
            where TImplementation : class, TService
        {
            Register(typeof(TService), typeof(TImplementation), lifetime);
        }

        public void Register<TConcrete>(Lifestyle lifetime)
            where TConcrete : class
        {
            Register<TConcrete, TConcrete>(lifetime);
        }

        public void Register(Type serviceType, Type implementation, Lifestyle lifetime)
        {
            container.Register(serviceType, implementation, lifetime ?? GetDefaultLifetime());
            typeExclusionTracker.ExcludeService(serviceType, implementation);
            //container.RegisterConditional<>
        }

        public void RegisterAll(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifetime)
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
            typeExclusionTracker.ExcludeServices(serviceTypes, implementation);
        }

        public void Register<TService>(Func<TService> instanceCreator, Lifestyle lifetime = null) where TService : class
        {
            container.Register<TService>(instanceCreator, lifetime ?? GetDefaultLifetime());
            if (typeof(TService).IsInterface)
            {
                typeExclusionTracker.ExcludeService(typeof(TService));
            }
        }

        public Lifestyle GetSingletonLifetime()
        {
            return Lifestyle.Singleton;
        }

        public Lifestyle GetDefaultLifetime()
        {
            return Lifestyle.Transient;
        }

        #endregion

        #region ISimpleInjectorContainer Members

        public void RegisterFunc<T>()
            where T : class
        {
            container.RegisterSingleton<Func<T>>(() => container.GetInstance<T>());
        }

        public void RegisterDecorator(Type serviceType, Type decoratorType)
        {
            container.RegisterDecorator(serviceType, decoratorType);
            typeExclusionTracker.ExcludeImplementation(decoratorType, new[] { serviceType });
        }

        public void RegisterCollection<TService>(IEnumerable<Type> implementationTypes)
            where TService : class
        {
            container.RegisterCollection<TService>(implementationTypes);
        }

        #endregion
    }
}
