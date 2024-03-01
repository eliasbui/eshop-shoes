namespace Eshop.Infrastructure.TransactionalOutbox.Darp.Internal
{
    public class DaprTransactionalOutboxOptions
    {
        public static string Name = "DaprTransactionalOutbox";
        public string StateStoreName { get; set; } = "statestore";
        public string OutboxName { get; set; } = "outbox";
    }
}