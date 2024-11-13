namespace Store_Products.Service
{
    using Confluent.Kafka;
    using Newtonsoft.Json;
    using ShopClassLibrary.ModelShop;
    using System.Threading.Tasks;

    public class ProductConsumer
    {
        private readonly ConsumerConfig _consumerConfig;

        public ProductConsumer(string bootstrapServers, string groupId)
        {
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Latest // Получать только последние сообщения
            };
        }

        public async Task<Product> GetProductFromKafkaAsync(long productId)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            consumer.Subscribe("ProductUpdates");

            // Слушаем Kafka и ищем сообщение с нужным продуктом
            while (true)
            {
                var consumeResult = consumer.Consume();
                if (consumeResult.Message.Key == productId.ToString())
                {
                    var product = JsonConvert.DeserializeObject<Product>(consumeResult.Message.Value);
                    Console.WriteLine($"Product retrieved from Kafka: {product.Id}");
                    return product;
                }
            }
        }
    }

}
