using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IServiceExtractor
    {
        //IEnumerable<Type> RegisterMapping(IEnumerable<Type> services, Type implementationType);
        void RegisterMonitoredType(Type serviceType, Action<IEnumerable<Type>> callback);
        void Resolve();//todo: refactor
    }
}