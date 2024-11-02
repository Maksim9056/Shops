using ShopClassLibrary.ModelShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_Test
{
    internal class Data_product
    {
        public static readonly List<Product> Products = new List<Product>
        {
           new Product
           {
              Name_Product = "Смартфон XPhone",
              ProductsDescription = "Мощный смартфон с высококачественной камерой и быстрым процессором",
              ProductCount = 20,
              ProductPrice = 50000,
              Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\xphone.png" },
              Category_Id = new Category(){Name_Category="Электроника"}
           },
           new Product
           {
             Name_Product = "Ноутбук ProMax",
             ProductsDescription = "Легкий и мощный ноутбук для работы и развлечений",
             ProductCount = 15,
             ProductPrice = 75000,
             Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\aple.png" },
             Category_Id = new Category(){Name_Category="Электроника"}
           },
           new Product
           {
             Name_Product = "Наушники SoundX",
             ProductsDescription = "Беспроводные наушники с шумоподавлением",
             ProductCount = 30,
             ProductPrice = 10000,
             Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Наушники SoundX.png" },
             Category_Id = new Category(){Name_Category="Электроника"}
           },
           new Product
           {
             Name_Product = "Умные часы FitTrack",
             ProductsDescription = "Умные часы с отслеживанием физической активности и пульсометра",
             ProductCount = 25,
             ProductPrice = 15000,
             Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Умные часы FitTrack.png" },
             Category_Id = new Category(){Name_Category="Электроника"}
           },
           new Product
           {
            Name_Product = "Портативная колонка BassBoom",
            ProductsDescription = "Портативная Bluetooth колонка с мощным басом",
            ProductCount = 40,
            ProductPrice = 12000,
            Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\BassBoomКолонка.png" },
            Category_Id = new Category(){Name_Category="Электроника"}
           },
          new Product
          {
           Name_Product = "Холодильник CoolTech",
           ProductsDescription = "Энергоэффективный холодильник с функцией быстрой заморозки",
           ProductCount = 10,
           ProductPrice = 60000,
           Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Холодильник CoolTech.png" },
           Category_Id = new Category(){Name_Category="Бытовая техника"}
          },

          new Product
          {
           Name_Product = "Микроволновая печь HeatWave",
           ProductsDescription = "Микроволновая печь с грилем и автопрограммами",
           ProductCount = 8,
           ProductPrice = 8000,
           Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\HeaМикровоновка.png" },
           Category_Id = new Category(){Name_Category="Бытовая техника"}
          },
          new Product
          {
           Name_Product = "Пылесос DustBuster",
           ProductsDescription = "Мощный пылесос с системой фильтрации HEPA",
           ProductCount = 12,
           ProductPrice = 20000,
           Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Пылесос.png" },
           Category_Id = new Category(){Name_Category="Бытовая техника"}
          },
         new Product
         {
          Name_Product = "Кофеварка CoffeeMaster",
          ProductsDescription = "Автоматическая кофеварка с функцией капучино",
          ProductCount = 15,
          ProductPrice = 30000,
          Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Кофеварка.png" },
          Category_Id = new Category(){Name_Category="Бытовая техника"}
         },
         new Product
         {
          Name_Product = "Стиральная машина WashPro",
          ProductsDescription = "Компактная стиральная машина с быстрой стиркой",
          ProductCount = 7,
          ProductPrice = 45000,
          Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Стиральная машина.png" },
          Category_Id = new Category(){Name_Category="Бытовая техника"}
          },

         new Product
         {
          Name_Product = "Куртка FashionStorm",
          ProductsDescription = "Стильная куртка для мужчин, защищающая от ветра и дождя",
          ProductCount = 50,
          ProductPrice = 12000,
          Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Куртка.png" },
          Category_Id = new Category(){Name_Category="Мода"}
         },
         new Product
         {
          Name_Product = "Кроссовки SprintRun",
          ProductsDescription = "Удобные кроссовки для занятий спортом и повседневной носки",
          ProductCount = 60,
          ProductPrice = 7000,
          Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Кроссовки.png" },
          Category_Id = new Category(){Name_Category="Мода"}
         },
         new Product
         {
          Name_Product = "Платье ElegantStyle",
          ProductsDescription = "Элегантное платье для вечерних выходов",
          ProductCount = 30,
          ProductPrice = 15000,
          Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Платье.png" },
          Category_Id = new Category(){Name_Category="Мода"}
         },
        new Product
        {
         Name_Product = "Сумка LeatherBag",
         ProductsDescription = "Кожаная сумка с отделениями для ноутбука и документов",
         ProductCount = 20,
         ProductPrice = 25000,
         Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Сумка.png" },
         Category_Id = new Category(){Name_Category="Мода"}
        },
        new Product
        {
         Name_Product = "Очки SunShield",
         ProductsDescription = "Защитные очки с поляризацией для солнца",
         ProductCount = 100,
         ProductPrice = 5000,
         Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Очки.png" },
         Category_Id = new Category(){Name_Category="Мода"}

        },
        new Product
        {
         Name_Product = "Велотренажер PowerBike",
         ProductsDescription = "Велотренажер с регулируемым уровнем сопротивления",
         ProductCount = 10,
         ProductPrice = 40000,
         Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Велотренажер.png" },
         Category_Id = new Category(){Name_Category="Спорт и фитнес"}
        },

        new Product
        {
         Name_Product = "Йога-мат YogaPro",
         ProductsDescription = "Прочный и нескользящий йога-мат для занятий дома и в зале",
         ProductCount = 50,
         ProductPrice = 3000,
         Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Йога-мат.png" },
         Category_Id = new Category(){Name_Category="Спорт и фитнес"}
        },
        new Product
        {
         Name_Product = "Штанга IronLift",
         ProductsDescription = "Штанга с регулируемым весом для силовых тренировок",
         ProductCount = 5,
         ProductPrice = 20000,
         Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Штанга.png" },
         Category_Id = new Category(){Name_Category="Спорт и фитнес"}
        },
        new Product
        {
         Name_Product = "Фитнес-трекер MoveTrack",
         ProductsDescription = "Фитнес-трекер для отслеживания активности и сна",
         ProductCount = 25,
         ProductPrice = 8000,
         Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Фитнес-трекер.png" },
         Category_Id = new Category(){Name_Category="Спорт и фитнес"}
        },
        new Product
        {
         Name_Product = "Гантели DumbLift",
         ProductsDescription = "Набор гантелей с резиновым покрытием",
         ProductCount = 30,
         ProductPrice = 15000,
         Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Гантели.png" },
         Category_Id = new Category(){Name_Category="Спорт и фитнес"}
         }
        };
    }
}
