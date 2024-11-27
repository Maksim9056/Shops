using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public interface ICarteService
    {
        Task<List<�arte>> GetCartesByUserIdAsync(long userId);
        Task AddCarteAsync(�arte carte, long userId);
        Task UpdateCarteAsync(�arte carte);
        Task DeleteCarteAsync(long carteId);
    }
}