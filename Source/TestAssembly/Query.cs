namespace TestAssembly
{
    public class GetCustomerQuery : IQuery<Customer>
    {
        public GetCustomerQuery(string customerId)
        {

        }
    }

    public interface IQuery<T>
    {
    }
}
