using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleInjector;

namespace Flubar.SimpleInjector
{
    partial class SimpleInjectorContainerAdapter : ISimpleInjectorContainerAdapter
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

        private void ExcludeService(Type serviceType, Type implementation = null)
        {
            typeExclusionTracker.ExcludeService(serviceType, implementation);
        }

        private void ExcludeService<TService>()
        {
            ExcludeService(typeof(TService));
        }

        private void ExcludeImplementation(Type implementationType, Type serviceType = null)
        {
            typeExclusionTracker.ExcludeImplementation(implementationType, new[] { serviceType });
        }

        private void ExcludeImplementation<TImplementation>()
        {
            ExcludeImplementation(typeof(TImplementation));
        }

        #region IContainer<Lifestyle> Members

        void IContainer<Lifestyle>.RegisterType(Type serviceType, Type implementation, Lifestyle lifetime)
        {
            container.Register(serviceType, implementation, lifetime ?? GetDefaultLifetime());
            typeExclusionTracker.ExcludeService(serviceType, implementation);
            //container.RegisterConditional<>
        }

        void IContainer<Lifestyle>.RegisterAll(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifetime)
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

        public Lifestyle GetSingletonLifetime()
        {
            return Lifestyle.Singleton;
        }

        public Lifestyle GetDefaultLifetime()
        {
            return Lifestyle.Transient;
        }

        #endregion

        #region Container members

        public InstanceProducer[] GetCurrentRegistrations()
        {
            return container.GetCurrentRegistrations();
        }
        public InstanceProducer[] GetRootRegistrations()
        {
            return container.GetRootRegistrations();
        }

        public void Register<TConcrete>()
            where TConcrete : class
        {
            container.Register<TConcrete>();
            ExcludeService(typeof(TConcrete), typeof(TConcrete));
        }

