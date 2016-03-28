
using SimpleInjector;
using System;
using SimpleInjector;
using System.Collections.Generic;
using SimpleInjector.Advanced;
using System.Reflection;
using System.Collections;

namespace Flubar.SimpleInjector
{
	partial class ContainerWrapper : IContainer
	{
		private readonly Container _container;

		public ContainerWrapper(Container container)
		{
			_container = container;
		}

		public virtual InstanceProducer[] GetCurrentRegistrations() 		
		{
			return _container.GetCurrentRegistrations();
		}
		public virtual InstanceProducer[] GetRootRegistrations() 		
		{
			return _container.GetRootRegistrations();
		}
		public override bool Equals(Object obj) 		
		{
			return _container.Equals(obj);
		}
		public override int GetHashCode() 		
		{
			return _container.GetHashCode();
		}
		public override string ToString() 		
		{
			return _container.ToString();
		}
		public new Type GetType() 		
		{
			return _container.GetType();
		}
		public virtual void Dispose() 		
		{
			_container.Dispose();
		}
		public virtual void Register<TConcrete>() 
            where TConcrete: class		
		{
			_container.Register<TConcrete>();
		}
		public virtual void Register<TConcrete>(Lifestyle lifestyle) 
            where TConcrete: class		
		{
			_container.Register<TConcrete>(lifestyle);
		}
		public virtual void Register<TService, TImplementation>() 
            where TService: class
            where TImplementation: class, TService		
		{
			_container.Register<TService, TImplementation>();
		}
		public virtual void Register<TService, TImplementation>(Lifestyle lifestyle) 
            where TService: class
            where TImplementation: class, TService		
		{
			_container.Register<TService, TImplementation>(lifestyle);
		}
		public virtual void Register<TService>(Func<TService> instanceCreator) 
            where TService: class		
		{
			_container.Register<TService>(instanceCreator);
		}
		public virtual void Register<TService>(Func<TService> instanceCreator, Lifestyle lifestyle) 
            where TService: class		
		{
			_container.Register<TService>(instanceCreator, lifestyle);
		}
		public virtual void Register(Type concreteType) 		
		{
			_container.Register(concreteType);
		}
		public virtual void Register(Type serviceType, Type implementationType) 		
		{
			_container.Register(serviceType, implementationType);
		}
		public virtual void Register(Type serviceType, Type implementationType, Lifestyle lifestyle) 		
		{
			_container.Register(serviceType, implementationType, lifestyle);
		}
		public virtual void Register(Type serviceType, Func<Object> instanceCreator) 		
		{
			_container.Register(serviceType, instanceCreator);
		}
		public virtual void Register(Type serviceType, Func<Object> instanceCreator, Lifestyle lifestyle) 		
		{
			_container.Register(serviceType, instanceCreator, lifestyle);
		}
		public virtual void RegisterSingleton<TService>(TService instance) 
            where TService: class		
		{
			_container.RegisterSingleton<TService>(instance);
		}
		public virtual void RegisterSingleton(Type serviceType, Object instance) 		
		{
			_container.RegisterSingleton(serviceType, instance);
		}
		public virtual void RegisterSingleton<TConcrete>() 
            where TConcrete: class		
		{
			_container.RegisterSingleton<TConcrete>();
		}
		public virtual void RegisterSingleton<TService, TImplementation>() 
            where TService: class
            where TImplementation: class, TService		
		{
			_container.RegisterSingleton<TService, TImplementation>();
		}
		public virtual void RegisterSingleton<TService>(Func<TService> instanceCreator) 
            where TService: class		
		{
			_container.RegisterSingleton<TService>(instanceCreator);
		}
		public virtual void RegisterSingleton(Type serviceType, Type implementationType) 		
		{
			_container.RegisterSingleton(serviceType, implementationType);
		}
		public virtual void RegisterSingleton(Type serviceType, Func<Object> instanceCreator) 		
		{
			_container.RegisterSingleton(serviceType, instanceCreator);
		}
		public virtual void RegisterInitializer<TService>(Action<TService> instanceInitializer) 
            where TService: class		
		{
			_container.RegisterInitializer<TService>(instanceInitializer);
		}
		public virtual void RegisterInitializer(Action<SimpleInjector.Advanced.InstanceInitializationData> instanceInitializer, Predicate<InitializationContext> predicate) 		
		{
			_container.RegisterInitializer(instanceInitializer, predicate);
		}
		public virtual void AddRegistration(Type serviceType, Registration registration) 		
		{
			_container.AddRegistration(serviceType, registration);
		}
		public virtual TService GetInstance<TService>() 
            where TService: class		
		{
			return _container.GetInstance<TService>();
		}
		public virtual Object GetInstance(Type serviceType) 		
		{
			return _container.GetInstance(serviceType);
		}
		public virtual IEnumerable<TService> GetAllInstances<TService>() 
            where TService: class		
		{
			return _container.GetAllInstances<TService>();
		}
		public virtual IEnumerable<Object> GetAllInstances(Type serviceType) 		
		{
			return _container.GetAllInstances(serviceType);
		}
		public virtual InstanceProducer GetRegistration(Type serviceType) 		
		{
			return _container.GetRegistration(serviceType);
		}
		public virtual InstanceProducer GetRegistration(Type serviceType, bool throwOnFailure) 		
		{
			return _container.GetRegistration(serviceType, throwOnFailure);
		}
		public virtual void RegisterConditional<TService, TImplementation>(Predicate<PredicateContext> predicate) 
            where TService: class
            where TImplementation: class, TService		
		{
			_container.RegisterConditional<TService, TImplementation>(predicate);
		}
		public virtual void RegisterConditional<TService, TImplementation>(Lifestyle lifestyle, Predicate<PredicateContext> predicate) 
            where TService: class
            where TImplementation: class, TService		
		{
			_container.RegisterConditional<TService, TImplementation>(lifestyle, predicate);
		}
		public virtual void RegisterConditional(Type serviceType, Type implementationType, Predicate<PredicateContext> predicate) 		
		{
			_container.RegisterConditional(serviceType, implementationType, predicate);
		}
		public virtual void RegisterConditional(Type serviceType, Type implementationType, Lifestyle lifestyle, Predicate<PredicateContext> predicate) 		
		{
			_container.RegisterConditional(serviceType, implementationType, lifestyle, predicate);
		}
		public virtual void RegisterConditional(Type serviceType, Func<TypeFactoryContext, Type> implementationTypeFactory, Lifestyle lifestyle, Predicate<PredicateContext> predicate) 		
		{
			_container.RegisterConditional(serviceType, implementationTypeFactory, lifestyle, predicate);
		}
		public virtual void RegisterConditional(Type serviceType, Registration registration, Predicate<PredicateContext> predicate) 		
		{
			_container.RegisterConditional(serviceType, registration, predicate);
		}
		public virtual void Register(Type openGenericServiceType, IEnumerable<Assembly> assemblies) 		
		{
			_container.Register(openGenericServiceType, assemblies);
		}
		public virtual void Register(Type openGenericServiceType, IEnumerable<Assembly> assemblies, Lifestyle lifestyle) 		
		{
			_container.Register(openGenericServiceType, assemblies, lifestyle);
		}
		public virtual void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes) 		
		{
			_container.Register(openGenericServiceType, implementationTypes);
		}
		public virtual void Register(Type openGenericServiceType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle) 		
		{
			_container.Register(openGenericServiceType, implementationTypes, lifestyle);
		}
		public virtual void RegisterCollection<TService>(IEnumerable<Assembly> assemblies) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(assemblies);
		}
		public virtual void RegisterCollection(Type serviceType, params Assembly[] assemblies) 		
		{
			_container.RegisterCollection(serviceType, assemblies);
		}
		public virtual void RegisterCollection(Type serviceType, IEnumerable<Assembly> assemblies) 		
		{
			_container.RegisterCollection(serviceType, assemblies);
		}
		public virtual IEnumerable<Type> GetTypesToRegister(Type serviceType, IEnumerable<Assembly> assemblies) 		
		{
			return _container.GetTypesToRegister(serviceType, assemblies);
		}
		public virtual IEnumerable<Type> GetTypesToRegister(Type serviceType, IEnumerable<Assembly> assemblies, TypesToRegisterOptions options) 		
		{
			return _container.GetTypesToRegister(serviceType, assemblies, options);
		}
		public virtual void RegisterCollection<TService>(IEnumerable<TService> containerUncontrolledCollection) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(containerUncontrolledCollection);
		}
		public virtual void RegisterCollection<TService>(params TService[] singletons) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(singletons);
		}
		public virtual void RegisterCollection<TService>(IEnumerable<Type> serviceTypes) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(serviceTypes);
		}
		public virtual void RegisterCollection<TService>(IEnumerable<Registration> registrations) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(registrations);
		}
		public virtual void RegisterCollection(Type serviceType, IEnumerable<Type> serviceTypes) 		
		{
			_container.RegisterCollection(serviceType, serviceTypes);
		}
		public virtual void RegisterCollection(Type serviceType, IEnumerable<Registration> registrations) 		
		{
			_container.RegisterCollection(serviceType, registrations);
		}
		public virtual void RegisterCollection(Type serviceType, IEnumerable containerUncontrolledCollection) 		
		{
			_container.RegisterCollection(serviceType, containerUncontrolledCollection);
		}
		public virtual void RegisterDecorator<TService, TDecorator>() 
            where TService: class
            where TDecorator: class, TService		
		{
			_container.RegisterDecorator<TService, TDecorator>();
		}
		public virtual void RegisterDecorator<TService, TDecorator>(Lifestyle lifestyle) 		
		{
			_container.RegisterDecorator<TService, TDecorator>(lifestyle);
		}
		public virtual void RegisterDecorator(Type serviceType, Type decoratorType) 		
		{
			_container.RegisterDecorator(serviceType, decoratorType);
		}
		public virtual void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle) 		
		{
			_container.RegisterDecorator(serviceType, decoratorType, lifestyle);
		}
		public virtual void RegisterDecorator(Type serviceType, Type decoratorType, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate) 		
		{
			_container.RegisterDecorator(serviceType, decoratorType, lifestyle, predicate);
		}
		public virtual void RegisterDecorator(Type serviceType, Func<DecoratorPredicateContext, Type> decoratorTypeFactory, Lifestyle lifestyle, Predicate<DecoratorPredicateContext> predicate) 		
		{
			_container.RegisterDecorator(serviceType, decoratorTypeFactory, lifestyle, predicate);
		}
		public virtual void RegisterDecorator(Type serviceType, Type decoratorType, Predicate<DecoratorPredicateContext> predicate) 		
		{
			_container.RegisterDecorator(serviceType, decoratorType, predicate);
		}
		public virtual void Verify() 		
		{
			_container.Verify();
		}
		public virtual void Verify(VerificationOption option) 		
		{
			_container.Verify(option);
		}
	}
	public partial interface IContainer		
	{

      InstanceProducer[] GetCurrentRegistrations();

      InstanceProducer[] GetRootRegistrations();

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

      void RegisterInitializer(Action<SimpleInjector.Advanced.InstanceInitializationData> instanceInitializer, Predicate<InitializationContext> predicate);

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

