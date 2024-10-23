namespace StoreImage
{
    using ShopClassLibrary.ModelShop;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Drawing.Processing; // Добавьте это пространство имен для работы с рисунками
    using SixLabors.ImageSharp.Processing;
    using SixLabors.ImageSharp.Drawing;  // Пространство имен для работы с фигурами (геометрия)
    using System.IO;
    using Image = SixLabors.ImageSharp.Image;
    public class ImageService
    {
        // Метод для создания копий изображения с разными разрешениями
        // Метод для создания копий изображения с разными разрешениями
        public List<ImageCopy> CreateImageCopies(byte[] originalImageData)
        {
            var imageCopies = new List<ImageCopy>();

            // Пример генерации копий изображений с разными разрешениями
            imageCopies.Add(new ImageCopy
            {
                Resolution = "1920x1080",
                CopyImageData = ResizeAndFrameImage(originalImageData, 1920, 1080),
                FileSize = ResizeAndFrameImage(originalImageData, 1920, 1080).Length
            });

            imageCopies.Add(new ImageCopy
            {
                Resolution = "1280x720",
                CopyImageData = ResizeAndFrameImage(originalImageData, 1280, 720),
                FileSize = ResizeAndFrameImage(originalImageData, 1280, 720).Length
            });

            imageCopies.Add(new ImageCopy
            {
                Resolution = "640x360",
                CopyImageData = ResizeAndFrameImage(originalImageData, 640, 360),
                FileSize = ResizeAndFrameImage(originalImageData, 640, 360).Length
            });

            return imageCopies;
        }

        // Метод для изменения размера изображения и добавления рамки
        private byte[] ResizeAndFrameImage(byte[] imageData, int width, int height)
        {
            using var image = Image.Load(imageData);  // Загружаем изображение из массива байтов

            // Изменяем размер изображения
            image.Mutate(x => x.Resize(width, height));

            // Добавляем рамку (черную рамку толщиной 10 пикселей)
            var borderThickness = 10;
            var rectangle = new Rectangle(borderThickness / 2, borderThickness / 2, width - borderThickness, height - borderThickness);
            image.Mutate(x => x.Draw(Color.Black, borderThickness, rectangle));

            // Сохраняем результат в поток памяти
            using var ms = new MemoryStream();
            image.SaveAsJpeg(ms);  // Сохраняем изображение как JPEG в поток памяти
            return ms.ToArray();  // Преобразуем поток в массив байтов
        }
    }


}
