using System;
using NServiceBus;

namespace WcfServices
{
    public class SaveCustomer : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}