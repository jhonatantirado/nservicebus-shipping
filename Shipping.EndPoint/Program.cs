using MySql.Data.MySqlClient;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using System;
using System.Threading.Tasks;

namespace Shipping.EndPoint
{
    class Program
    {
        static async Task Main()
        {
            var endPointName = "Shipping.EndPoint";
            Console.Title = endPointName;
            var endpointConfiguration = new EndpointConfiguration(endPointName);

            //var transport = endpointConfiguration.UseTransport<LearningTransport>();
            //var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

            // Configure RabbitMQ transport
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            string rabbitmqUrl = Environment.GetEnvironmentVariable("RABBITMQ_PCF_NSB_URL");
            transport.ConnectionString(rabbitmqUrl);

            // Configure persistence
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MySql>();
            string mysqlConnectionString = Environment.GetEnvironmentVariable("MYSQL_AWS_NSB_URL");
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new MySqlConnection(mysqlConnectionString);
                }
            );

            // Enable the Outbox.
            endpointConfiguration.EnableOutbox();

            // Make sure NServiceBus creates queues in RabbitMQ, tables in MYSQL Server, etc.
            // You might want to turn this off in production, so that DevOps can use scripts to create these.
            endpointConfiguration.EnableInstallers();

            // Turn on auditing.
            //endpointConfiguration.AuditProcessedMessagesTo("audit");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
