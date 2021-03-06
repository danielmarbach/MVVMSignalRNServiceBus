﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApplication.ServiceReference2 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Customer", Namespace="http://schemas.datacontract.org/2004/07/WcfServices")]
    [System.SerializableAttribute()]
    public partial class Customer : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.ICustomerService")]
    public interface ICustomerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/GetCustomers", ReplyAction="http://tempuri.org/ICustomerService/GetCustomersResponse")]
        WpfApplication.ServiceReference2.Customer[] GetCustomers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/GetCustomers", ReplyAction="http://tempuri.org/ICustomerService/GetCustomersResponse")]
        System.Threading.Tasks.Task<WpfApplication.ServiceReference2.Customer[]> GetCustomersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/ToggleFailureMode", ReplyAction="http://tempuri.org/ICustomerService/ToggleFailureModeResponse")]
        void ToggleFailureMode();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/ToggleFailureMode", ReplyAction="http://tempuri.org/ICustomerService/ToggleFailureModeResponse")]
        System.Threading.Tasks.Task ToggleFailureModeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/SaveCustomer", ReplyAction="http://tempuri.org/ICustomerService/SaveCustomerResponse")]
        void SaveCustomer(WpfApplication.ServiceReference2.Customer customer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/SaveCustomer", ReplyAction="http://tempuri.org/ICustomerService/SaveCustomerResponse")]
        System.Threading.Tasks.Task SaveCustomerAsync(WpfApplication.ServiceReference2.Customer customer);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerServiceChannel : WpfApplication.ServiceReference2.ICustomerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerServiceClient : System.ServiceModel.ClientBase<WpfApplication.ServiceReference2.ICustomerService>, WpfApplication.ServiceReference2.ICustomerService {
        
        public CustomerServiceClient() {
        }
        
        public CustomerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WpfApplication.ServiceReference2.Customer[] GetCustomers() {
            return base.Channel.GetCustomers();
        }
        
        public System.Threading.Tasks.Task<WpfApplication.ServiceReference2.Customer[]> GetCustomersAsync() {
            return base.Channel.GetCustomersAsync();
        }
        
        public void ToggleFailureMode() {
            base.Channel.ToggleFailureMode();
        }
        
        public System.Threading.Tasks.Task ToggleFailureModeAsync() {
            return base.Channel.ToggleFailureModeAsync();
        }
        
        public void SaveCustomer(WpfApplication.ServiceReference2.Customer customer) {
            base.Channel.SaveCustomer(customer);
        }
        
        public System.Threading.Tasks.Task SaveCustomerAsync(WpfApplication.ServiceReference2.Customer customer) {
            return base.Channel.SaveCustomerAsync(customer);
        }
    }
}
