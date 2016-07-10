using Flubar.Syntax;
using Flubar.TypeFiltering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using TestAssembly;
using FluentAssertions;
using System;

namespace Flubar.SimpleInjector.Tests
{
    [TestClass]
    public class ContainerExtensionsTests
    {
        [TestMethod]
        public void ImportPackagesTest()
        {
            var container = new Container();
            container.RegistrationByConvention(builder =>
            {
                builder.ImportPackages(new PackageA(), new PackageB());
            });

            container.GetInstance<ITransientService>().Should().NotBeNull();
            container.GetInstance<ISingletonService>().Should().NotBeNull();
        }

        private class PackageA : IConventionBuilderPackage<Container, Lifestyle>
        {
            public void RegisterByConvention(IConventionBuilderSyntax<Lifestyle, Container> builder)
            {
                builder.Define(x => new[] { typeof(TransientService2) }
                    .UsingAllInterfacesStrategy());
            }
        }

        private class PackageB : IConventionBuilderPackage<Container, Lifestyle>
        {
            public void RegisterByConvention(IConventionBuilderSyntax<Lifestyle, Container> builder)
            {
                builder.Define(x => new[] { typeof(SingletonService) }
                    .UsingAllInterfacesStrategy());
            }
        }
    }
}
