
using SimpleInjector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SimpleInjector.Advanced;

namespace Flubar.SimpleInjector
{
	public partial interface IContainer		
	{

      InstanceProducer[] GetCurrentRegistrations();

      InstanceProducer[] GetRootRegistrations();

      bool Equals(Object obj);

      int GetHashCode();

      string ToString();

      Type GetType();

      void Dispose();

      void Register<TConcrete>()
            where TConcrete: class;

      void Register<TConcrete>(Lifestyle lifestyle)
            where TConcrete: class;

      void Register<TService, TImplementation>()
            where TService: class
            where TImplementation: class, TService;

      void Register<TService, TImplementation>(Lifestyle lifestyle)
            where TService: class
            where TImplementation: class, TService;

      void Register<TService>(Func<TService> instanceCreator)
            where TService: class;

      void Register<TService>(Func<TService> instanceCreator, Lifestyle lifestyle)
            where TService: class;

      void Register(Type concreteType);

      void Register(Type serviceType, Type implementationType);

      void Register(Type serviceType, Type implementationType, Lifestyle lifestyle);

      void Register(Type serviceType, Func<Object> instanceCreator);

      void Register(Type serviceType, Func<Object> instanceCreator, Lifestyle lifestyle);

      void RegisterSingleton<TService>(TService instance)
            where TService: class;

      void RegisterSingleton(Type serviceType, Object instance);

      void RegisterSingleton<TConcrete>()
            where TConcrete: class;

      void RegisterSingleton<TService, TImplementation>()
            where TService: class
            where TImplementation: class, TService;

      void RegisterSingleton<TService>(Func<TService> instanceCreator)
            where TService: class;

      void RegisterSingleton(Type serviceType, Type implementationType);

      void RegisterSingleton(Type serviceType, Func<Object> instanceCreator);

      void RegisterInitializer<TService>(Action<TService> instanceInitializer)
            where TService: class;

      void RegisterInitializer(Action<InstanceInitializationData> instanceInitializer, Predicate<InitializationContext> predicate);

      void AddRegistration(Type serviceType, Registration registration);

      TService GetInstance<TService>()
            where TService: class;

      Object GetInstance(Type serviceType);

      IEnumerable<TService> GetAllInstances<TService>()
            where TService: class;

      IEnumerable<Object> GetAllInstances(Type serviceType);

      InstanceProducer GetRegistration(Type serviceType);

      InstanceProducer GetRegistration(Type serviceType, bool throwOnFailure);

      void RegisterConditional<TService, TImplementation>(Predicate<PredicateContext> predicate)
            where TService: class
            where TImplementation: class, TService;

      void RegisterConditional<TService, TImplementation>(Lifestyle lifestyle, Predicate<PredicateContext> predicate)
            where TService: class
            where TImplementation: class, TService;

      void RegisterConditional(Type serviceType, Type implementationType, Predicate<PredicateContext> predicate);

      void RegisterConditional(Type serviceType, Type implementationType, Lifestyle lifestyle, Predicate<PredicateContext> predicate);

      void RegisterConditional(Type serviceType, Func<TypeFactoryContext, Type> implementationTypeFactory, Lifestyle lifestyle, Predicate<PredicateContext> predicate);

      void RegisterConditional(Type serviceType, Registration registration, Predicate<PredicateContext> predicate);

      void Register(Type openGenericServiceType, IEnumerable<Assembly> assemblies);

      void Register(Type openGenericServiceType, IEnumerable<Assembly> assemblies, Lifestyle lifestyle);

      void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes);

      void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle);

      void RegisterCollection<TService>(IEnumerable<Assembly> assemblies)
            where TService: class;

      void RegisterCollection(Type serviceType, params Assembly[] assemblies);

      void RegisterCollection(Type serviceType, IEnumerable<Assembly> assemblies);

      IEnumerable<Type> GetTypesToRegister(Type serviceType, IEnumerable<Assembly> assemblies);

      IEnumerable<Type> GetTypesToRegister(Type serviceType, IEnumerable<Assembly> assemblies, TypesToRegisterOptions options);

      void RegisterCollection<TService>(IEnumerable<TService> containerUncontrolledCollection)
            where TService: class;

      void RegisterCollection<TService>(params TService[] singletons)
            where TService: class;

      void RegisterCollection<TService>(IEnumerable<Type> serviceTypes)
            where TService: class;

      void RegisterCollection<TService>(IEnumerable<Registration> registrations)
            where TService: class;

      void RegisterCollection(Type serviceType, IEnumerable<Type> serviceTypes);

      void RegisterCollection(Type serviceType, IEnumerable<Registration> registrations);

      void RegisterCollection(Type serviceType, IEnumerable containerUncontrolledCollection);

      void RegisterDecorator<TService, TDecorator>()
            where TService: class
            where TDecorator: class, TService;

      void RegisterDecorator<TService, TDecorator>(Lifestyle lifestyle);

      void RegisterDecorator(Type serviceType, Type decoratorType);

      void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle);

      void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate);

      void RegisterDecorator(Type serviceType, Func<DecoratorPredicateContext, Type> decoratorTypeFactory, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate);

      void RegisterDecorator(Type serviceType, Type decoratorType, Predicate<DecoratorPredicateContext> predicate);

      void Verify();

      void Verify(VerificationOption option);
	}
}

