namespace RabbitMQ.Shared;

public static class PubSubSettings
{
    public const string Url = "amqp://guest:guest@localhost:5672";

    public static class Queue
    {
        public const string Default = "demo-queue";
        public const string Direct = "demo-direct-queue";
    }

    public static class Exchange
    {
        public const string DirectExchange = "demo-direct-exchange";
    }

    public static class Routing
    {
        public const string RoutingKey1 = "account.init";
    }
}
