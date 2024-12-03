using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using ShopClassLibrary.ModelShop;
using static System.Net.Mime.MediaTypeNames;

namespace Story_Test
{
    public class Tests
    {
     
     //"C:\\edgedriver_win64\\msedgedriver.exe",
        string CategoryUrl = "https://localhost:7264/products";

        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.EdgeConfig());

            EdgeOptions options = new EdgeOptions();
            //options.AddArgument("--headless"); // Режим без интерфейса
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            driver = new EdgeDriver( options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(CategoryUrl);
        }



        [TearDown]
        public void Cleanup()
        {
            var logs = driver.Manage().Logs.GetLog(LogType.Browser);
            logs.Where(log => log.Level == LogLevel.Warning || log.Level == LogLevel.Severe)
                .ToList()
                .ForEach(log => Console.WriteLine($"{log.Level}: {log.Message}"));

            driver.Quit();
            driver.Dispose();
        }
        

    }
}
//string category = "https://localhost:7264/categories";
//private bool IsProductPresent(string productName, )
//{
//    try
//    {
//        // Пытаемся найти продукт по XPath, чтобы избежать проблем с прокруткой
//        //var elements = driver.FindElements());
//        IWebElement Name_Product = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath($"//td[contains(text(), '{productName}')]")));

//        if (Name_Product.Displayed > 0)
//        {
//            // Если элемент найден, прокручиваем к нему для проверки
//            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", elements[0]);
//            return true;
//        }
//        else
//        {
//            // Альтернативный метод — проверка с помощью JavaScript в случае, если элемент не найден
//            string script = $"return document.body.innerText.includes('{productName}');";
//            return (bool)((IJavaScriptExecutor)driver).ExecuteScript(script);
//        }
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine($"Ошибка при поиске продукта: {e.Message}");
//        return false;
//    }
//}

//private bool IsProductPresent(string productName)
//{
//    string script = $"return document.body.innerText.includes('{productName}');";

//    try
//    {
//        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", script);


//        return (bool)((IJavaScriptExecutor)driver).ExecuteScript(script);
//    }catch(Exception E)
//    {
//        Console.WriteLine(E.Message);
//        return (bool)((IJavaScriptExecutor)driver).ExecuteScript(script);

//    }
//}

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
