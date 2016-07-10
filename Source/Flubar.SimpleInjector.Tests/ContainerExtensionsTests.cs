using System;
using Flubar;
using Flubar.Syntax;
using Flubar.TypeFiltering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using TestAssembly;
using Flubar.Configuration;
using FluentAssertions;

namespace Flubar.SimpleInjector.Tests
{
    [TestClass]
    public class ContainerExtensionsTests
    {
        [TestMethod]
        public void ImportPackagesTest()
        {
            var container = new Container();
            container.RegistrationByConvention(BehaviorConfiguration.Default, (builder, filter) =>
            {
                builder.ImportPackages(new PackageA(), new PackageB());
            });

            container.GetInstance<ITransientService>().Should().NotBeNull();
            container.GetInstance<ISingletonService>().Should().NotBeNull();
        }

        private class PackageA : IConventionBuilderPackage<Container, Lifestyle>
        {
            public void RegisterByConvention(Container container, IConventionBuilder<Lifestyle> builder, IImplementationFilter implementationFilter)
            {
                builder.Define(x => new[] { typeof(TransientService2) }
                    .UsingAllInterfacesStrategy());
            }
        }

        private class PackageB : IConventionBuilderPackage<Container, Lifestyle>
        {
            public void RegisterByConvention(Container container, IConventionBuilder<Lifestyle> builder, IImplementationFilter implementationFilter)
            {
                builder.Define(x => new[] { typeof(SingletonService) }
                    .UsingAllInterfacesStrategy());
            }
        }
    }
}
