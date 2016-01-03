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

    public interface ICustomerRepository : IRepository<Customer>
    {

    }

    public class CustomerRepository : ICustomerRepository
    {

    }

    public class Customer
    {
        public string Name { get; set; }
    }

    public class Order
    {

    }
}
