using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    public sealed class NullServiceFilter : IServiceFilter
    {
        public bool IsServiceExcluded(Type serviceType)
        {
            return false;
        }
    }
}
