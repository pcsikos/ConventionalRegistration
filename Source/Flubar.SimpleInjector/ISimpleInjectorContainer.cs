using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.SimpleInjector
{
    //todo: rename to adapter
    public interface ISimpleInjectorContainer : IContainer<Lifestyle>
    {
        void RegisterCollection<TService>(IEnumerable<Type> serviceTypes) where TService : class;
        void RegisterDecorator(Type serviceType, Type decoratorType);
        void RegisterFunc<T>() where T : class;
        //void RegisterMultipleServices(IEnumerable<Type> serviceTypes, Type implementation, Lifestyle lifestyle);
        //void Register<TService>(Func<TService> instanceCreator, Lifestyle lifetime = null);
        void Register<TService, TImplementation>(Lifestyle lifetime = null) 
            where TService : class 
            where TImplementation : class, TService;
        void Register<TConcrete>(Lifestyle lifetime)
            where TConcrete : class;
    }
}
