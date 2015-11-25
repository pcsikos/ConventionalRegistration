using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAssembly
{
    public interface ITransientService : IDisposable
    {
    }

    public class TransientService : ITransientService
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
