using System;

namespace WpfApplication.ViewModels
{
    public class CustomerModel
    {
        protected bool Equals(CustomerModel other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomerModel) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(CustomerModel left, CustomerModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CustomerModel left, CustomerModel right)
        {
            return !Equals(left, right);
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}