using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WcfServices
{
    public static class CustomerDatabase
    {
        private static long accessCounter;

        public static bool Offline = false;

        private static readonly ConcurrentDictionary<Guid, CustomerEntity> customers =
            new ConcurrentDictionary<Guid, CustomerEntity>(
                new List<KeyValuePair<Guid, CustomerEntity>>
                {
                    new KeyValuePair<Guid, CustomerEntity>(new Guid("{0FCA5B0A-07C6-45EA-92EB-3794A434C974}"),
                        new CustomerEntity {Id = new Guid("{0FCA5B0A-07C6-45EA-92EB-3794A434C974}"), Name = "Daniel"}),
                    new KeyValuePair<Guid, CustomerEntity>(new Guid("{0FCA5B0A-07C6-45EA-92EB-3794A434C973}"),
                        new CustomerEntity {Id = new Guid("{0FCA5B0A-07C6-45EA-92EB-3794A434C973}"), Name = "Peter"})
                });

        public static async Task<IEnumerable<CustomerEntity>> GetCustomers()
        {
            Interlocked.Increment(ref accessCounter);
            if (Offline)
            {
                throw new TimeoutException("Offline");
            }
            await Task.Delay(1000);
            return customers.Values.ToList();
        }

        public static async Task Save(CustomerEntity customer)
        {
            var currentCounter = Interlocked.Increment(ref accessCounter);
            if (Offline)
            {
                throw new TimeoutException("Offline");
            }

            await Task.Delay(1000);
            if (currentCounter % 2 == 0)
            {
                throw new DBConcurrencyException("Oops");
            }

            customers.TryAdd(customer.Id, customer);
        }
    }
}