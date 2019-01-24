using System;
using System.Runtime.Serialization;

namespace WcfServices
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}