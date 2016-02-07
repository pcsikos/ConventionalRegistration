using Flubar.SimpleInjector.CodeGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.SimpleInjector.Tests
{
    [TestClass]
    public class CodeDomTest2
    {
        [TestMethod]
        public void RunTest()
        {
            //var result = CodeGeneration.CodeDomTest.Run();
            var m = typeof(DemoClass).GetMethod("Method2");
            var s1 = CodeGenerator.GetTypeParametersConstrants(m);
            m = typeof(DemoClass).GetMethod("Method4");
            var s2 = CodeGenerator.GetTypeParametersConstrants(m);
        }

        
    }
}
