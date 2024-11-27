using ShopClassLibrary.ModelShop;
using System.Net.Http.Json;

namespace Shops.Client.Service
{
    public class CarteService
    {

        private readonly HttpClient _httpClient;
        private readonly UrlService _apiKey;
        private readonly string _url;
    
         
        public CarteService(HttpClient httpClient, UrlService apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _url = _apiKey.GetUserServiceUrl();
        }

        public async Task<List<Сarte>> GetCartesAsync(long userId)
        {
            return await _httpClient.GetFromJsonAsync<List<Сarte>>(_url+$"/api/users/{userId}/cartes");
        }

        public async Task<Сarte> GetCarteByIdAsync(long User_id,long carteId)
        {
            return await _httpClient.GetFromJsonAsync<Сarte>(_url + $"/api/users/{User_id}/cartes/{carteId}");
        }

        public async Task AddCarteAsync(Сarte carte, long userId)
        {
            await _httpClient.PostAsJsonAsync(_url + $"/api/users/{userId}/cartes", carte);
        }

        public async Task UpdateCarteAsync(long userId ,Сarte carte)
        {
            await _httpClient.PutAsJsonAsync(_url + $"/api/users/{userId}/cartes/{carte.Id}", carte);
        }

        public async Task DeleteCarteAsync(long userId, long carteId)
        {
            await _httpClient.DeleteAsync(_url + $"/api/users/{userId}/cartes/{carteId}");
        }
    }
}
