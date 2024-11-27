using ShopClassLibrary.ModelShop;
using System.Net.Http.Json;

namespace Shops.Client.Service
{
    public class CarteService
    {

        private readonly HttpClient _httpClient;
        private readonly UrlService _apiKey;
        private readonly string _url;
    
         
        public CarteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _url = _apiKey.GetUserServiceUrl();
        }

        public async Task<List<Сarte>> GetCartesAsync(long userId)
        {
            return await _httpClient.GetFromJsonAsync<List<Сarte>>(_url+$"/UserConroller/users/{userId}/cartes");
        }

        public async Task<Сarte> GetCarteByIdAsync(long carteId)
        {
            return await _httpClient.GetFromJsonAsync<Сarte>(_url + $"/UserConroller/cartes/{carteId}");
        }

        public async Task AddCarteAsync(Сarte carte, long userId)
        {
            await _httpClient.PostAsJsonAsync(_url + $"/UserConroller/users/{userId}/cartes", carte);
        }

        public async Task UpdateCarteAsync(Сarte carte)
        {
            await _httpClient.PutAsJsonAsync(_url + $"/UserConroller/cartes/{carte.Id}", carte);
        }

        public async Task DeleteCarteAsync(long carteId)
        {
            await _httpClient.DeleteAsync(_url + $"/UserConroller/cartes/{carteId}");
        }
    }
}
