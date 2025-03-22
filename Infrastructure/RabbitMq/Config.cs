namespace RabbitMq;

public static class Config
{
    public static string HostName = Environment.GetEnvironmentVariable("RabbitMQ_HostName") ?? "localhost";
    public static int Port = int.Parse(Environment.GetEnvironmentVariable("RabbitMQ_Port") ?? "5672");
    public static string UserName = Environment.GetEnvironmentVariable("RabbitMQ_UserName") ?? "guest";
    public static string Password = Environment.GetEnvironmentVariable("RabbitMQ_Password") ?? "guest";

    public static string ExchangeName = "notifications_exchange";
    public static string QueueName = "notifications_queue";
    public static string RoutingKey = "notification.execution";

    public static bool DurableExchange = true;
    public static bool DurableQueue = true;
    public static bool PersistentMessages = true;
}