using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Flubar.RegistrationProducers;

namespace Flubar.Tests
{
    [TestClass]
    public class TypeSelectorTests : UnitTest
    {
        private TypeSelector typeSelector;
        private Type unitTestType;

        [TestMethod]
        public void Contructor_NullTypesGiven_ShouldThrowNullArgumentException()
        {
            Instantiate(() => new TypeSelector(null, new NullServiceFilter()))
                .ShouldThrowExactly<ArgumentNullException>().Where(x => x.ParamName == "types");
        }

        [TestMethod]
        public void Excluding_ArrayOverload_RandomTypeGiven_ShouldExcludeType()
        {
            typeSelector.Excluding(new[] { unitTestType });

            //typeSelector.UsingStrategy(new TestRegistrationProducer()).RegisterEach
        }

        //[TestMethod]
        //public void Contructor_EmptyTypesGiven_ShouldThrowNullArgumentException()
        //{
        //    Instantiate(() => new TypeSelector(new Type[0]))
        //        .ShouldThrowExactly<ArgumentException>().Where(x => x.ParamName == "types");
        //}

        [TestInitialize]
        public void Initialize()
        {
            unitTestType = typeof(UnitTest);
            typeSelector = new TypeSelector(new[] { unitTestType }, new NullServiceFilter());
        }

        private class TestRegistrationProducer : IRegistrationProducer
        {
            public IRegistrationEntry CreateRegistrationEntry(Type type)
            {
                return new RegistrationEntry(type);
            }
        }
    }
}
