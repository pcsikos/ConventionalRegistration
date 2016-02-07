using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.SimpleInjector.CodeGeneration
{
    public class DemoClass
    {
        public void Method0<TValue>()
        {
        }


        public void Method1<TValue>()
            where TValue : class
        {
        }

        public void Method2<TValue>()
            where TValue : struct
        {

        }

        public void Method3<TValue>()
            where TValue : IComparable
        {
        }

        public void Method4<TValue, TResult>()
            where TValue : class
            where TResult : TValue
        {
        }

        public void Method5<TValue>(Func<TValue> func)
            where TValue : IComparable
        {
        }

        public void Method6<TValue>(IEnumerable<TValue> func)
            where TValue : IComparable
        {
        }

    }
}
