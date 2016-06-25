
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

      void AddRegistration(Type serviceType, Registration registration);

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

      void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes);

      void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle);

      void RegisterDecorator<TService, TDecorator>()
            where TService: class
            where TDecorator: class, TService;

      void RegisterDecorator<TService, TDecorator>(Lifestyle lifestyle);

      void RegisterDecorator(Type serviceType, Type decoratorType);

      void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle);

      void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate);

      void RegisterDecorator(Type serviceType, Type decoratorType, Predicate<DecoratorPredicateContext> predicate);
	}
}

