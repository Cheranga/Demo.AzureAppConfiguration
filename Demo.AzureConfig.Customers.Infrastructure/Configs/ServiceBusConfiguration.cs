namespace Demo.AzureConfig.Customers.Infrastructure.Configs
{
    public class ServiceBusConfiguration
    {
        public string Topic { get; set; }
        public string SendOnlyConnectionString { get; set; }
    }
}