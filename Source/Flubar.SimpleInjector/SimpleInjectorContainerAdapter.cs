using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SimpleInjector;
using SimpleInjector.Advanced;

namespace Flubar.SimpleInjector
{
    partial class SimpleInjectorContainerAdapter : ISimpleInjectorContainer
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

        public void Register(Type serviceType, Func<object> instanceCreator, Lifestyle lifetime = null)
        {
            container.Register(serviceType, instanceCreator, lifetime ?? GetDefaultLifetime());
            if (serviceType.IsInterface)
            {
                typeExclusionTracker.ExcludeService(serviceType);
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

        #region IContainer<Lifestyle> Members

        //public void RegisterFunc<T>()// move to extension method
        //    where T : class
        //{
        //    container.RegisterSingleton<Func<T>>(() => container.GetInstance<T>());
        //}

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

        public void RegisterSingleton<T>(T instance) where T : class
        {
            container.RegisterSingleton<T>(instance);
            if (typeof(T).IsInterface)
            {
                typeExclusionTracker.ExcludeService(typeof(T), instance.GetType());
            }
            else
            {
                typeExclusionTracker.ExcludeImplementation(instance.GetType());
            }
        }

        public void RegisterSingleton<TService, TImplementation>()
              where TService : class
              where TImplementation : class, TService
        {
            container.RegisterSingleton<TService, TImplementation>();
            typeExclusionTracker.ExcludeService(typeof(TService), typeof(TImplementation));
        }

        #endregion

        #region IContainer Members

        public InstanceProducer[] GetCurrentRegistrations()
        {
            throw new NotImplementedException();
        }

        public InstanceProducer[] GetRootRegistrations()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Register<TConcrete>() where TConcrete : class
        {
            throw new NotImplementedException();
        }

        void IContainer.Register<TService, TImplementation>()
        {
            throw new NotImplementedException();
        }

        public void Register<TService>(Func<TService> instanceCreator) where TService : class
        {
            throw new NotImplementedException();
        }

        public void Register(Type concreteType)
        {
            throw new NotImplementedException();
        }

        public void Register(Type serviceType, Type implementationType)
        {
            throw new NotImplementedException();
        }

        public void Register(Type serviceType, Func<object> instanceCreator)
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton(Type serviceType, object instance)
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton<TConcrete>() where TConcrete : class
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton<TService>(Func<TService> instanceCreator) where TService : class
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton(Type serviceType, Func<object> instanceCreator)
        {
            throw new NotImplementedException();
        }

        public void RegisterInitializer<TService>(Action<TService> instanceInitializer) where TService : class
        {
            throw new NotImplementedException();
        }

        public void RegisterInitializer(Action<InstanceInitializationData> instanceInitializer, Predicate<InitializationContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void AddRegistration(Type serviceType, Registration registration)
        {
            throw new NotImplementedException();
        }

        public TService GetInstance<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TService> GetAllInstances<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public InstanceProducer GetRegistration(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public InstanceProducer GetRegistration(Type serviceType, bool throwOnFailure)
        {
            throw new NotImplementedException();
        }

        void IContainer.RegisterConditional<TService, TImplementation>(Predicate<PredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        void IContainer.RegisterConditional<TService, TImplementation>(Lifestyle lifestyle, Predicate<PredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void RegisterConditional(Type serviceType, Type implementationType, Predicate<PredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void RegisterConditional(Type serviceType, Type implementationType, Lifestyle lifestyle, Predicate<PredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void RegisterConditional(Type serviceType, Func<TypeFactoryContext, Type> implementationTypeFactory, Lifestyle lifestyle, Predicate<PredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void RegisterConditional(Type serviceType, Registration registration, Predicate<PredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void Register(Type openGenericServiceType, IEnumerable<Assembly> assemblies)
        {
            throw new NotImplementedException();
        }

        public void Register(Type openGenericServiceType, IEnumerable<Assembly> assemblies, Lifestyle lifestyle)
        {
            throw new NotImplementedException();
        }

        public void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes)
        {
            throw new NotImplementedException();
        }

        public void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle)
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection<TService>(IEnumerable<Assembly> assemblies) where TService : class
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection(Type serviceType, params Assembly[] assemblies)
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection(Type serviceType, IEnumerable<Assembly> assemblies)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> GetTypesToRegister(Type serviceType, IEnumerable<Assembly> assemblies)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> GetTypesToRegister(Type serviceType, IEnumerable<Assembly> assemblies, TypesToRegisterOptions options)
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection<TService>(IEnumerable<TService> containerUncontrolledCollection) where TService : class
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection<TService>(params TService[] singletons) where TService : class
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection<TService>(IEnumerable<Registration> registrations) where TService : class
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection(Type serviceType, IEnumerable<Type> serviceTypes)
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection(Type serviceType, IEnumerable<Registration> registrations)
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection(Type serviceType, IEnumerable containerUncontrolledCollection)
        {
            throw new NotImplementedException();
        }

        public void RegisterDecorator<TService, TDecorator>()
            where TService : class
            where TDecorator : class, TService
        {
            throw new NotImplementedException();
        }

        public void RegisterDecorator<TService, TDecorator>(Lifestyle lifestyle)
        {
            throw new NotImplementedException();
        }

        public void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle)
        {
            throw new NotImplementedException();
        }

        public void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void RegisterDecorator(Type serviceType, Func<DecoratorPredicateContext, Type> decoratorTypeFactory, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void RegisterDecorator(Type serviceType, Type decoratorType, Predicate<DecoratorPredicateContext> predicate)
        {
            throw new NotImplementedException();
        }

        public void Verify()
        {
            throw new NotImplementedException();
        }

        public void Verify(VerificationOption option)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
