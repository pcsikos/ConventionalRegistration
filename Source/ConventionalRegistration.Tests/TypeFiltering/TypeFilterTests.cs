using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConventionalRegistration.TypeFiltering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ConventionalRegistration.TypeFiltering.Tests
{
    [TestClass()]
    public class TypeFilterTests
    {
        [TestMethod()]
        public void Contains_GenericTypeGiven_ShouldReturnTrueWhenOpenGenericInList()
        {
            var target = new TypeFilter(new[] { typeof(IEnumerable<>) });
            var result = target.Contains(typeof(IEnumerable<string>));

            result.Should().BeTrue();
        }
    }
}