using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConventionalRegistration.TypeFiltering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAssembly;
using FluentAssertions;

namespace ConventionalRegistration.TypeFiltering.Tests
{
    [TestClass()]
    public class TypeSelectorTests
    {
        //[TestMethod()]
        //public void TypeSelectorTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void IncludingTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void IncludingTest1()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void IncludingTest2()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void ExcludingTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void ExcludingTest1()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void ExcludingTest2()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void ExcludingGenericTypesTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void WithoutAttributeTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void WithoutAttributeTest1()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void WithAttributeTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void WithAttributeTest1()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void WhereTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void IsImplementingTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void IsImplementingGenericTypes_GenericTypeDefinitionContainedInOneOfTheTypesGiven_ShouldExludeTypesNotImplementingProvidedType()
        {
            var target = new TypeSelector(new[] { typeof(GetCustomerQuery), typeof(CustomerRepository) });
            target.IsImplementingGenericTypes(typeof(IQuery<>));
            var types = target.ToArray();

            types.Should().HaveCount(1).And.Contain(typeof(GetCustomerQuery));
        }

        [TestMethod()]
        public void IsNotImplementingGenericTypes_GenericTypeDefinitionContainedInOneOfTheTypesGiven_ShouldExludeTypesImplementingProvidedType()
        {
            var target = new TypeSelector(new[] { typeof(GetCustomerQuery), typeof(CustomerRepository) });
            target.IsNotImplementingGenericTypes(typeof(IQuery<>));
            var types = target.ToArray();

            types.Should().HaveCount(1).And.Contain(typeof(CustomerRepository));
        }

        //[TestMethod()]
        //public void SelectTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void SelectAllClassesTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void GetEnumeratorTest()
        //{
        //    Assert.Fail();
        //}
    }
}