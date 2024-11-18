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
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SecurityProtocol = SecurityProtocol.Plaintext,
                FetchMaxBytes = 10485760,              // Увеличение до 10MB для ускорения потребления
                MaxPartitionFetchBytes = 10485760,     // Увеличено до 10MB
                SessionTimeoutMs = 30000,              // Тайм-аут оставлен на 30 секунд
                HeartbeatIntervalMs = 10000,           // Интервал heartbeat оставлен на 10 секунд
                MaxPollIntervalMs = 600000             // Максимальное время между опросами - 10 минут
            };


            // Проверка на существование топика только при запуске
            CreateTopicIfNotExists().Wait();
        }

        private async Task CreateTopicIfNotExists()
        {
            var adminConfig = new AdminClientConfig { BootstrapServers = _consumerConfig.BootstrapServers };
            using (var adminClient = new AdminClientBuilder(adminConfig).Build())
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));
                if (!metadata.Topics.Any(t => t.Topic == _topicName))
                {
                    var topicSpecification = new TopicSpecification
                    {
                        Name = _topicName,
                        NumPartitions = 5,             // Увеличено до 5 для повышения производительности
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
        }
        public async Task<Product> GetProductFromKafkaAsync(long productId, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            consumer.Subscribe(_topicName);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // Используем тайм-аут вместо cancellationToken в Consume
                    var consumeResult = consumer.Consume(TimeSpan.FromSeconds(10)); // Увеличиваем тайм-аут до 10 секунд
                    if (consumeResult == null)
                    {
                        // Логируем пропуск из-за тайм-аута
                        Console.WriteLine("Timeout while waiting for message.");
                        continue;
                    }

                    if (consumeResult.Message.Key == productId.ToString())
                    {
                        return JsonConvert.DeserializeObject<Product>(consumeResult.Message.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                // Логируем все другие исключения
                Console.WriteLine($"Error while consuming message for Product ID: {productId}: {ex.Message}");

                return null;
            }

            // Возвращаем null, если не найден продукт с указанным productId
            return null;
        }

        public async Task<List<Product>> GetProductListFromKafkaAsync(long productId, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            consumer.Subscribe(_topicName);

            var products = new List<Product>();

            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(cancellationToken);
                if (consumeResult?.Message?.Key == productId.ToString())
                {
                    products.Add(JsonConvert.DeserializeObject<Product>(consumeResult.Message.Value));
                }
                if (products.Count >= 10) break; // прерываем цикл, если собрано достаточно результатов
            }
            return products;
        }

        public async Task<List<Product>> GetProductsByQueryAsync(string query, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            consumer.Subscribe(_topicName);

            var products = new List<Product>();

            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(cancellationToken);
                if (consumeResult == null) continue;

                var product = JsonConvert.DeserializeObject<Product>(consumeResult.Message.Value);
                if (product.Name_Product.Contains(query))
                {
                    products.Add(product);
                }

                if (products.Count >= 10) break; // прекращаем поиск после 10 совпадений
            }

            return products;
        }
    }



}



