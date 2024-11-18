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
                SecurityProtocol = SecurityProtocol.Plaintext,
                Acks = Acks.All,
                MessageMaxBytes = 10000000 // Уменьшен до 10MB
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task SendProductUpdateAsync(Product product)
        {
            // Исключаем большие данные из JSON, если они не нужны для отправки
            if (product.Id_ProductDataImage != null)
            {
                product.Id_ProductDataImage.OriginalImageData = null; // Или используйте [JsonIgnore] на поле
            }
            if (product.Category_Id?.Image_Category != null)
            {
                product.Category_Id.Image_Category.OriginalImageData = null; // Или используйте [JsonIgnore]
            }

            var productJson = JsonConvert.SerializeObject(product);
            var message = new Message<string, string> { Key = product.Id.ToString(), Value = productJson };

            try
            {
                await _producer.ProduceAsync(_topicName, message);
                Console.WriteLine($"Сообщение с {product.Id} доставлено.");
            }
            catch (ProduceException<string, string> ex)
            {
                Console.WriteLine($"Ошибка при отправке сообщения с ID: {product.Id}. Ошибка: {ex.Error.Reason}");
                // Логика повторной отправки или обработки ошибки
            }
        }

        public async Task SendProductDeleteAsync(long productId)
        {
            var message = new Message<string, string> { Key = productId.ToString(), Value = null };

            try
            {
                await _producer.ProduceAsync(_topicName, message);
                Console.WriteLine($"Сообщение с ID {productId} удалено.");
            }
            catch (ProduceException<string, string> ex)
            {
                Console.WriteLine($"Ошибка при удалении сообщения с ID: {productId}. Ошибка: {ex.Error.Reason}");
                // Логика повторной отправки или обработки ошибки
            }
        }
    }

}
