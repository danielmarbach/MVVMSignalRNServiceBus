using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WcfServices
{
    public class CustomerHub : Hub<ICustomerHub>
    {
        public Task CustomerAdded(CustomerEntity customer)
        {
            return Clients.All.CustomerAdded(customer);
        }
    }
}