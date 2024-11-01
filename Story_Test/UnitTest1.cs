using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using ShopClassLibrary.ModelShop;
using static System.Net.Mime.MediaTypeNames;

namespace Story_Test
{
    public class Tests
    {
        private static readonly List<Product> Products = new List<Product>
{
    new Product
    {
        Name_Product = "Смартфон XPhone",
        ProductsDescription = "Мощный смартфон с высококачественной камерой и быстрым процессором",
        ProductCount = 20,
        ProductPrice = 50000,
        
       Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\xphone.png" }
       ,Category_Id = new Category(){Name_Category="Электроника"}
    },
    new Product
    {
        Name_Product = "Ноутбук ProMax",
        ProductsDescription = "Легкий и мощный ноутбук для работы и развлечений",
        ProductCount = 15,
        ProductPrice = 75000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\aple.png" }      
        ,Category_Id = new Category(){Name_Category="Электроника"}

    },
    new Product
    {
        Name_Product = "Наушники SoundX",
        ProductsDescription = "Беспроводные наушники с шумоподавлением",
        ProductCount = 30,
        ProductPrice = 10000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Наушники SoundX.png" }
               ,Category_Id = new Category(){Name_Category="Электроника"}

    },
    new Product
    {
        Name_Product = "Умные часы FitTrack",
        ProductsDescription = "Умные часы с отслеживанием физической активности и пульсометра",
        ProductCount = 25,
        ProductPrice = 15000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Умные часы FitTrack.png" }
               ,Category_Id = new Category(){Name_Category="Электроника"}

    },
    new Product
    {
        Name_Product = "Портативная колонка BassBoom",
        ProductsDescription = "Портативная Bluetooth колонка с мощным басом",
        ProductCount = 40,
        ProductPrice = 12000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\BassBoomКолонка.png" }
                       ,Category_Id = new Category(){Name_Category="Электроника"}

    },
    new Product
    {
        Name_Product = "Холодильник CoolTech",
        ProductsDescription = "Энергоэффективный холодильник с функцией быстрой заморозки",
        ProductCount = 10,
        ProductPrice = 60000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Холодильник CoolTech.png" }
        ,  Category_Id = new Category(){Name_Category="Бытовая техника"}
    },
    new Product
    {
        Name_Product = "Микроволновая печь HeatWave",
        ProductsDescription = "Микроволновая печь с грилем и автопрограммами",
        ProductCount = 8,
        ProductPrice = 8000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\HeaМикровоновка.png" }
                ,  Category_Id = new Category(){Name_Category="Бытовая техника"}

    },
    new Product
    {
        Name_Product = "Пылесос DustBuster",
        ProductsDescription = "Мощный пылесос с системой фильтрации HEPA",
        ProductCount = 12,
        ProductPrice = 20000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Пылесос.png" }
                ,  Category_Id = new Category(){Name_Category="Бытовая техника"}

    },
    new Product
    {
        Name_Product = "Кофеварка CoffeeMaster",
        ProductsDescription = "Автоматическая кофеварка с функцией капучино",
        ProductCount = 15,
        ProductPrice = 30000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Кофеварка.png" }
                ,  Category_Id = new Category(){Name_Category="Бытовая техника"}

    },
    new Product
    {
        Name_Product = "Стиральная машина WashPro",
        ProductsDescription = "Компактная стиральная машина с быстрой стиркой",
        ProductCount = 7,
        ProductPrice = 45000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Стиральная машина.png" }
                ,  Category_Id = new Category(){Name_Category="Бытовая техника"}

    },
    new Product
    {
        Name_Product = "Куртка FashionStorm",
        ProductsDescription = "Стильная куртка для мужчин, защищающая от ветра и дождя",
        ProductCount = 50,
        ProductPrice = 12000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Куртка.png" }
                ,  Category_Id = new Category(){Name_Category="Мода"}

    },
    new Product
    {
        Name_Product = "Кроссовки SprintRun",
        ProductsDescription = "Удобные кроссовки для занятий спортом и повседневной носки",
        ProductCount = 60,
        ProductPrice = 7000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Кроссовки.png" }
                        ,  Category_Id = new Category(){Name_Category="Мода"}

    },
    new Product
    {
        Name_Product = "Платье ElegantStyle",
        ProductsDescription = "Элегантное платье для вечерних выходов",
        ProductCount = 30,
        ProductPrice = 15000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Платье.png" }
                        ,  Category_Id = new Category(){Name_Category="Мода"}

    },
    new Product
    {
        Name_Product = "Сумка LeatherBag",
        ProductsDescription = "Кожаная сумка с отделениями для ноутбука и документов",
        ProductCount = 20,
        ProductPrice = 25000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Сумка.png" }
                        ,  Category_Id = new Category(){Name_Category="Мода"}


    },
    new Product
    {
        Name_Product = "Очки SunShield",
        ProductsDescription = "Защитные очки с поляризацией для солнца",
        ProductCount = 100,
        ProductPrice = 5000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Очки.png" }
                        ,  Category_Id = new Category(){Name_Category="Мода"}

    },
    new Product
    {
        Name_Product = "Велотренажер PowerBike",
        ProductsDescription = "Велотренажер с регулируемым уровнем сопротивления",
        ProductCount = 10,
        ProductPrice = 40000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Велотренажер.png" }
                        ,  Category_Id = new Category(){Name_Category="Спорт и фитнес"}

    },
    new Product
    {
        Name_Product = "Йога-мат YogaPro",
        ProductsDescription = "Прочный и нескользящий йога-мат для занятий дома и в зале",
        ProductCount = 50,
        ProductPrice = 3000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Йога-мат.png" }
                                ,  Category_Id = new Category(){Name_Category="Спорт и фитнес"}

    },
    new Product
    {
        Name_Product = "Штанга IronLift",
        ProductsDescription = "Штанга с регулируемым весом для силовых тренировок",
        ProductCount = 5,
        ProductPrice = 20000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Штанга.png" }
                                ,  Category_Id = new Category(){Name_Category="Спорт и фитнес"}

    },
    new Product
    {
        Name_Product = "Фитнес-трекер MoveTrack",
        ProductsDescription = "Фитнес-трекер для отслеживания активности и сна",
        ProductCount = 25,
        ProductPrice = 8000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Фитнес-трекер.png" }
                                ,  Category_Id = new Category(){Name_Category="Спорт и фитнес"}

    },
    new Product
    {
        Name_Product = "Гантели DumbLift",
        ProductsDescription = "Набор гантелей с резиновым покрытием",
        ProductCount = 30,
        ProductPrice = 15000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Гантели.png" }
                                ,  Category_Id = new Category(){Name_Category="Спорт и фитнес"}

    }
};
        string CategoryUrl = "https://localhost:7264/products";

