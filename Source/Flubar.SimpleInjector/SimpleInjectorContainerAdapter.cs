using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SimpleInjector;
using SimpleInjector.Advanced;

namespace Flubar.SimpleInjector
{
    partial class SimpleInjectorContainerAdapterAdapter : ISimpleInjectorContainerAdapter
    {
        private readonly Container container;
        private readonly ITypeExclusionTracker typeExclusionTracker;

        public SimpleInjectorContainerAdapterAdapter(Container container, ITypeExclusionTracker typeExclusionTracker)
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

        //void IContainer<Lifestyle>.Register<TService, TImplementation>(Lifestyle lifetime = null)
        //    //where TService : class
        //    //where TImplementation : class, TService
        //{
        //    Register(typeof(TService), typeof(TImplementation), lifetime);
        //}

        //void IContainer<Lifestyle>.Register<TConcrete>(Lifestyle lifetime)
        //    where TConcrete : class
        //{
        //    Register<TConcrete, TConcrete>(lifetime);
        //}

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

        //void IContainer<Lifestyle>.Register(Type serviceType, Func<object> instanceCreator, Lifestyle lifetime = null)
        //{
        //    container.Register(serviceType, instanceCreator, lifetime ?? GetDefaultLifetime());
        //    if (serviceType.IsInterface)
        //    {
        //        typeExclusionTracker.ExcludeService(serviceType);
        //    }
        //}

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

        //public void RegisterCollection<TService>(IEnumerable<Type> implementationTypes)
        //    where TService : class
        //{
        //    container.RegisterCollection<TService>(implementationTypes);
        //}

        #endregion

        #region Contianer members