        public void Register<TConcrete>(Lifestyle lifestyle)
            where TConcrete : class
        {
            container.Register<TConcrete>(lifestyle);
            ExcludeService(typeof(TConcrete), typeof(TConcrete));
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>();
            ExcludeService(typeof(TService), typeof(TImplementation));
        }
        public void Register<TService, TImplementation>(Lifestyle lifestyle)
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>(lifestyle);
            ExcludeService(typeof(TService), typeof(TImplementation));
        }
        public void Register<TService>(Func<TService> instanceCreator)
            where TService : class
        {
            container.Register<TService>(instanceCreator);
            ExcludeService<TService>();
        }
        public void Register<TService>(Func<TService> instanceCreator, Lifestyle lifestyle)
            where TService : class
        {
            container.Register<TService>(instanceCreator, lifestyle);
            ExcludeService<TService>();
        }
        public void Register(Type concreteType)
        {
            container.Register(concreteType);
            ExcludeImplementation(concreteType);
        }
        public void Register(Type serviceType, Type implementationType)
        {
            container.Register(serviceType, implementationType);
            ExcludeService(serviceType, implementationType);
        }
        public void Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            container.Register(serviceType, implementationType, lifestyle);
            ExcludeService(serviceType, implementationType);
        }
        public void Register(Type serviceType, Func<Object> instanceCreator)
        {
            container.Register(serviceType, instanceCreator);
            ExcludeService(serviceType);
        }
        public void Register(Type serviceType, Func<Object> instanceCreator, Lifestyle lifestyle)
        {
            container.Register(serviceType, instanceCreator, lifestyle);
            ExcludeService(serviceType);
        }
        public void RegisterSingleton<TService>(TService instance)
            where TService : class
        {
            container.RegisterSingleton<TService>(instance);
            ExcludeService(typeof(TService), instance.GetType());
        }
        public void RegisterSingleton(Type serviceType, Object instance)
        {
            container.RegisterSingleton(serviceType, instance);
            ExcludeService(serviceType, instance.GetType());
        }
        public void RegisterSingleton<TConcrete>()
            where TConcrete : class
        {
            container.RegisterSingleton<TConcrete>();
            ExcludeService(typeof(TConcrete), typeof(TConcrete));
        }
        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterSingleton<TService, TImplementation>();
            ExcludeService(typeof(TService), typeof(TImplementation));
        }
        public void RegisterSingleton<TService>(Func<TService> instanceCreator)
            where TService : class
        {
            container.RegisterSingleton<TService>(instanceCreator);
            ExcludeService<TService>();
        }
        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            container.RegisterSingleton(serviceType, implementationType);
            ExcludeService(serviceType, implementationType);
        }
        public void RegisterSingleton(Type serviceType, Func<Object> instanceCreator)
        {
            container.RegisterSingleton(serviceType, instanceCreator);
            ExcludeService(serviceType);
        }
        public void AddRegistration(Type serviceType, Registration registration)
        {
            container.AddRegistration(serviceType, registration);
            ExcludeService(serviceType, registration.ImplementationType);
        }
        public void RegisterConditional<TService, TImplementation>(Predicate<PredicateContext> predicate)
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterConditional<TService, TImplementation>(predicate);
            ExcludeService(typeof(TService), typeof(TImplementation));
        }
        public void RegisterConditional<TService, TImplementation>(Lifestyle lifestyle, Predicate<PredicateContext> predicate)
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterConditional<TService, TImplementation>(lifestyle, predicate);
            ExcludeService(typeof(TService), typeof(TImplementation));
        }
        public void RegisterConditional(Type serviceType, Type implementationType, Predicate<PredicateContext> predicate)
        {
            container.RegisterConditional(serviceType, implementationType, predicate);
            ExcludeService(serviceType, implementationType);
        }
        public void RegisterConditional(Type serviceType, Type implementationType, Lifestyle lifestyle, Predicate<PredicateContext> predicate)
        {
            container.RegisterConditional(serviceType, implementationType, lifestyle, predicate);
            ExcludeService(serviceType, implementationType);
        }
        public void RegisterConditional(Type serviceType, Func<TypeFactoryContext, Type> implementationTypeFactory, Lifestyle lifestyle, Predicate<PredicateContext> predicate)
        {
            container.RegisterConditional(serviceType, implementationTypeFactory, lifestyle, predicate);
            ExcludeService(serviceType);
        }
        public void RegisterConditional(Type serviceType, Registration registration, Predicate<PredicateContext> predicate)
        {
            container.RegisterConditional(serviceType, registration, predicate);
            ExcludeService(serviceType, registration.ImplementationType);
        }
        public void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes)
        {
            container.Register(openGenericServiceType, implementationTypes);
        }
        public void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle)
        {
            container.Register(openGenericServiceType, implementationTypes, lifestyle);
        }
        public void RegisterDecorator<TService, TDecorator>()
            where TService : class
            where TDecorator : class, TService
        {
            container.RegisterDecorator<TService, TDecorator>();
            ExcludeImplementation(typeof(TDecorator), typeof(TService));
        }
        public void RegisterDecorator<TService, TDecorator>(Lifestyle lifestyle)
        {
            container.RegisterDecorator<TService, TDecorator>(lifestyle);
            ExcludeImplementation(typeof(TDecorator), typeof(TService));
        }
        public void RegisterDecorator(Type serviceType, Type decoratorType)
        {
            container.RegisterDecorator(serviceType, decoratorType);
            ExcludeImplementation(decoratorType, serviceType);
        }
        public void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle)
        {
            container.RegisterDecorator(serviceType, decoratorType, lifestyle);
            ExcludeImplementation(decoratorType, serviceType);
        }
        public void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate)
        {
            container.RegisterDecorator(serviceType, decoratorType, lifestyle, predicate);
            ExcludeImplementation(decoratorType, serviceType);
        }
        public void RegisterDecorator(Type serviceType, Type decoratorType, Predicate<DecoratorPredicateContext> predicate)
        {
            container.RegisterDecorator(serviceType, decoratorType, predicate);
            ExcludeImplementation(decoratorType, serviceType);
        }
        #endregion
    }
}
