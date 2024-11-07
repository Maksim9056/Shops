using ShopClassLibrary.ModelShop;
using ShopClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace Store_Orders.Service
{
    public class UserService
    {
        private readonly ShopData _context;

        public UserService(ShopData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetUserWithDetailsAsync(long userId)
        {
            var user = await _context.Users.Include(u => u.Status).Include(u => u.Id_User_Image).FirstOrDefaultAsync(u => u.Id == userId);
            var orders = _context.Orders
                .Include(o => o.User).Include(o => o.Status)
                .Where(o => o.User == user).ToList();

            Parallel.ForEach(orders, product =>
            {
                //if (product.Id_ProductDataImage != null)
                //{
                product.User.Id_User_Image.OriginalImageData = new byte[0];
                //}
                product.User.Id_User_Image.ImageCopies = new List<ImageCopy>();
                //if (product.Category_Id?.Image_Category != null)
                //{
                product.User.Orders = new List<Order>();
                //}
            });
            return orders;
        }
    }

}
