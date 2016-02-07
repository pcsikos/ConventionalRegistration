
using SimpleInjector;

namespace SimpleInjector
{
	public partial class ContainerWrapper : IContainer
	{
		private readonly Container _container;

		public ContainerWrapper(Container container)
		{
			_container = container;
		}

		public virtual SimpleInjector.InstanceProducer[] GetCurrentRegistrations() 		
		{
			return _container.GetCurrentRegistrations();
		}
		public virtual SimpleInjector.InstanceProducer[] GetRootRegistrations() 		
		{
			return _container.GetRootRegistrations();
		}
		public override bool Equals(System.Object obj) 		
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
		public new System.Type GetType() 		
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
		public virtual void Register<TConcrete>(SimpleInjector.Lifestyle lifestyle) 
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
		public virtual void Register<TService, TImplementation>(SimpleInjector.Lifestyle lifestyle) 
            where TService: class
            where TImplementation: class, TService		
		{
			_container.Register<TService, TImplementation>(lifestyle);
		}
		public virtual void Register<TService>(System.Func<TService> instanceCreator) 
            where TService: class		
		{
			_container.Register<TService>(instanceCreator);
		}
		public virtual void Register<TService>(System.Func<TService> instanceCreator, SimpleInjector.Lifestyle lifestyle) 
            where TService: class		
		{
			_container.Register<TService>(instanceCreator, lifestyle);
		}
		public virtual void Register(System.Type concreteType) 		
		{
			_container.Register(concreteType);
		}
		public virtual void Register(System.Type serviceType, System.Type implementationType) 		
		{
			_container.Register(serviceType, implementationType);
		}
		public virtual void Register(System.Type serviceType, System.Type implementationType, SimpleInjector.Lifestyle lifestyle) 		
		{
			_container.Register(serviceType, implementationType, lifestyle);
		}
		public virtual void Register(System.Type serviceType, System.Func<System.Object> instanceCreator) 		
		{
			_container.Register(serviceType, instanceCreator);
		}
		public virtual void Register(System.Type serviceType, System.Func<System.Object> instanceCreator, SimpleInjector.Lifestyle lifestyle) 		
		{
			_container.Register(serviceType, instanceCreator, lifestyle);
		}
		public virtual void RegisterSingleton<TService>(TService instance) 
            where TService: class		
		{
			_container.RegisterSingleton<TService>(instance);
		}
		public virtual void RegisterSingleton(System.Type serviceType, System.Object instance) 		
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
		public virtual void RegisterSingleton<TService>(System.Func<TService> instanceCreator) 
            where TService: class		
		{
			_container.RegisterSingleton<TService>(instanceCreator);
		}
		public virtual void RegisterSingleton(System.Type serviceType, System.Type implementationType) 		
		{
			_container.RegisterSingleton(serviceType, implementationType);
		}
		public virtual void RegisterSingleton(System.Type serviceType, System.Func<System.Object> instanceCreator) 		
		{
			_container.RegisterSingleton(serviceType, instanceCreator);
		}
		public virtual void RegisterInitializer<TService>(System.Action<TService> instanceInitializer) 
            where TService: class		
		{
			_container.RegisterInitializer<TService>(instanceInitializer);
		}
		public virtual void RegisterInitializer(System.Action<SimpleInjector.Advanced.InstanceInitializationData> instanceInitializer, System.Predicate<SimpleInjector.Advanced.InitializationContext> predicate) 		
		{
			_container.RegisterInitializer(instanceInitializer, predicate);
		}
		public virtual void AddRegistration(System.Type serviceType, SimpleInjector.Registration registration) 		
		{
			_container.AddRegistration(serviceType, registration);
		}
		public virtual TService GetInstance<TService>() 
            where TService: class		
		{
			return _container.GetInstance<TService>();
		}
		public virtual System.Object GetInstance(System.Type serviceType) 		
		{
			return _container.GetInstance(serviceType);
		}
		public virtual System.Collections.Generic.IEnumerable<TService> GetAllInstances<TService>() 
            where TService: class		
		{
			return _container.GetAllInstances<TService>();
		}
		public virtual System.Collections.Generic.IEnumerable<System.Object> GetAllInstances(System.Type serviceType) 		
		{
			return _container.GetAllInstances(serviceType);
		}
		public virtual SimpleInjector.InstanceProducer GetRegistration(System.Type serviceType) 		
		{
			return _container.GetRegistration(serviceType);
		}
		public virtual SimpleInjector.InstanceProducer GetRegistration(System.Type serviceType, bool throwOnFailure) 		
		{
			return _container.GetRegistration(serviceType, throwOnFailure);
		}
		public virtual void RegisterConditional<TService, TImplementation>(System.Predicate<SimpleInjector.PredicateContext> predicate) 
            where TService: class
            where TImplementation: class, TService		
		{
			_container.RegisterConditional<TService, TImplementation>(predicate);
		}
		public virtual void RegisterConditional<TService, TImplementation>(SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.PredicateContext> predicate) 
            where TService: class
            where TImplementation: class, TService		
		{
			_container.RegisterConditional<TService, TImplementation>(lifestyle, predicate);
		}
		public virtual void RegisterConditional(System.Type serviceType, System.Type implementationType, System.Predicate<SimpleInjector.PredicateContext> predicate) 		
		{
			_container.RegisterConditional(serviceType, implementationType, predicate);
		}
		public virtual void RegisterConditional(System.Type serviceType, System.Type implementationType, SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.PredicateContext> predicate) 		
		{
			_container.RegisterConditional(serviceType, implementationType, lifestyle, predicate);
		}
		public virtual void RegisterConditional(System.Type serviceType, System.Func<SimpleInjector.TypeFactoryContext, System.Type> implementationTypeFactory, SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.PredicateContext> predicate) 		
		{
			_container.RegisterConditional(serviceType, implementationTypeFactory, lifestyle, predicate);
		}
		public virtual void RegisterConditional(System.Type serviceType, SimpleInjector.Registration registration, System.Predicate<SimpleInjector.PredicateContext> predicate) 		
		{
			_container.RegisterConditional(serviceType, registration, predicate);
		}
		public virtual void Register(System.Type openGenericServiceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies) 		
		{
			_container.Register(openGenericServiceType, assemblies);
		}
		public virtual void Register(System.Type openGenericServiceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies, SimpleInjector.Lifestyle lifestyle) 		
		{
			_container.Register(openGenericServiceType, assemblies, lifestyle);
		}
		public virtual void Register(System.Type openGenericServiceType, System.Collections.Generic.IEnumerable<System.Type> implementationTypes) 		
		{
			_container.Register(openGenericServiceType, implementationTypes);
		}
		public virtual void Register(System.Type openGenericServiceType, System.Collections.Generic.IEnumerable<System.Type> implementationTypes, SimpleInjector.Lifestyle lifestyle) 		
		{
			_container.Register(openGenericServiceType, implementationTypes, lifestyle);
		}
		public virtual void RegisterCollection<TService>(System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(assemblies);
		}
		public virtual void RegisterCollection(System.Type serviceType, params System.Reflection.Assembly[] assemblies) 		
		{
			_container.RegisterCollection(serviceType, assemblies);
		}
		public virtual void RegisterCollection(System.Type serviceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies) 		
		{
			_container.RegisterCollection(serviceType, assemblies);
		}
		public virtual System.Collections.Generic.IEnumerable<System.Type> GetTypesToRegister(System.Type serviceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies) 		
		{
			return _container.GetTypesToRegister(serviceType, assemblies);
		}
		public virtual System.Collections.Generic.IEnumerable<System.Type> GetTypesToRegister(System.Type serviceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies, SimpleInjector.TypesToRegisterOptions options) 		
		{
			return _container.GetTypesToRegister(serviceType, assemblies, options);
		}
		public virtual void RegisterCollection<TService>(System.Collections.Generic.IEnumerable<TService> containerUncontrolledCollection) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(containerUncontrolledCollection);
		}
		public virtual void RegisterCollection<TService>(params TService[] singletons) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(singletons);
		}
		public virtual void RegisterCollection<TService>(System.Collections.Generic.IEnumerable<System.Type> serviceTypes) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(serviceTypes);
		}
		public virtual void RegisterCollection<TService>(System.Collections.Generic.IEnumerable<SimpleInjector.Registration> registrations) 
            where TService: class		
		{
			_container.RegisterCollection<TService>(registrations);
		}
		public virtual void RegisterCollection(System.Type serviceType, System.Collections.Generic.IEnumerable<System.Type> serviceTypes) 		
		{
			_container.RegisterCollection(serviceType, serviceTypes);
		}
		public virtual void RegisterCollection(System.Type serviceType, System.Collections.Generic.IEnumerable<SimpleInjector.Registration> registrations) 		
		{
			_container.RegisterCollection(serviceType, registrations);
		}
		public virtual void RegisterCollection(System.Type serviceType, System.Collections.IEnumerable containerUncontrolledCollection) 		
		{
			_container.RegisterCollection(serviceType, containerUncontrolledCollection);
		}
		public virtual void RegisterDecorator<TService, TDecorator>() 
            where TService: class
            where TDecorator: class, TService		
		{
			_container.RegisterDecorator<TService, TDecorator>();
		}
		public virtual void RegisterDecorator<TService, TDecorator>(SimpleInjector.Lifestyle lifestyle) 		
		{
			_container.RegisterDecorator<TService, TDecorator>(lifestyle);
		}
		public virtual void RegisterDecorator(System.Type serviceType, System.Type decoratorType) 		
		{
			_container.RegisterDecorator(serviceType, decoratorType);
		}
		public virtual void RegisterDecorator(System.Type serviceType, System.Type decoratorType, SimpleInjector.Lifestyle lifestyle) 		
		{
			_container.RegisterDecorator(serviceType, decoratorType, lifestyle);
		}
		public virtual void RegisterDecorator(System.Type serviceType, System.Type decoratorType, SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.DecoratorPredicateContext> predicate) 		
		{
			_container.RegisterDecorator(serviceType, decoratorType, lifestyle, predicate);
		}
		public virtual void RegisterDecorator(System.Type serviceType, System.Func<SimpleInjector.DecoratorPredicateContext, System.Type> decoratorTypeFactory, SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.DecoratorPredicateContext> predicate) 		
		{
			_container.RegisterDecorator(serviceType, decoratorTypeFactory, lifestyle, predicate);
		}
		public virtual void RegisterDecorator(System.Type serviceType, System.Type decoratorType, System.Predicate<SimpleInjector.DecoratorPredicateContext> predicate) 		
		{
			_container.RegisterDecorator(serviceType, decoratorType, predicate);
		}
		public virtual void Verify() 		
		{
			_container.Verify();
		}
		public virtual void Verify(SimpleInjector.VerificationOption option) 		
		{
			_container.Verify(option);
		}
	}
	public partial interface IContainer		
	{

      SimpleInjector.InstanceProducer[] GetCurrentRegistrations();

      SimpleInjector.InstanceProducer[] GetRootRegistrations();

      void Dispose();

      void Register<TConcrete>()
            where TConcrete: class;

      void Register<TConcrete>(SimpleInjector.Lifestyle lifestyle)
            where TConcrete: class;

      void Register<TService, TImplementation>()
            where TService: class
            where TImplementation: class, TService;

      void Register<TService, TImplementation>(SimpleInjector.Lifestyle lifestyle)
            where TService: class
            where TImplementation: class, TService;

      void Register<TService>(System.Func<TService> instanceCreator)
            where TService: class;

      void Register<TService>(System.Func<TService> instanceCreator, SimpleInjector.Lifestyle lifestyle)
            where TService: class;

      void Register(System.Type concreteType);

      void Register(System.Type serviceType, System.Type implementationType);

      void Register(System.Type serviceType, System.Type implementationType, SimpleInjector.Lifestyle lifestyle);

      void Register(System.Type serviceType, System.Func<System.Object> instanceCreator);

      void Register(System.Type serviceType, System.Func<System.Object> instanceCreator, SimpleInjector.Lifestyle lifestyle);

      void RegisterSingleton<TService>(TService instance)
            where TService: class;

      void RegisterSingleton(System.Type serviceType, System.Object instance);

      void RegisterSingleton<TConcrete>()
            where TConcrete: class;

      void RegisterSingleton<TService, TImplementation>()
            where TService: class
            where TImplementation: class, TService;

      void RegisterSingleton<TService>(System.Func<TService> instanceCreator)
            where TService: class;

      void RegisterSingleton(System.Type serviceType, System.Type implementationType);

      void RegisterSingleton(System.Type serviceType, System.Func<System.Object> instanceCreator);

      void RegisterInitializer<TService>(System.Action<TService> instanceInitializer)
            where TService: class;

      void RegisterInitializer(System.Action<SimpleInjector.Advanced.InstanceInitializationData> instanceInitializer, System.Predicate<SimpleInjector.Advanced.InitializationContext> predicate);

      void AddRegistration(System.Type serviceType, SimpleInjector.Registration registration);

      TService GetInstance<TService>()
            where TService: class;

      System.Object GetInstance(System.Type serviceType);

      System.Collections.Generic.IEnumerable<TService> GetAllInstances<TService>()
            where TService: class;

      System.Collections.Generic.IEnumerable<System.Object> GetAllInstances(System.Type serviceType);

      SimpleInjector.InstanceProducer GetRegistration(System.Type serviceType);

      SimpleInjector.InstanceProducer GetRegistration(System.Type serviceType, bool throwOnFailure);

      void RegisterConditional<TService, TImplementation>(System.Predicate<SimpleInjector.PredicateContext> predicate)
            where TService: class
            where TImplementation: class, TService;

      void RegisterConditional<TService, TImplementation>(SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.PredicateContext> predicate)
            where TService: class
            where TImplementation: class, TService;

      void RegisterConditional(System.Type serviceType, System.Type implementationType, System.Predicate<SimpleInjector.PredicateContext> predicate);

      void RegisterConditional(System.Type serviceType, System.Type implementationType, SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.PredicateContext> predicate);

      void RegisterConditional(System.Type serviceType, System.Func<SimpleInjector.TypeFactoryContext, System.Type> implementationTypeFactory, SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.PredicateContext> predicate);

      void RegisterConditional(System.Type serviceType, SimpleInjector.Registration registration, System.Predicate<SimpleInjector.PredicateContext> predicate);

      void Register(System.Type openGenericServiceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies);

      void Register(System.Type openGenericServiceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies, SimpleInjector.Lifestyle lifestyle);

      void Register(System.Type openGenericServiceType, System.Collections.Generic.IEnumerable<System.Type> implementationTypes);

      void Register(System.Type openGenericServiceType, System.Collections.Generic.IEnumerable<System.Type> implementationTypes, SimpleInjector.Lifestyle lifestyle);

      void RegisterCollection<TService>(System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies)
            where TService: class;

      void RegisterCollection(System.Type serviceType, params System.Reflection.Assembly[] assemblies);

      void RegisterCollection(System.Type serviceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies);

      System.Collections.Generic.IEnumerable<System.Type> GetTypesToRegister(System.Type serviceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies);

      System.Collections.Generic.IEnumerable<System.Type> GetTypesToRegister(System.Type serviceType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies, SimpleInjector.TypesToRegisterOptions options);

      void RegisterCollection<TService>(System.Collections.Generic.IEnumerable<TService> containerUncontrolledCollection)
            where TService: class;

      void RegisterCollection<TService>(params TService[] singletons)
            where TService: class;

      void RegisterCollection<TService>(System.Collections.Generic.IEnumerable<System.Type> serviceTypes)
            where TService: class;

      void RegisterCollection<TService>(System.Collections.Generic.IEnumerable<SimpleInjector.Registration> registrations)
            where TService: class;

      void RegisterCollection(System.Type serviceType, System.Collections.Generic.IEnumerable<System.Type> serviceTypes);

      void RegisterCollection(System.Type serviceType, System.Collections.Generic.IEnumerable<SimpleInjector.Registration> registrations);

      void RegisterCollection(System.Type serviceType, System.Collections.IEnumerable containerUncontrolledCollection);

      void RegisterDecorator<TService, TDecorator>()
            where TService: class
            where TDecorator: class, TService;

      void RegisterDecorator<TService, TDecorator>(SimpleInjector.Lifestyle lifestyle);

      void RegisterDecorator(System.Type serviceType, System.Type decoratorType);

      void RegisterDecorator(System.Type serviceType, System.Type decoratorType, SimpleInjector.Lifestyle lifestyle);

      void RegisterDecorator(System.Type serviceType, System.Type decoratorType, SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.DecoratorPredicateContext> predicate);

      void RegisterDecorator(System.Type serviceType, System.Func<SimpleInjector.DecoratorPredicateContext, System.Type> decoratorTypeFactory, SimpleInjector.Lifestyle lifestyle, System.Predicate<SimpleInjector.DecoratorPredicateContext> predicate);

      void RegisterDecorator(System.Type serviceType, System.Type decoratorType, System.Predicate<SimpleInjector.DecoratorPredicateContext> predicate);

      void Verify();

      void Verify(SimpleInjector.VerificationOption option);
	}
}

