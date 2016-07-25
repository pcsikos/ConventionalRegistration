using System;
using System.Collections.Generic;
using System.Linq;

namespace ConventionalRegistration.TypeFiltering
{
    /// <summary>
    /// Represents conditions and types to filter given types.
    /// </summary>
    public class TypeFilter : IServiceFilter
    {
        private ISet<Type> types;
        private IList<Func<Type, bool>> filters = new List<Func<Type, bool>>();

        public TypeFilter(IEnumerable<Type> types)
        {
            Check.NotNull(types, nameof(types));
            this.types = new HashSet<Type>(types);
        }

        public TypeFilter()
        {
            types = new HashSet<Type>();
        }

        public void ExcludeType(Type type)
        {
            Check.NotNull(type, nameof(type));
            types.Add(type);
        }

        public bool Contains(Type type)
        {
            Check.NotNull(type, nameof(type));
            return types.Contains(type) || filters.Any(filter => filter(type));
        }

        public void AddFilter(Func<Type, bool> filter)
        {
            Check.NotNull(filter, nameof(filter));
            filters.Add(filter);
        }

        public IEnumerable<Type> GetAllowedServices(Type implementation, IEnumerable<Type> services)
        {
            Check.NotNull(implementation, nameof(implementation));
            Check.NotNull(services, nameof(services));
            return services.Where(service => !Contains(service));
        }
    }
}
