using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.Tests
{
    public class UnitTest
    {
        protected Action Instantiate<T>(Func<T> instanceCreator)
           where T : class
        {
            return () => instanceCreator();
        }
    }
}
