using NServiceBus;

namespace WcfServices
{
    public interface IProvideMessageSession
    {
        IMessageSession Session { get; set; }
    }
}