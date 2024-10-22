using Microsoft.EntityFrameworkCore;
using ShopClassLibrary.ModelShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary
{
    public class ShopData : DbContext
    {
        public ShopData(DbContextOptions<ShopData> options) : base(options) 
        {

            //Database.EnsureCreated();
            Database.Migrate();
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rights> Rights { get; set; }
        public DbSet<RightsUsers> RightsUsers { get; set; }
        public DbSet<Tasks_Shop> SystemUrls { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Category { get; set; }
        //public DbSet<Product_category> Product_category { get; set; }

        public DbSet<Image> Image { get; set; }
        public DbSet<ImageCopy> ImageCopy { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {



                // Инициализация данных для статусов пользователей
                modelBuilder.Entity<Status>().HasData(
                    new Status { Id = 1, StatusName = "Новый пользователь", Description = "Пользователь только что зарегистрировался" },
                    new Status { Id = 2, StatusName = "Активный", Description = "Пользователь активен в системе" },
                    new Status { Id = 3, StatusName = "Ожидание подтверждения", Description = "Пользователь ожидает подтверждения email" },
                    new Status { Id = 4, StatusName = "Заблокирован", Description = "Пользователь заблокирован из-за нарушения политики" },
                    new Status { Id = 5, StatusName = "Удален", Description = "Пользователь удалил свою учетную запись" },
                   new Status { Id = 6, StatusName = "Новый товар", Description = "Товар в наличии" },
                   new Status { Id = 7, StatusName = "Удален товар", Description = "Товар не вналичии" }

                );
                // Настройка связи один-ко-многим между Image и ImageCopy
                modelBuilder.Entity<Image>()
                    .HasMany(i => i.ImageCopies)
                    .WithOne(ic => ic.Image)
                    .HasForeignKey(ic => ic.ImageID);

                base.OnModelCreating(modelBuilder);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Связь между Order и User (многие ко одному)
        //    modelBuilder.Entity<Order>()
        //        .HasOne(o => o.User)
        //        .WithMany(u => u.Orders)
        //        .HasForeignKey(o => o.User);

        //    // Связь между Order и Status (многие ко одному)
        //    modelBuilder.Entity<Order>()
        //        .HasOne(o => o.Status)
        //        .WithMany(s => s.Orders)
        //        .HasForeignKey(o => o.StatusId);

        //    // Связь между Product и Order (многие ко многим)
        //    modelBuilder.Entity<Order>()
        //        .HasMany(o => o.Products)
        //        .WithMany(p => p.Orders);

        //    // Связь между User и RightsUsers (один ко многим)
        //    modelBuilder.Entity<RightsUsers>()
        //        .HasOne(ru => ru.Userы)
        //        .WithMany(u => u.RightsUsers)
        //        .HasForeignKey(ru => ru.UserId);

        //    // Связь между Rights и RightsUsers (один ко многим)
        //    modelBuilder.Entity<RightsUsers>()
        //        .HasOne(ru => ru.Rights)
        //        .WithMany(r => r.RightsUsers)
        //        .HasForeignKey(ru => ru.RightsId);

        //    // Связь между Tasks_Shop и Project (многие ко одному)
        //    modelBuilder.Entity<Tasks_Shop>()
        //        .HasOne(t => t.Project)
        //        .WithMany(p => p.Tasks)
        //        .HasForeignKey(t => t.ProjectId);
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Устанавливаем связь один ко многим между SystemAccount и SystemUrl
        //    modelBuilder.Entity<SystemAccount>()
        //        .HasMany(sa => sa.Urls)
        //        .WithOne()
        //        .HasForeignKey(su => su.SystemAccountId);
        //}

    }
}
