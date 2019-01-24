using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;

namespace WcfServices
{
    public class CustomerService : ICustomerService, IProvideMessageSession
    {
        public IMessageSession Session { get; set; }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await CustomerDatabase.GetCustomers();
            return customers.Select(c => new Customer { Id = c.Id, Name = c.Name });
        }

        public Task ToggleFailureMode()
        {
            CustomerDatabase.Offline = !CustomerDatabase.Offline;
            return Task.CompletedTask;
        }

        public Task SaveCustomer(Customer customer)
        {
            return Session.SendLocal(new SaveCustomer {Id = customer.Id, Name = customer.Name});
        }
    }
}