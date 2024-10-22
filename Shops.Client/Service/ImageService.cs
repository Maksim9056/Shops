using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata;
using ShopClassLibrary.ModelShop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Shops.Client.Service
{
    public class ImageService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlService _apiKey;
        private readonly string _url;
        public ImageService(HttpClient httpClient, UrlService apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _url = _apiKey.GetStoreImage_Products_ServiceUrl();
        }

        public async Task<Image> UploadImageAsync(IBrowserFile file, string description)
        {
            try
            {


                var image = new Image
                {
                    Description = "Some description", // Ensure this field is set correctly
                    UploadDate = DateTime.UtcNow
                };
                image.ImageCopies = new List<ImageCopy>();

                // Using a MultipartFormDataContent to send image and data separately
                var content = new MultipartFormDataContent();

                // Read the file into a byte array
                using (var stream = file.OpenReadStream(maxAllowedSize: 204 * 1024 * 10240)) // 100 MB limit
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    image.OriginalImageData = memoryStream.ToArray();

                    // Add the image file as content

                }
                var fileContent = new ByteArrayContent(image.OriginalImageData);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType ?? "application/octet-stream");
                // "multipart/form-data"
                content.Add(fileContent, "file", file.Name);
                // Add other properties (description, upload date) as string content
                content.Add(new StringContent(image.Description ?? "Default description"), "description"); // Ensure the description is non-empty
                content.Add(new StringContent(image.UploadDate.ToString("o")), "uploadDate");
                //_httpClient.Timeout = TimeSpan.FromMinutes(10); // Increased timeout for slow uploads.PostAsync("api/Images/create", content);


                var result = await _httpClient.PostAsync(_url + "/Image/create", content);

                if (result.IsSuccessStatusCode)
                {

                    var response = await result.Content.ReadFromJsonAsync<Image>();
                    Console.WriteLine($"Image created: {response.Id}");

                    return response;
                }
                else
                {
                    var errorMessage = await result.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Image : {errorMessage}");
                    return new Image { Description = errorMessage };
                }

                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Image { Description = ex.Message };
            }
        }
    }

}
