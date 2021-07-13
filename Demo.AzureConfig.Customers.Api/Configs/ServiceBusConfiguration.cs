namespace Demo.AzureConfig.Customers.Api.Configs
{
    public class ServiceBusConfiguration
    {
        public string Topic { get; set; }
        public string SendOnlyConnectionString { get; set; }
    }
}