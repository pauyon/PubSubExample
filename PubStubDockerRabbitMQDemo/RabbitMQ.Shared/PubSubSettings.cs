namespace RabbitMQ.Shared;

public static class PubSubSettings
{
    public const string Url = "amqp://guest:guest@localhost:5672";

    public static class Queue
    {
        public const string Default = "demo-queue";
        public const string Direct = "demo-direct-queue";
        public const string Topic = "demo-topic-queue";
    }

    public static class Exchange
    {
        public const string DirectExchange = "demo-direct-exchange";
        public const string TopicExchange = "demo-topic-exchange";
    }

    public static class Routing
    {
        public const string RoutingKey1 = "account.init";
        public const string WildCardKey = "account.*";
    }
}
