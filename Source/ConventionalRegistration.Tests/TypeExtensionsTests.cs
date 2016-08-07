using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConventionalRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ConventionalRegistration.Tests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void GetGenericInterfacesMatching_GenericListGiven_ShouldReturnGenericIList()
        {
            var result = TypeExtensions.GetGenericInterfacesMatching(typeof(List<>)).ToArray();

            result.Should().Contain(typeof(IList<>));
        }
    }
}