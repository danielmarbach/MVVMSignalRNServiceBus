using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace WcfServices
{
    public class SaveCustomerHandler : IHandleMessages<SaveCustomer>
    {
        private static ILog Logger = LogManager.GetLogger<SaveCustomerHandler>();

        public async Task Handle(SaveCustomer message, IMessageHandlerContext context)
        {
            Logger.Info($"Attempting to create customer with name '{message.Name}'");

            var newCustomer = new CustomerEntity { Id = message.Id, Name = message.Name };
            await CustomerDatabase.Save(newCustomer);

            Logger.Info($"Created customer with name '{message.Name}'");
        }
    }
}