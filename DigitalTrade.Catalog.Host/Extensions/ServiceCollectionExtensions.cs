using DigitalTrade.Catalog.Api.Contracts.Catalog.Kafka;
using DigitalTrade.Catalog.AppServices.Options;
using DigitalTrade.Catalog.Host.Middlewares;
using KafkaFlow;
using KafkaFlow.Serializer;

namespace DigitalTrade.Catalog.Host.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaFlow(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaOptions = configuration
                               .GetSection(KafkaOptions.Section)
                               .Get<KafkaOptions>()
                           ?? throw new ArgumentNullException(KafkaOptions.Section);

        if (kafkaOptions.Servers.Length == 0)
        {
            throw new ArgumentException("kafkaOptions.Servers.Length == 0");
        }

        return services.AddKafka(kafka => kafka
            .UseConsoleLog()
            .AddCluster(cluster => cluster
                .WithBrokers(kafkaOptions.Servers)
                .CreateTopicIfNotExists(Topics.CatalogChangedName, 1, 1)
                .AddProducer(
                    Topics.CatalogChangedProducerName,
                    producer => producer
                        .DefaultTopic(Topics.CatalogChangedName)
                        .AddMiddlewares(m => m
                            .AddSerializer<JsonCoreSerializer>()
                            .Add<LoggingProducerMiddleware>()
                        )
                )
            )
        );
    }
}