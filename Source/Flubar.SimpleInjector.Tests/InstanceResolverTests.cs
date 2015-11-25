using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using SimpleInjector;
using TestAssembly;
using SimpleInjector.Extensions.LifetimeScoping;
using TestAssembly.Data;

namespace Flubar.SimpleInjector.Tests
{
    public abstract class InstanceResolverTests
    {
        private readonly Container container;

        public InstanceResolverTests()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();
        }

        protected Container Container
        {
            get
            {
                return container;
            }
        }

        protected object GetInstance(Type type)
        {
            return Container.GetInstance(type);
        }

        protected T GetInstance<T>() where T : class
        {
            return Container.GetInstance<T>();
        }

        [TestMethod]
        public void Resolving_IDisposable_ShouldThrowActivationException()
        {
            this.Invoking(x => x.GetInstance<IDisposable>())
                .ShouldThrow<ActivationException>();
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
            using (container.BeginLifetimeScope())
            {
                var instance1 = GetInstance<DbConnection>();
                var instance2 = GetInstance<DbConnection>();

                instance1.Should().NotBeNull();
                instance1.Should().BeSameAs(instance2);
            }
        }

        [TestMethod]
        public void Resolving_DbConnectionInDifferentScope_ShouldReturnDifferentInstance()
        {
            DbConnection instance1;
            using (container.BeginLifetimeScope())
            {
                instance1 = GetInstance<DbConnection>();

            }

            DbConnection instance2;
            using (container.BeginLifetimeScope())
            {
                instance2 = GetInstance<DbConnection>();
            }
            instance1.Should().NotBeNull();
            instance1.Should().NotBeSameAs(instance2);
        }

        [TestMethod]
        public void Resolving_DbContext1InSameScope_ShouldAlwaysReturnSameInstance()
        {
            using (container.BeginLifetimeScope())
            {
                var instance1 = GetInstance<DbContext1>();
                var instance2 = GetInstance<DbContext1>();

                instance1.Should().NotBeNull();
                instance1.Should().BeSameAs(instance2);
            }
        }

        [TestMethod]
        public void Resolving_DbContext1InDifferentScope_ShouldReturnDifferentInstance()
        {
            DbContext1 instance1;
            using (container.BeginLifetimeScope())
            {
                instance1 = GetInstance<DbContext1>();

            }

            DbContext1 instance2;
            using (container.BeginLifetimeScope())
            {
                instance2 = container.GetInstance<DbContext1>();
            }
            instance1.Should().NotBeNull();
            instance1.Should().NotBeSameAs(instance2);
        }

        [TestMethod]
        public void Resolving_DbContext2InSameScope_ShouldAlwaysReturnSameInstance()
        {
            using (container.BeginLifetimeScope())
            {
                var instance1 = GetInstance<DbContext2>();
                var instance2 = GetInstance<DbContext2>();

                instance1.Should().NotBeNull();
                instance1.Should().BeSameAs(instance2);
            }
        }

        [TestMethod]
        public void Resolving_DbContext2InDifferentScope_ShouldReturnDifferentInstance()
        {
            DbContext2 instance1;
            using (container.BeginLifetimeScope())
            {
                instance1 = GetInstance<DbContext2>();

            }

            DbContext2 instance2;
            using (container.BeginLifetimeScope())
            {
                instance2 = GetInstance<DbContext2>();
            }
            instance1.Should().NotBeNull();
            instance1.Should().NotBeSameAs(instance2);
        }

        [TestMethod]
        public void Resolving_DbContextsInSameScope_ShouldHaveSameDbConnectionInstance()
        {
            using (container.BeginLifetimeScope())
            {
                var context1 = GetInstance<DbContext1>();
                var context2 = GetInstance<DbContext2>();

                context1.Should().NotBeNull();
                context2.Should().NotBeNull();
                context1.Connection.Should().BeSameAs(context2.Connection);
            }
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
            using (container.BeginLifetimeScope())
            {
                var customerRepository = GetInstance<IRepository<Customer>>();
                var orderRepository = GetInstance<IRepository<Order>>();

                customerRepository.Should().NotBeNull();
                orderRepository.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void Resolving_Decorators_ShouldWrapInstance()
        {
            var command = GetInstance<ICommand>();

            var result = command.GetString();

            result.Should().NotBeNull();
            result.Should().Be("cbabc");
        }
    }
}
