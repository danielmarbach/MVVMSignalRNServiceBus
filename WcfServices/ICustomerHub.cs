using System.Threading.Tasks;

namespace WcfServices
{
    public interface ICustomerHub
    {
        Task CustomerAdded(CustomerEntity customer);
    }
}