using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using NServiceBus.Logging;

namespace WcfServices
{
    public class SaveCustomerHandler : IHandleMessages<SaveCustomer>
    {
        private static readonly ILog Logger = LogManager.GetLogger<SaveCustomerHandler>();
        private readonly IHubContext<CustomerHub, ICustomerHub> customerHubContext;

        public SaveCustomerHandler(IHubContext<CustomerHub, ICustomerHub> customerHubContext)
        {
            this.customerHubContext = customerHubContext;
        }

        public async Task Handle(SaveCustomer message, IMessageHandlerContext context)
        {
            var newCustomer = new CustomerEntity { Id = message.Id, Name = message.Name + "ByServer" };

            Logger.Info($"Attempting to create customer with name '{newCustomer.Name}'");

            await CustomerDatabase.Save(newCustomer);
            await customerHubContext.Clients.All.CustomerAdded(newCustomer);

            Logger.Info($"Created customer with name '{newCustomer.Name}'");
        }
    }
}