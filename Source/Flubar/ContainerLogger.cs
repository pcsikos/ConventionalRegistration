using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    public class ContainerLogger<TLifetime> : IContainer<TLifetime>, IDecorator
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> decorated;
        private readonly ILog logger;

        public ContainerLogger(IContainer<TLifetime> decorated, ILog logger)
        {
            this.logger = logger;
            this.decorated = decorated;
        }

        public object Decoratee
        {
            get
            {
                return decorated;
            }
        }

        public TLifetime GetDefaultLifetime()
        {
            return decorated.GetDefaultLifetime();
        }

        public TLifetime GetSingletonLifetime()
        {
            return decorated.GetSingletonLifetime();
        }

        public void RegisterMultipleImplementations(Type serviceType, IEnumerable<Type> implementations)
        {
            decorated.RegisterMultipleImplementations(serviceType, implementations);
        }

        public void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, TLifetime lifetime = null)
        {
            decorated.RegisterMultipleServices(serviceTypes, implementation, lifetime);
            LogRegistration(implementation, serviceTypes);
        }

        public void RegisterService(Type serviceType, Type implementation, TLifetime lifetime = null)
        {
            decorated.RegisterService(serviceType, implementation, lifetime);
            LogRegistration(implementation, serviceType);
        }

        private void LogRegistration(Type implementation, IEnumerable<Type> services)
        {
            foreach (var serviceType in services)
            {
                LogRegistration(implementation, serviceType);
            }
        }

        private void LogRegistration(Type implementation, Type serviceType)
        {
            logger.Info("Registration {0} to {1}.", serviceType.FullName, implementation.FullName);
        }
    }
}
