using System;

namespace TestAssembly
{
    public interface ISingletonService : IDisposable
    {

    }

    public class SingletonService : ISingletonService
    {
        public void Dispose()
        {
            
        }
    }
}
