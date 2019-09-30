using Autofac;
using Newtonsoft.Json;
using NServiceBus;
using NServiceBus.Logging;
using System;

namespace ITG.Brix.Teams.API
{
    public static class CommonNServiceBusConfiguration
    {
        static readonly ILog log = LogManager.GetLogger(typeof(CommonNServiceBusConfiguration));

        public static void ApplyCommonNServiceBusConfiguration(this EndpointConfiguration endpointConfiguration,
            IContainer autofacExternalContainer = null, bool enableMonitoring = true,
            Action<TransportExtensions<RabbitMQTransport>> bridgeConfigurator = null)
        {

            log.Info("Using RabbitMQ Transport");
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
                .ConnectionString("host=localhost")
                .UseConventionalRoutingTopology();

            bridgeConfigurator?.Invoke(transport);

            // Persistence Configuration
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            if (autofacExternalContainer != null)
            {
                endpointConfiguration.UseContainer<AutofacBuilder>(
                    customizations => { customizations.ExistingLifetimeScope(autofacExternalContainer); });
            }

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var serialization = endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            serialization.Settings(settings);

            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            if (enableMonitoring)
            {
                endpointConfiguration.AuditProcessedMessagesTo("audit");
            }
        }
    }
}
