using Confluent.Kafka;
using Newtonsoft.Json;
using ShopClassLibrary.ModelShop;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Store_Products.Service
{
    public class ProductProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topicName = "ProductUpdates";

        public ProductProducer(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                SecurityProtocol = SecurityProtocol.Plaintext, // Подтвердите, что используется PLAINTEXT
                Acks = Acks.All, // Требуем подтверждение от всех реплик для надёжной доставки
                MessageMaxBytes = 200000000 // Устанавливаем максимальный размер сообщения
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task SendProductUpdateAsync(Product product)
        {
            var productJson = JsonConvert.SerializeObject(product);
            var message = new Message<string, string> { Key = product.Id.ToString(), Value = productJson };
            var deliveryResult = await _producer.ProduceAsync(_topicName, message);
            Console.WriteLine($"Сообщение '{deliveryResult.Value}' доставлено в {deliveryResult.TopicPartitionOffset}");
            await _producer.ProduceAsync("ProductUpdates", message);
        }

        public async Task SendProductDeleteAsync(long productId)
        {
            var message = new Message<string, string> { Key = productId.ToString(), Value = null };
            await _producer.ProduceAsync("ProductUpdates", message);
        }
    }

}
