﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfServices
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Task<IEnumerable<Customer>> GetCustomers();

        [OperationContract]
        Task ToggleFailureMode();

        [OperationContract]
        Task SaveCustomer(Customer customer);
    }
}