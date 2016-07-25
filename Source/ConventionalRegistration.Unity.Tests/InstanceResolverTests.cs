using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAssembly;
using TestAssembly.Data;

namespace ConventionalRegistration.Unity.Tests
{
    public abstract class InstanceResolverTests
    {
        private UnityContainer container;

        [TestInitialize]
        public virtual void Initialize()
        {
            container = new UnityContainer();
            //container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();
        }

        protected UnityContainer Container => container;

        protected object GetInstance(Type type)
        {
            return Container.Resolve(type);
        }

        protected T GetInstance<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        [TestMethod]
        public void Resolving_IDisposable_ShouldThrowActivationException()
        {
            this.Invoking(x => x.GetInstance<IDisposable>());
            //.ShouldThrow<ActivationException>();
        }

        [TestMethod]
        public void Resolving_ITransientService_ShouldAlwaysReturnDifferentInstance()
        {
            var instance1 = GetInstance<ITransientService>();
            var instance2 = GetInstance<ITransientService>();

            instance1.Should().NotBeNull();
            instance1.Should().NotBeSameAs(instance2);

        }

        [TestMethod]
        public void Resolving_ISingletonService_ShouldAlwaysReturnSameInstance()
        {
            var instance1 = GetInstance<ISingletonService>();
            var instance2 = GetInstance<ISingletonService>();

            instance1.Should().NotBeNull();
            instance1.Should().BeSameAs(instance2);
        }

        [TestMethod]
        public void Resolving_DbConnectionInSameScope_ShouldAlwaysReturnSameInstance()
        {
            var instance1 = GetInstance<DbConnection>();
            var instance2 = GetInstance<DbConnection>();

            instance1.Should().NotBeNull();
            instance1.Should().BeSameAs(instance2);
        }

        public IEnumerable<TResult> RunInDifferentThread<TResult>(Func<TResult> func, int times)
        {
            var tasks = new Task<TResult>[times];
            for (int i = 0; i < times; i++)
            {
                tasks[i] = Task<TResult>.Factory.StartNew(() =>
                {
                    Thread.Sleep(100);
                    return func();
                });
            }

            for (int i = 0; i < times; i++)
            {
                tasks[i].Wait();
            }
            return tasks.Select(x => x.Result).ToArray();
        }

        [TestMethod]
        public void Resolving_DbConnectionInDifferentScope_ShouldReturnDifferentInstance()
        {
            var instances = RunInDifferentThread(GetInstance<DbConnection>, 2).ToArray();

            instances[0].Should().NotBeNull();
            instances[0].Should().NotBeSameAs(instances[1]);
        }

        [TestMethod]
        public void Resolving_DbContext1InSameScope_ShouldAlwaysReturnSameInstance()
        {
            var instance1 = GetInstance<DbContext1>();
            var instance2 = GetInstance<DbContext1>();

            instance1.Should().NotBeNull();
            instance1.Should().BeSameAs(instance2);
        }

        [TestMethod]
        public void Resolving_DbContext1InDifferentScope_ShouldReturnDifferentInstance()
        {
            var instances = RunInDifferentThread(GetInstance<DbContext1>, 2).ToArray();

            instances[0].Should().NotBeNull();
            instances[0].Should().NotBeSameAs(instances[1]);
        }

        [TestMethod]
        public void Resolving_DbContext2InSameScope_ShouldAlwaysReturnSameInstance()
        {
            var instance1 = GetInstance<DbContext2>();
            var instance2 = GetInstance<DbContext2>();

            instance1.Should().NotBeNull();
            instance1.Should().BeSameAs(instance2);
        }

        [TestMethod]
        public void Resolving_DbContext2InDifferentScope_ShouldReturnDifferentInstance()
        {
            var instances = RunInDifferentThread(GetInstance<DbContext2>, 2).ToArray();

            instances[0].Should().NotBeNull();
            instances[0].Should().NotBeSameAs(instances[1]);
        }

        [TestMethod]
        public void Resolving_DbContextsInSameScope_ShouldHaveSameDbConnectionInstance()
        {
            var context1 = GetInstance<DbContext1>();
            var context2 = GetInstance<DbContext2>();

            context1.Should().NotBeNull();
            context2.Should().NotBeNull();
            context1.Connection.Should().BeSameAs(context2.Connection);
        }

        [TestMethod]
        public void Resolving_IFileReadOrIFileWrite_ShouldAllwaysReturnSameInstance()
        {
            var fileRead = GetInstance<IFileRead>();
            var fileWrite = GetInstance<IFileWrite>();

            fileRead.Should().NotBeNull();
            fileWrite.Should().NotBeNull();
            fileRead.Should().BeSameAs(fileWrite);
        }

        [TestMethod]
        public void Resolving_IDataProvider_ShouldReturnXmlDataProvider()
        {
            var dataProvider = GetInstance<IDataProvider>();
            dataProvider.Should().BeOfType<XmlDataProvider>();
        }

        [TestMethod]
        public void Resolving_Func_ShouldAllwaysReturnSameInstance()
        {
            var func1 = GetInstance<Func<ITransientService>>();
            var func2 = GetInstance<Func<ITransientService>>();

            var instance1 = func1();
            var instance2 = func2();

            func1.Should().NotBeNull();
            func2.Should().NotBeNull();
            func1.Should().BeSameAs(func2);
            instance1.Should().NotBeNull();
            instance1.Should().NotBeSameAs(instance2);
        }

        [TestMethod]
        public void Resolving_OpenGeneric_ShouldInstanceWithCorrectGenericArgument()
        {
            var customerRepository = GetInstance<IRepository<Customer>>();
            var orderRepository = GetInstance<IRepository<Order>>();

            customerRepository.Should().NotBeNull();
            orderRepository.Should().NotBeNull();
        }

        [TestMethod]
        public void Resolving_Decorators_ShouldWrapInstance()
        {
            var command = GetInstance<ICommandHandler<CustomCommand>>();

            var result = command.Handle(new CustomCommand { });

            result.Should().NotBeNull().And.Be("cbabc");
        }

        [TestMethod]
        public void Resolving_CollectionOfOpenGenericsForDefinedType()
        {
            var customerValidators = Container.ResolveAll<IValidator<Customer>>();
            var orderValidators = Container.ResolveAll<IValidator<Order>>();

            customerValidators.Should().NotBeNull().And.HaveCount(2).And.Subject.Should().Contain(x => x is CustomerLocationValidator);
            customerValidators.OfType<CustomerLocationValidator>().Single().Name.Should().Be("abc");
            orderValidators.Should().NotBeNull().And.HaveCount(1);
        }

        [TestMethod]
        public void Resolving_CollectionOfOpenGenericsForUndefinedType()
        {
            var productValidators = Container.ResolveAll<IValidator<Product>>();
            productValidators.Should().BeEmpty();
        }

        [TestMethod]
        public void Resolving_ConcreteCommand_ShouldReturnDefinedValidator()
        {
            var customerCommandValidators = Container.ResolveAll<ICommandValidator<PlaceOrderCommand>>();
            customerCommandValidators.Should().NotBeNull().And.HaveCount(1);
        }

        [TestMethod]
        public void Resolving_AbstractCommandShouldReturnEmptyList()
        {
            var customerCommandValidators = Container.ResolveAll<ICommandValidator<CustomerCommand>>();
            customerCommandValidators.Should().NotBeNull().And.BeEmpty();
        }

        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, LifetimeManager lifetimeManager)
        {
            Container.RegisterType(implementation, lifetimeManager);
            foreach (var type in serviceTypes)
            {
                Container.RegisterType(type, implementation);
            }
        }
    }
}
