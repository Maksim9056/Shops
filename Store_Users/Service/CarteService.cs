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

        public async Task<List<�arte>> GetCartesByUserIdAsync(long userId)
        {
            try
            {
                var user = await _context.Users.Include(u => u.�artes).FirstOrDefaultAsync(u => u.Id == userId);
                return user?.�artes ?? new List<�arte>();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new List<�arte>(); 
            }
        }

        public async Task AddCarteAsync(�arte carte, long userId)
        {
            try
            {
                var user = await _context.Users.Include(u => u.�artes).FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null) return;

                user.�artes.Add(carte);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
            }
        }

        public async Task UpdateCarteAsync(�arte carte)
        {
            try
            {
                // ����� ������������, ���������� � ������
                var user = await _context.Users.Include(u => u.�artes).FirstOrDefaultAsync(u => u.�artes.Any(c => c.Id == carte.Id));
                if (user == null) return;

                // ����� ������������ �����
                var existingCarte = user.�artes.FirstOrDefault(c => c.Id == carte.Id);
                if (existingCarte == null) return;

                // �������� �������� �����
                _context.Entry(existingCarte).CurrentValues.SetValues(carte);

                // ��������� ���������
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // ����� ����� �������� ����������� ������
            }
        }


        public async Task DeleteCarteAsync(long carteId)
        {
            try
            {
                // ����� ������������, � �������� ���� ��� �����
                var user = await _context.Users.Include(u => u.�artes).FirstOrDefaultAsync(u => u.�artes.Any(c => c.Id == carteId));
                if (user == null) return;

                // ����� �����
                var carte = user.�artes.FirstOrDefault(c => c.Id == carteId);
                if (carte == null) return;

                // ������� ����� �� ������ ������������
                user.�artes.Remove(carte);

                // ������� ����� �� ���������
                _context.�arte.Remove(carte);

                // ��������� ���������
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // ����������� ������ ��� �������������
            }
        }

    }
}