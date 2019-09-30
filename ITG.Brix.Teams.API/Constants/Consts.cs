namespace ITG.Brix.Teams.API.Constants
{
    public static class Consts
    {
        public static class Configuration
        {
            public const string Id = "ITG.Brix.Teams";
            public const string ConnectionString = "ConnectionString";
            public const string AzureServiceBusEnabled = "AzureServiceBusEnabled";
            public const string AzureServiceBusConnectionString = "AzureServiceBusConnectionString";
            public const string RabbitMQEnabled = "RabbitMQEnabled";
            public const string RabbitMQConnectionString = "RabbitMQConnectionString";
        }
        public static class Config
        {
            public const string ConfigurationPackageObject = "Config";

            public static class Environment
            {
                public const string Section = "Environment";
                public const string Param = "ASPNETCORE_ENVIRONMENT";
            }

            public static class Database
            {
                public const string Section = "Database";
                public const string Param = "DatabaseConnectionString";
            }

            internal static class AzureServiceBusTransport
            {
                internal const string Section = "AzureServiceBus";
                internal const string Status = "Status";
                internal const string ConnectionString = "ConnectionString";
            }

            internal static class RabbitMQTransport
            {
                internal const string Section = "RabbitMQ";
                internal const string Status = "Status";
                internal const string ConnectionString = "ConnectionString";
            }
        }
    }
}
