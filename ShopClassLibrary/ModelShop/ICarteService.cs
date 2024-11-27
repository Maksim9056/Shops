using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public interface ICarteService
    {
        Task<List<Ñarte>> GetCartesByUserIdAsync(long userId);
        Task AddCarteAsync(Ñarte carte, long userId);
        Task UpdateCarteAsync(Ñarte carte);
        Task DeleteCarteAsync(long carteId);
    }
}