        protected IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            EdgeOptions options = new EdgeOptions();
            driver = new EdgeDriver("C:\\edgedriver_win64\\msedgedriver.exe", options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(CategoryUrl);
        }

        [Test]
        public void TestProductAddition()
        {
            foreach (var product in Products)
            {
                // Проверяем, если продукт уже существует
                if (!IsProductPresent(product.Name_Product))
                {
                    // Если продукт не существует, добавляем его
                    AddProduct(product);

                    // Проверяем, что продукт был добавлен
                    Assert.IsTrue(IsProductPresent(product.Name_Product), $"{product.Name_Product} was not added successfully.");
                }
                else
                {
                    Console.WriteLine($"{product.Name_Product} уже существует в системе.");
                }
            }
        }

        private bool IsProductPresent(string productName)
        {
            try
            {
                return driver.FindElement(By.XPath($"//td[contains(text(), '{productName}')]")) != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private void AddProduct(Product product)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("body > div.page > main > article > button"))).Click();
            SelectCategory(product.Category_Id);

            // Вводим данные о продукте
            driver.FindElement(By.CssSelector("input[name='productName']")).SendKeys(product.Name_Product);
            driver.FindElement(By.CssSelector("textarea[name='productDescription']")).SendKeys(product.ProductsDescription);
            driver.FindElement(By.CssSelector("input[name='productCount']")).SendKeys(product.ProductCount.ToString());
            driver.FindElement(By.CssSelector("input[name='productPrice']")).SendKeys(product.ProductPrice.ToString());

            // Загрузка изображения
            IWebElement fileInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("input[type='file']")));
            fileInput.SendKeys(product.Id_ProductDataImage.Description);

            // Сохранение продукта
            //driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }
        private void SelectCategory(Category category)
        {
            string categorySelector = category.Name_Category switch
            {
                "Электроника" => "input[value='Электроника']",
                "Бытовая техника" => "input[value='Бытовая техника']",
                "Мода" => "input[value='Мода']",
                "Спорт и фитнес" => "input[value='Спорт и фитнес']",
                _ => throw new ArgumentException("Неизвестная категория")
            };

            IWebElement categoryCheckbox = driver.FindElement(By.CssSelector(categorySelector));
            if (!categoryCheckbox.Selected)
            {
                categoryCheckbox.Click();
            }
        }
        //string category = "https://localhost:7264/categories";

        //[Test]
        ////[SetUp]
        //public void Setup()
        //{
        //    int S = Product.Count;
        //    EdgeOptions options = new EdgeOptions();
        //    driver = new EdgeDriver("C:\\edgedriver_win64\\msedgedriver.exe", options);
        //    driver.Manage().Window.Maximize();
        //    //driver.Navigate().GoToUrl("https://localhost:7264/");
        //    driver.Navigate().GoToUrl(category);
        //    //UploadFileTest();
        //}

        //public void UploadFileTest()
        //{
        //    // Wait until the button is clickable, then click
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("body > div.page > main > article > button"))).Click();

        //    // Locate the file input field again to avoid StaleElementReferenceException
        //    IWebElement fileInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("body > div.page > main > article > form > div:nth-child(3) > input[type=file]")));
        //    fileInput.SendKeys("C:\\Users\\Maks\\Downloads\\aple.png");


        //}

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}