using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Store_Users.Service
{
    public class CarteService : ICarteService
    {
        private readonly ShopData _context;

        public CarteService(ShopData context)
        {
            _context = context;
        }

        public async Task<List<Сarte>> GetCartesByUserIdAsync(long userId)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Сartes).FirstOrDefaultAsync(u => u.Id == userId);
                return user?.Сartes ?? new List<Сarte>();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new List<Сarte>(); 
            }
        }

        public async Task AddCarteAsync(Сarte carte, long userId)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Сartes).FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null) return;

                user.Сartes.Add(carte);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
            }
        }

        public async Task UpdateCarteAsync(Сarte carte)
        {
            try
            {
                // Найти пользователя, связанного с картой
                var user = await _context.Users.Include(u => u.Сartes).FirstOrDefaultAsync(u => u.Сartes.Any(c => c.Id == carte.Id));
                if (user == null) return;

                // Найти существующую карту
                var existingCarte = user.Сartes.FirstOrDefault(c => c.Id == carte.Id);
                if (existingCarte == null) return;

                // Обновить свойства карты
                _context.Entry(existingCarte).CurrentValues.SetValues(carte);

                // Сохранить изменения
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибок
            }
        }


        public async Task DeleteCarteAsync(long carteId)
        {
            try
            {
                // Найти пользователя, у которого есть эта карта
                var user = await _context.Users.Include(u => u.Сartes).FirstOrDefaultAsync(u => u.Сartes.Any(c => c.Id == carteId));
                if (user == null) return;

                // Найти карту
                var carte = user.Сartes.FirstOrDefault(c => c.Id == carteId);
                if (carte == null) return;

                // Удалить карту из списка пользователя
                user.Сartes.Remove(carte);

                // Удалить карту из контекста
                _context.Сarte.Remove(carte);

                // Сохранить изменения
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибок при необходимости
            }
        }

    }
}