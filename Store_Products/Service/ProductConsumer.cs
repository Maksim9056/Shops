namespace Store_Products.Service
{
    using Confluent.Kafka;
    using Confluent.Kafka.Admin;
    using Newtonsoft.Json;
    using ShopClassLibrary.ModelShop;
    using System.Threading.Tasks;

    public class ProductConsumer
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly string _topicName = "ProductUpdates";

        public ProductConsumer(IConfiguration configuration)
        {
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = "product-group",
                AutoOffsetReset = AutoOffsetReset.Earliest, // Чтение с самого начала
                SecurityProtocol = SecurityProtocol.Plaintext, // Подтвердите, что используется PLAINTEXT

                // Увеличиваем максимальный размер сообщения для потребителя до 200 MB
                FetchMaxBytes = 200000000,
                MaxPartitionFetchBytes = 200000000,
                SessionTimeoutMs = 60000,          // Тайм-аут сессии увеличен до 60 секунд
                HeartbeatIntervalMs = 60000,       // Интервал heartbeat увеличен до 20 секунд
                MaxPollIntervalMs = 600000,        // Максимальное время между опросами - 10 минут
            };
            CreateTopicIfNotExists().Wait();

        }

        private async Task CreateTopicIfNotExists()
        {
            var adminConfig = new AdminClientConfig { BootstrapServers = _consumerConfig.BootstrapServers };

            using (var adminClient = new AdminClientBuilder(adminConfig).Build())
            {
                try
                {
                    // Проверка наличия темы
                    var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                    if (!metadata.Topics.Any(t => t.Topic == _topicName))
                    {
                        // Создание темы
                        var topicSpecification = new TopicSpecification
                        {
                            Name = _topicName,
                            NumPartitions = 1,
                            ReplicationFactor = 1
                        };

                        await adminClient.CreateTopicsAsync(new List<TopicSpecification> { topicSpecification });
                        Console.WriteLine($"Тема '{_topicName}' была успешно создана.");
                    }
                    else
                    {
                        Console.WriteLine($"Тема '{_topicName}' уже существует.");
                    }
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"Не удалось создать тему '{_topicName}': {e.Results[0].Error.Reason}");
                }
            }
        }
        public async Task<Product> GetProductFromKafkaAsync(long productId, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            consumer.Subscribe("ProductUpdates");

            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(TimeSpan.FromMilliseconds(500)); // ждем 500 мс
                if (consumeResult == null) continue;

                if (consumeResult.Message.Key == productId.ToString())
                {

                    //Console.WriteLine($"Вернули из {consumeResult.Message.Value}");
                    return JsonConvert.DeserializeObject<Product>(consumeResult.Message.Value);
                }
            }

            return null; // возвращаем null, если не нашли нужное сообщение
        }


        public async Task<List<Product>> GetProductListFromKafkaAsync(long productId, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            consumer.Subscribe("ProductUpdates");

            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(TimeSpan.FromMilliseconds(500)); // ждем 500 мс
                if (consumeResult == null) continue;

                if (consumeResult.Message.Key == productId.ToString())
                {
                    return JsonConvert.DeserializeObject<List<Product>>(consumeResult.Message.Value);
                }
            }

            return null; // возвращаем null, если не нашли нужное сообщение
        }

        public async Task<List<Product>> GetProductsByQueryAsync(string query, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            consumer.Subscribe("ProductUpdates");

            var products = new List<Product>();

            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(TimeSpan.FromMilliseconds(500)); // ждем до 500 мс
                if (consumeResult == null) continue;

                var product = JsonConvert.DeserializeObject<Product>(consumeResult.Message.Value);

                if (product.Name_Product.Contains(query))
                {
                    products.Add(product);
                }

                // Завершаем, если нашли нужное количество совпадений
                if (products.Count >= 10)
                {
                    break;
                }
            }

            return products;
        }



    }


}
