using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WcfServices
{
    public class CustomerService : ICustomerService
    {
        public Task<IEnumerable<Customer>> GetCustomers()
        {
            return Task.FromResult<IEnumerable<Customer>>(new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), Name = "Daniel" },
            });
        }

        public Task<Customer> GetCustomer(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}