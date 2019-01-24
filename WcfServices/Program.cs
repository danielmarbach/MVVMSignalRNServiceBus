using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;

namespace WcfServices
{
    class Program
    {
        static Uri baseAddress = new Uri("http://localhost:8084/system");

        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(SystemService), baseAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true, MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
                };
                host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }

    [ServiceContract]
    public interface ISystemService
    {
        [OperationContract]
        Task<System> GetSystem(Guid id);

        [OperationContract]
        Task SaveSystem(System system);
    }

    [DataContract]
    public class System
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }

    public class SystemService : ISystemService
    {
        public Task<System> GetSystem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveSystem(System system)
        {
            throw new NotImplementedException();
        }
    }
}
