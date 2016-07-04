using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar.TypeFiltering
{
    /// <summary>
    /// Provides methods for filtering.
    /// </summary>
    public class TypeFilter : ITypeFilter
    {
        private ISet<Type> types;
        private IList<Func<Type, bool>> filters = new List<Func<Type, bool>>();

        public TypeFilter(IEnumerable<Type> types)
        {
            this.types = new HashSet<Type>(types);
        }

        public TypeFilter()
        {
            types = new HashSet<Type>();
        }

        public void ExcludeType(Type type)
        {
            types.Add(type);
        }

        public bool Contains(Type type)
        {
            return types.Contains(type) || filters.Any(filter => filter(type));
        }

        public void AddFilter(Func<Type, bool> filter)
        {
            filters.Add(filter);
        }
    }
}
