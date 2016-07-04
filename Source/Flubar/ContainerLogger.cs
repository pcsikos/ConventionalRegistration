using System;
using System.Collections.Generic;
using Flubar.Diagnostics;

namespace Flubar
{
    /// <summary>
    /// Provides a decorator over <see cref="IContainer{TLifetime}"/> to log the key parts of the process of registration.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    public class ContainerLogger<TLifetime> : IContainer<TLifetime>, IDecorator
        where TLifetime : class
    {
        private readonly IContainer<TLifetime> decoratee;
        private readonly ILog logger;

        public ContainerLogger(IContainer<TLifetime> decoratee, ILog logger)
        {
            this.logger = logger;
            this.decoratee = decoratee;
        }

        public object Decoratee
        {
            get
            {
                if (decoratee is IDecorator)
                {
                    return ((IDecorator)decoratee).Decoratee;
                }
                return decoratee;
            }
        }

        public TLifetime GetDefaultLifetime()
        {
            return decoratee.GetDefaultLifetime();
        }

        public TLifetime GetSingletonLifetime()
        {
            return decoratee.GetSingletonLifetime();
        }

        public void RegisterMultipleImplementations(Type serviceType, IEnumerable<Type> implementations)
        {
            decoratee.RegisterMultipleImplementations(serviceType, implementations);
        }

        public void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, TLifetime lifetime = null)
        {
            decoratee.RegisterMultipleServices(serviceTypes, implementation, lifetime);
            LogRegistration(implementation, serviceTypes);
        }

        public void RegisterService(Type serviceType, Type implementation, TLifetime lifetime = null)
        {
            decoratee.RegisterService(serviceType, implementation, lifetime);
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
