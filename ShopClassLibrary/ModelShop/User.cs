namespace ShopClassLibrary.ModelShop
{
    public class User
    {
       public int Id { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public long Year { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
       
        public string PhoneNumber { get; set; }
        public bool IsBlocked { get; set; } // Признак блокировки пользователя
        public byte [] User_Image { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public Status Status { get; set; } // Статус связи пользователя и прав (например, "Активна", "Отменена")
    }
}
