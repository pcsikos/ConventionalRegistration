using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAssembly
{
    //public class NullValidator<TEntity> : IValidator<TEntity>
    //{
    //}

    public interface IValidator<TEntity>
    {
    }

    public class IdValidator<TEntity> : IValidator<TEntity>
        where TEntity : IEntity
    {

    }


    public class CustomerLocationValidator : IValidator<Customer>
    {
        public string Name { get; set; }
    }

    public class CustomerCreditValidator : IValidator<Customer>
    {

    }

    public class OrderValidator : IValidator<Order>
    {

    }

    public class Product : IEntity
    {
        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    public interface IEntity
    {
        int Id { get; }
    }
}
