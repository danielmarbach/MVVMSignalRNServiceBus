using System;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;

namespace WcfServices
{
    class Program
    {
        static Uri baseAddress = new Uri("http://localhost:49100/customer");

        static async Task Main(string[] args)
        {
            ReserveUrlACL();

            var builder = CreateWebHostBuilder(args);
            var webHost = builder.Build();

            // for the sake of the example and simplify I'm hosting NServiceBus together with WCF
            var configuration = new EndpointConfiguration("WcfService");
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseTransport<LearningTransport>();

            var metrics = configuration.EnableMetrics();
            metrics.SendMetricDataToServiceControl(
                "Particular.Monitoring",
                TimeSpan.FromMilliseconds(500)
            );

            configuration.SendFailedMessagesTo("error");
            configuration.AuditProcessedMessagesTo("audit");

            var recoverability = configuration.Recoverability();
            recoverability.Delayed(c => c.NumberOfRetries(0)); // for demo

            // hardwired to avoid using more dependencies
            configuration.RegisterComponents(c => c.ConfigureComponent(()=> webHost.Services.GetService<IHubContext<CustomerHub, ICustomerHub>>(), DependencyLifecycle.InstancePerCall));

            await webHost.StartAsync();
            var endpoint = await Endpoint.Start(configuration);

            using (var host = new ServiceHost(typeof(CustomerService), baseAddress))
            {
                // Enable metadata publishing.
                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true, MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
                };
                host.Description.Behaviors.Add(smb);
                host.Description.Behaviors.Add(new MessageSessionInspectorBehavior(endpoint));

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

            await endpoint.Stop();
            await webHost.StopAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>();

        static void ReserveUrlACL()
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
