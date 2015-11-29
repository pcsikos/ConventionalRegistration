using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    public interface IServiceFilter
    {
        bool IsServiceExcluded(Type serviceType);
    }
}
