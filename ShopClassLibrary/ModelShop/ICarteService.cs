using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public interface ICarteService
    {
        Task<List<Сarte>> GetCartesByUserIdAsync(long userId);
        Task<Сarte> GetCarteByUserAsync(long userId, long carteId); // Новый метод

        Task AddCarteAsync(Сarte carte, long userId);
        Task UpdateCarteAsync(Сarte carte);
        Task DeleteCarteAsync(long carteId);
    }
}