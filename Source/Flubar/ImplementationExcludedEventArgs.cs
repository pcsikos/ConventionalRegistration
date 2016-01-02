using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    public class ImplementationExcludedEventArgs : EventArgs
    {
        readonly Type implementation;
        readonly IEnumerable<Type> serviceTypes;

        public ImplementationExcludedEventArgs(Type implementation)
            : this(implementation, new Type[0])
        {
        }

        public ImplementationExcludedEventArgs(Type implementation, IEnumerable<Type> serviceTypes)
        {
            this.implementation = implementation;
            this.serviceTypes = serviceTypes;
        }

        public ImplementationExcludedEventArgs(Type implementation, Type serviceType)
            : this(implementation, new[] { serviceType })
        {
        }

        public Type Implementation => implementation;
        public IEnumerable<Type> Services => serviceTypes;
    }
}
