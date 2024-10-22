namespace StoreImage
{
    using ShopClassLibrary.ModelShop;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;
    using System.IO;
    using Image = SixLabors.ImageSharp.Image;

    public class ImageService
    {
        // Метод для создания копий изображения с разными разрешениями
        //public List<byte[]> CreateImageCopies(byte[] imageData)
        //{
        //    var imageCopies = new List<byte[]>();

        //    // Создаем копии для различных разрешений
        //    imageCopies.Add(ResizeImage(imageData, 1920, 1080));  // Для монитора
        //    imageCopies.Add(ResizeImage(imageData, 1280, 720));   // Для планшета
        //    imageCopies.Add(ResizeImage(imageData, 640, 360));    // Для мобильных устройств

        //    return imageCopies;
        //}
        public List<ImageCopy> CreateImageCopies(byte[] originalImageData)
        {
            var imageCopies = new List<ImageCopy>();

            // Пример генерации копий изображений с разными разрешениями
            imageCopies.Add(new ImageCopy
            {
                Resolution = "1920x1080",
                CopyImageData = ResizeImage(originalImageData, 1920, 1080),
                FileSize = ResizeImage(originalImageData, 1920, 1080).Length
            });

            imageCopies.Add(new ImageCopy
            {
                Resolution = "1280x720",
                CopyImageData = ResizeImage(originalImageData, 1280, 720),
                FileSize = ResizeImage(originalImageData, 1280, 720).Length
            });

            imageCopies.Add(new ImageCopy
            {
                Resolution = "640x360",
                CopyImageData = ResizeImage(originalImageData, 640, 360),
                FileSize = ResizeImage(originalImageData, 640, 360).Length
            });

            return imageCopies;
        }

        // Метод для изменения размера изображения
        private byte[] ResizeImage(byte[] imageData, int width, int height)
        {
            using var image = Image.Load(imageData);  // Загружаем изображение из массива байтов
            image.Mutate(x => x.Resize(width, height));  // Изменяем размер изображения

            using var ms = new MemoryStream();
            image.SaveAsJpeg(ms);  // Сохраняем изображение как JPEG в поток памяти
            return ms.ToArray();  // Преобразуем поток в массив байтов
        }
    }

}
