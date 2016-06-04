using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    public class TypeFilter : IImplementationFilter, ITypeFilter
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

        public void ExcludeImplementation(Type implementation)//, IEnumerable<Type> services = null)
        {
            types.Add(implementation);
        }

        public bool Contains(Type type)
        {
            return types.Contains(type) || filters.Any(filter => filter(type));
        }

        internal void AddFilter(Func<Type, bool> filter)
        {
            filters.Add(filter);
        }
    }
}
