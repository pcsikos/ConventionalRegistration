using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAssembly.Data;

namespace TestAssembly
{
    public class Repository<TEntity> : IRepository<TEntity>
    {
        public Repository(DbContext1 context1)
        {

        }
    }

    public interface IRepository<TEntity>
    {

    }

    public class Customer
    {

    }

    public class Order
    {

    }
}