        public InstanceProducer[] GetCurrentRegistrations()
        {
            return container.GetCurrentRegistrations();
        }
        public InstanceProducer[] GetRootRegistrations()
        {
            return container.GetRootRegistrations();
        }
        public override bool Equals(Object obj)
        {
            return container.Equals(obj);
        }
        public override int GetHashCode()
        {
            return container.GetHashCode();
        }
        public override string ToString()
        {
            return container.ToString();
        }
        public new Type GetType()
        {
            return container.GetType();
        }
        public void Dispose()
        {
            container.Dispose();
        }
        public void Register<TConcrete>()
            where TConcrete : class
        {
            container.Register<TConcrete>();
            typeExclusionTracker.ExcludeService(typeof(TConcrete), typeof(TConcrete));
        }
        public void Register<TConcrete>(Lifestyle lifestyle)
            where TConcrete : class
        {
            container.Register<TConcrete>(lifestyle);
            typeExclusionTracker.ExcludeService(typeof(TConcrete), typeof(TConcrete));
        }
        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>();
            typeExclusionTracker.ExcludeService(typeof(TService), typeof(TImplementation));
        }
        public void Register<TService, TImplementation>(Lifestyle lifestyle)
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>(lifestyle);
            typeExclusionTracker.ExcludeService(typeof(TService), typeof(TImplementation));
        }
        public void Register<TService>(Func<TService> instanceCreator)
            where TService : class
        {
            container.Register<TService>(instanceCreator);
        }
        public void Register<TService>(Func<TService> instanceCreator, Lifestyle lifestyle)
            where TService : class
        {
            container.Register<TService>(instanceCreator, lifestyle);
            if (typeof(TService).IsInterface)
            {
                typeExclusionTracker.ExcludeService(typeof(TService));
            }
        }
        public void Register(Type concreteType)
        {
            container.Register(concreteType);
        }
        public void Register(Type serviceType, Type implementationType)
        {
            container.Register(serviceType, implementationType);
        }
        public void Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            container.Register(serviceType, implementationType, lifestyle);
        }
        public void Register(Type serviceType, Func<Object> instanceCreator)
        {
            container.Register(serviceType, instanceCreator);
        }
        public void Register(Type serviceType, Func<Object> instanceCreator, Lifestyle lifestyle)
        {
            container.Register(serviceType, instanceCreator, lifestyle);
        }
        public void RegisterSingleton<TService>(TService instance)
            where TService : class
        {
            container.RegisterSingleton<TService>(instance);
            if (typeof(TService).IsInterface)
            {
                typeExclusionTracker.ExcludeService(typeof(TService), instance.GetType());
            }
            else
            {
                typeExclusionTracker.ExcludeImplementation(instance.GetType());
            }


        }
        public void RegisterSingleton(Type serviceType, Object instance)
        {
            container.RegisterSingleton(serviceType, instance);
        }
        public void RegisterSingleton<TConcrete>()
            where TConcrete : class
        {
            container.RegisterSingleton<TConcrete>();
        }
        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterSingleton<TService, TImplementation>();
            typeExclusionTracker.ExcludeService(typeof(TService), typeof(TImplementation));
        }
        public void RegisterSingleton<TService>(Func<TService> instanceCreator)
            where TService : class
        {
            container.RegisterSingleton<TService>(instanceCreator);
        }
        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            container.RegisterSingleton(serviceType, implementationType);
        }
        public void RegisterSingleton(Type serviceType, Func<Object> instanceCreator)
        {
            container.RegisterSingleton(serviceType, instanceCreator);
        }
        public void RegisterInitializer<TService>(Action<TService> instanceInitializer)
            where TService : class
        {
            container.RegisterInitializer<TService>(instanceInitializer);
        }
        public void RegisterInitializer(Action<InstanceInitializationData> instanceInitializer, Predicate<InitializationContext> predicate)
        {
            container.RegisterInitializer(instanceInitializer, predicate);
        }
        public void AddRegistration(Type serviceType, Registration registration)
        {
            container.AddRegistration(serviceType, registration);
        }
        public TService GetInstance<TService>()
            where TService : class
        {
            return container.GetInstance<TService>();
        }
        public Object GetInstance(Type serviceType)
        {
            return container.GetInstance(serviceType);
        }
        public IEnumerable<TService> GetAllInstances<TService>()
            where TService : class
        {
            return container.GetAllInstances<TService>();
        }
        public IEnumerable<Object> GetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }
        public InstanceProducer GetRegistration(Type serviceType)
        {
            return container.GetRegistration(serviceType);
        }
        public InstanceProducer GetRegistration(Type serviceType, bool throwOnFailure)
        {
            return container.GetRegistration(serviceType, throwOnFailure);
        }
        public void RegisterConditional<TService, TImplementation>(Predicate<PredicateContext> predicate)
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterConditional<TService, TImplementation>(predicate);
        }
        public void RegisterConditional<TService, TImplementation>(Lifestyle lifestyle, Predicate<PredicateContext> predicate)
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterConditional<TService, TImplementation>(lifestyle, predicate);
        }
        public void RegisterConditional(Type serviceType, Type implementationType, Predicate<PredicateContext> predicate)
        {
            container.RegisterConditional(serviceType, implementationType, predicate);
        }
        public void RegisterConditional(Type serviceType, Type implementationType, Lifestyle lifestyle, Predicate<PredicateContext> predicate)
        {
            container.RegisterConditional(serviceType, implementationType, lifestyle, predicate);
        }
        public void RegisterConditional(Type serviceType, Func<TypeFactoryContext, Type> implementationTypeFactory, Lifestyle lifestyle, Predicate<PredicateContext> predicate)
        {
            container.RegisterConditional(serviceType, implementationTypeFactory, lifestyle, predicate);
        }
        public void RegisterConditional(Type serviceType, Registration registration, Predicate<PredicateContext> predicate)
        {
            container.RegisterConditional(serviceType, registration, predicate);
        }
        public void Register(Type openGenericServiceType, IEnumerable<Assembly> assemblies)
        {
            container.Register(openGenericServiceType, assemblies);
        }
        public void Register(Type openGenericServiceType, IEnumerable<Assembly> assemblies, Lifestyle lifestyle)
        {
            container.Register(openGenericServiceType, assemblies, lifestyle);
        }
        public void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes)
        {
            container.Register(openGenericServiceType, implementationTypes);
        }
        public void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle)
        {
            container.Register(openGenericServiceType, implementationTypes, lifestyle);
        }
        public void RegisterCollection<TService>(IEnumerable<Assembly> assemblies)
            where TService : class
        {
            container.RegisterCollection<TService>(assemblies);
        }
        public void RegisterCollection(Type serviceType, params Assembly[] assemblies)
        {
            container.RegisterCollection(serviceType, assemblies);
        }
        public void RegisterCollection(Type serviceType, IEnumerable<Assembly> assemblies)
        {
            container.RegisterCollection(serviceType, assemblies);
        }
        public IEnumerable<Type> GetTypesToRegister(Type serviceType, IEnumerable<Assembly> assemblies)
        {
            return container.GetTypesToRegister(serviceType, assemblies);
        }
        public IEnumerable<Type> GetTypesToRegister(Type serviceType, IEnumerable<Assembly> assemblies, TypesToRegisterOptions options)
        {
            return container.GetTypesToRegister(serviceType, assemblies, options);
        }
        public void RegisterCollection<TService>(IEnumerable<TService> containerUncontrolledCollection)
            where TService : class
        {
            container.RegisterCollection<TService>(containerUncontrolledCollection);
        }
        public void RegisterCollection<TService>(params TService[] singletons)
            where TService : class
        {
            container.RegisterCollection<TService>(singletons);
        }
        public void RegisterCollection<TService>(IEnumerable<Type> serviceTypes)
            where TService : class
        {
            container.RegisterCollection<TService>(serviceTypes);
        }
        public void RegisterCollection<TService>(IEnumerable<Registration> registrations)
            where TService : class
        {
            container.RegisterCollection<TService>(registrations);
        }
        public void RegisterCollection(Type serviceType, IEnumerable<Type> serviceTypes)
        {
            container.RegisterCollection(serviceType, serviceTypes);
        }
        public void RegisterCollection(Type serviceType, IEnumerable<Registration> registrations)
        {
            container.RegisterCollection(serviceType, registrations);
        }
        public void RegisterCollection(Type serviceType, IEnumerable containerUncontrolledCollection)
        {
            container.RegisterCollection(serviceType, containerUncontrolledCollection);
        }
        public void RegisterDecorator<TService, TDecorator>()
            where TService : class
            where TDecorator : class, TService
        {
            container.RegisterDecorator<TService, TDecorator>();
        }
        public void RegisterDecorator<TService, TDecorator>(Lifestyle lifestyle)
        {
            container.RegisterDecorator<TService, TDecorator>(lifestyle);
        }
        public void RegisterDecorator(Type serviceType, Type decoratorType)
        {
            container.RegisterDecorator(serviceType, decoratorType);
            typeExclusionTracker.ExcludeImplementation(decoratorType, new[] { serviceType });
        }
        public void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle)
        {
            container.RegisterDecorator(serviceType, decoratorType, lifestyle);
        }
        public void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate)
        {
            container.RegisterDecorator(serviceType, decoratorType, lifestyle, predicate);
        }
        public void RegisterDecorator(Type serviceType, Func<DecoratorPredicateContext, Type> decoratorTypeFactory, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate)
        {
            container.RegisterDecorator(serviceType, decoratorTypeFactory, lifestyle, predicate);
        }
        public void RegisterDecorator(Type serviceType, Type decoratorType, Predicate<DecoratorPredicateContext> predicate)
        {
            container.RegisterDecorator(serviceType, decoratorType, predicate);
        }
        public void Verify()
        {
            container.Verify();
        }
        public void Verify(VerificationOption option)
        {
            container.Verify(option);
        }
        #endregion
    }
}
