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
        Name_Product = "�������� XPhone",
        ProductsDescription = "������ �������� � ������������������ ������� � ������� �����������",
        ProductCount = 20,
        ProductPrice = 50000,
        
       Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\xphone.png" }
       ,Category_Id = new Category(){Name_Category="�����������"}
    },
    new Product
    {
        Name_Product = "������� ProMax",
        ProductsDescription = "������ � ������ ������� ��� ������ � �����������",
        ProductCount = 15,
        ProductPrice = 75000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\aple.png" }      
        ,Category_Id = new Category(){Name_Category="�����������"}

    },
    new Product
    {
        Name_Product = "�������� SoundX",
        ProductsDescription = "������������ �������� � ���������������",
        ProductCount = 30,
        ProductPrice = 10000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\�������� SoundX.png" }
               ,Category_Id = new Category(){Name_Category="�����������"}

    },
    new Product
    {
        Name_Product = "����� ���� FitTrack",
        ProductsDescription = "����� ���� � ������������� ���������� ���������� � �����������",
        ProductCount = 25,
        ProductPrice = 15000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\����� ���� FitTrack.png" }
               ,Category_Id = new Category(){Name_Category="�����������"}

    },
    new Product
    {
        Name_Product = "����������� ������� BassBoom",
        ProductsDescription = "����������� Bluetooth ������� � ������ �����",
        ProductCount = 40,
        ProductPrice = 12000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\BassBoom�������.png" }
                       ,Category_Id = new Category(){Name_Category="�����������"}

    },
    new Product
    {
        Name_Product = "����������� CoolTech",
        ProductsDescription = "����������������� ����������� � �������� ������� ���������",
        ProductCount = 10,
        ProductPrice = 60000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\����������� CoolTech.png" }
        ,  Category_Id = new Category(){Name_Category="������� �������"}
    },
    new Product
    {
        Name_Product = "������������� ���� HeatWave",
        ProductsDescription = "������������� ���� � ������ � ���������������",
        ProductCount = 8,
        ProductPrice = 8000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\Hea������������.png" }
                ,  Category_Id = new Category(){Name_Category="������� �������"}

    },
    new Product
    {
        Name_Product = "������� DustBuster",
        ProductsDescription = "������ ������� � �������� ���������� HEPA",
        ProductCount = 12,
        ProductPrice = 20000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\�������.png" }
                ,  Category_Id = new Category(){Name_Category="������� �������"}

    },
    new Product
    {
        Name_Product = "��������� CoffeeMaster",
        ProductsDescription = "�������������� ��������� � �������� ��������",
        ProductCount = 15,
        ProductPrice = 30000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\���������.png" }
                ,  Category_Id = new Category(){Name_Category="������� �������"}

    },
    new Product
    {
        Name_Product = "���������� ������ WashPro",
        ProductsDescription = "���������� ���������� ������ � ������� �������",
        ProductCount = 7,
        ProductPrice = 45000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\���������� ������.png" }
                ,  Category_Id = new Category(){Name_Category="������� �������"}

    },
    new Product
    {
        Name_Product = "������ FashionStorm",
        ProductsDescription = "�������� ������ ��� ������, ���������� �� ����� � �����",
        ProductCount = 50,
        ProductPrice = 12000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\������.png" }
                ,  Category_Id = new Category(){Name_Category="����"}

    },
    new Product
    {
        Name_Product = "��������� SprintRun",
        ProductsDescription = "������� ��������� ��� ������� ������� � ������������ �����",
        ProductCount = 60,
        ProductPrice = 7000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\���������.png" }
                        ,  Category_Id = new Category(){Name_Category="����"}

    },
    new Product
    {
        Name_Product = "������ ElegantStyle",
        ProductsDescription = "���������� ������ ��� �������� �������",
        ProductCount = 30,
        ProductPrice = 15000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\������.png" }
                        ,  Category_Id = new Category(){Name_Category="����"}

    },
    new Product
    {
        Name_Product = "����� LeatherBag",
        ProductsDescription = "������� ����� � ����������� ��� �������� � ����������",
        ProductCount = 20,
        ProductPrice = 25000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\�����.png" }
                        ,  Category_Id = new Category(){Name_Category="����"}


    },
    new Product
    {
        Name_Product = "���� SunShield",
        ProductsDescription = "�������� ���� � ������������ ��� ������",
        ProductCount = 100,
        ProductPrice = 5000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\����.png" }
                        ,  Category_Id = new Category(){Name_Category="����"}

    },
    new Product
    {
        Name_Product = "������������ PowerBike",
        ProductsDescription = "������������ � ������������ ������� �������������",
        ProductCount = 10,
        ProductPrice = 40000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\������������.png" }
                        ,  Category_Id = new Category(){Name_Category="����� � ������"}

    },
    new Product
    {
        Name_Product = "����-��� YogaPro",
        ProductsDescription = "������� � ������������ ����-��� ��� ������� ���� � � ����",
        ProductCount = 50,
        ProductPrice = 3000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\����-���.png" }
                                ,  Category_Id = new Category(){Name_Category="����� � ������"}

    },
    new Product
    {
        Name_Product = "������ IronLift",
        ProductsDescription = "������ � ������������ ����� ��� ������� ����������",
        ProductCount = 5,
        ProductPrice = 20000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\������.png" }
                                ,  Category_Id = new Category(){Name_Category="����� � ������"}

    },
    new Product
    {
        Name_Product = "������-������ MoveTrack",
        ProductsDescription = "������-������ ��� ������������ ���������� � ���",
        ProductCount = 25,
        ProductPrice = 8000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\������-������.png" }
                                ,  Category_Id = new Category(){Name_Category="����� � ������"}

    },
    new Product
    {
        Name_Product = "������� DumbLift",
        ProductsDescription = "����� �������� � ��������� ���������",
        ProductCount = 30,
        ProductPrice = 15000,
        Id_ProductDataImage = new ShopClassLibrary.ModelShop.Image { Description = "C:\\Users\\Maks\\Downloads\\�������.png" }
                                ,  Category_Id = new Category(){Name_Category="����� � ������"}

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
                // ���������, ���� ������� ��� ����������
                if (!IsProductPresent(product.Name_Product))
                {
                    // ���� ������� �� ����������, ��������� ���
                    AddProduct(product);

                    // ���������, ��� ������� ��� ��������
                    Assert.IsTrue(IsProductPresent(product.Name_Product), $"{product.Name_Product} was not added successfully.");
                }
                else
                {
                    Console.WriteLine($"{product.Name_Product} ��� ���������� � �������.");
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

            // ������ ������ � ��������
            driver.FindElement(By.CssSelector("input[name='productName']")).SendKeys(product.Name_Product);
            driver.FindElement(By.CssSelector("textarea[name='productDescription']")).SendKeys(product.ProductsDescription);
            driver.FindElement(By.CssSelector("input[name='productCount']")).SendKeys(product.ProductCount.ToString());
            driver.FindElement(By.CssSelector("input[name='productPrice']")).SendKeys(product.ProductPrice.ToString());

            // �������� �����������
            IWebElement fileInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("input[type='file']")));
            fileInput.SendKeys(product.Id_ProductDataImage.Description);

            // ���������� ��������
            //driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }
        private void SelectCategory(Category category)
        {
            string categorySelector = category.Name_Category switch
            {
                "�����������" => "input[value='�����������']",
                "������� �������" => "input[value='������� �������']",
                "����" => "input[value='����']",
                "����� � ������" => "input[value='����� � ������']",
                _ => throw new ArgumentException("����������� ���������")
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