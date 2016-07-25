using System;

namespace ConventionalRegistration.Tests
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
