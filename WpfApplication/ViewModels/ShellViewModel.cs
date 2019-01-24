using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.AspNetCore.SignalR.Client;
using WpfApplication.ServiceReference2;

namespace WpfApplication.ViewModels
{
    public class ShellViewModel : Screen, IHandleWithTask<CustomerModel>, IHandleWithTask<LoadCustomers>
    {
        private string customerName;
        private IEventAggregator eventAggregator;
        private ICustomerService customerService;
        private HubConnection hubConnection;

        public ShellViewModel(IEventAggregator eventAggregator, ICustomerService customerService, HubConnection hubConnection)
        {
            this.hubConnection = hubConnection;
            this.customerService = customerService;
            this.eventAggregator = eventAggregator;

            this.eventAggregator.Subscribe(this);

            Customers = new BindableCollection<CustomerModel>();
        }

        public string CustomerName
        {
            get => customerName;
            set => Set(ref customerName, value);
        }

        protected override async void OnActivate()
        {
            hubConnection.On<CustomerModel>("CustomerAdded",
                customer => { eventAggregator.PublishOnUIThreadAsync(customer); });
            await eventAggregator.PublishOnUIThreadAsync(new LoadCustomers());
        }

        public async Task ToggleFailureMode()
        {
            await customerService.ToggleFailureModeAsync();
        }

        public async Task Save()
        {
            var customerModel = new CustomerModel { Id = Guid.NewGuid(), Name = CustomerName};
            await customerService.SaveCustomerAsync(new Customer { Id = customerModel.Id, Name = customerModel.Name });
            await eventAggregator.PublishOnUIThreadAsync(customerModel);
            CustomerName = null;
        }

        public BindableCollection<CustomerModel> Customers { get; }

        public Task Handle(CustomerModel message)
        {
            Customers.Remove(message);
            Customers.Add(message);
            return Task.CompletedTask;
        }

        public async Task Handle(LoadCustomers message)
        {
            var allSystems = await customerService.GetCustomersAsync();
            Customers.AddRange(allSystems.Select(s => new CustomerModel { Id = s.Id, Name = s.Name }));
        }
    }
}