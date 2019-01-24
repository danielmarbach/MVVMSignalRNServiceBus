using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using WpfApplication.ServiceReference2;
using WpfApplication.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace WpfApplication
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            var hubConnection = new HubConnectionBuilder().WithUrl(
                "http://localhost:5000/customerhub")
                .Build();

            hubConnection.Closed += async e =>
            {
                Console.WriteLine(e);
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnection.StartAsync();
            };

            hubConnection.StartAsync().GetAwaiter().GetResult();

            var customerServiceClient = new CustomerServiceClient("BasicHttpBinding_ICustomerService1");

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.RegisterInstance(typeof(ICustomerService), null, customerServiceClient);
            container.RegisterInstance(typeof(HubConnection), null, hubConnection);

            container.PerRequest<ShellViewModel>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}