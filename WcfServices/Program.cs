using System;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfServices
{
    class Program
    {
        static Uri baseAddress = new Uri("http://localhost:49100/customer");

        static void Main(string[] args)
        {
            ReserveUrlACL();

            using (var host = new ServiceHost(typeof(CustomerService), baseAddress))
            {
                // Enable metadata publishing.
                var smb = new ServiceMetadataBehavior
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

        public static void ReserveUrlACL()
        {
            var everyone = new SecurityIdentifier(
                "S-1-1-0").Translate(typeof(NTAccount)).ToString();

            string parameter = @"http add urlacl url=http://+:49100/ user=\" + everyone;

            ProcessStartInfo psi = new ProcessStartInfo("netsh", parameter)
            {
                Verb = "runas",
                RedirectStandardOutput = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false
            };

            var process = Process.Start(psi);
            process.WaitForExit();
        }
    }
}
