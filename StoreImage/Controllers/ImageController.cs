using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace StoreImage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ShopData _context;
        private readonly ImageService _imageService;

        public ImageController(ShopData context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> ImageId(long Id)
        {
            try
            {


                var Category = await _context.ImageCopy.FirstOrDefaultAsync(u => u.ImageID == Id && u.Resolution == "640x360");
                if (Category == null)
                {
                    return StatusCode(404);
                }
                else
                {
                    return Ok(Category);

                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> ImageCreate([FromForm] IFormFile file, [FromForm] string description = "No description provided", [FromForm] DateTime? uploadDate = null)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is required.");
                }

                byte[] originalImageData;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    originalImageData = memoryStream.ToArray();
                }

                var image = new Image
                {
                    Description = description,
                    UploadDate = uploadDate ?? DateTime.UtcNow, // If not provided, default to current UTC time
                    OriginalImageData = originalImageData,
                    ImageCopies = new List<ImageCopy>()
                };

                var imageCopies = _imageService.CreateImageCopies(image.OriginalImageData);
                foreach (var copy in imageCopies)
                {
                    copy.Image = image; // Set navigation property
                    image.ImageCopies.Add(copy);
                }

                await _context.Image.AddAsync(image);
                await _context.SaveChangesAsync();
                image.OriginalImageData =new  byte[1];
                return Ok(new
                {
                    image.Id,
                    image.Description,
                    image.UploadDate,
                    // Include other relevant properties
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        //[HttpPost("create")]
        //public async Task<ActionResult> ImageCreate([FromBody] ShopClassLibrary.ModelShop.Image image)
        //{
        //    try
        //    {
        //        // Проверка на наличие данных изображения
        //        if (image.OriginalImageData == null || image.OriginalImageData.Length == 0)
        //        {
        //            return BadRequest("Original image data is required.");
        //        }

        //        // Генерация копий изображения
        //        var imageCopies = _imageService.CreateImageCopies(image.OriginalImageData);

        //        // Привязываем созданные копии к оригинальному изображению
        //        foreach (var copy in imageCopies)
        //        {
        //            // Устанавливаем внешний ключ ImageID для каждой копии
        //            copy.ImageID = image.Id;  // EF автоматически установит ImageID после сохранения оригинала
        //            image.ImageCopies.Add(copy);  // Добавляем копию в коллекцию копий у оригинала
        //        }

        //        // Добавляем оригинальное изображение в базу данных
        //        await _context.Image.AddAsync(image);  // Оригинальное изображение добавляется в таблицу Images

        //        // Сохраняем все изменения, включая оригинальное изображение и его копии
        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Image and its copies created successfully", image = image });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Логирование ошибки
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpPost("create")]
        //public async Task<ActionResult> ImageCreate([FromBody] ShopClassLibrary.ModelShop.Image image)
        //{
        //    try
        //    {

        //        // Генерируем копии изображения
        //        var imageCopies = _imageService.CreateImageCopies(image.OriginalImageData);

        //        await _context.Image.AddAsync(image);
        //        await _context.SaveChangesAsync();
        //        return Ok(new { message = "Image created successfully", category = image });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Логирование ошибки
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        //private byte[] CreateImageCopy(byte[] originalImageData, int width, int height)
        //{
        //    using (var ms = new MemoryStream(originalImageData))
        //    {
        //        using (var originalImage = System.Drawing.Image.FromStream(ms))
        //        {
        //            var resizedImage = ResizeImage(originalImage, width, height);
        //            using (var resultStream = new MemoryStream())
        //            {
        //                resizedImage.Save(resultStream, ImageFormat.Jpeg);
        //                return resultStream.ToArray();
        //            }
        //        }
        //    }
        //}

        //private System.Drawing.Image ResizeImage(System.Drawing.Image image, int width, int height)
        //{
        //    var destRect = new Rectangle(0, 0, width, height);
        //    var destImage = new Bitmap(width, height);

        //    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        //    using (var graphics = Graphics.FromImage(destImage))
        //    {
        //        graphics.CompositingMode = CompositingMode.SourceCopy;
        //        graphics.CompositingQuality = CompositingQuality.HighQuality;
        //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphics.SmoothingMode = SmoothingMode.HighQuality;
        //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //        using (var wrapMode = new ImageAttributes())
        //        {
        //            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        //            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        //        }
        //    }

        //    return destImage;
        //}

        //private int GetImageFileSize(byte[] imageData)
        //{
        //    return imageData.Length;
        //}
    }
}
