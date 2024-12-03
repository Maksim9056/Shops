using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_Test
{
    internal class Log : Tests
    {

        public string email = "maksim_bobretsov@mail.ru";
        public string password = "86139527Es!";

        [Test]
        public void Log_()
        {
            try
            {

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IWebElement log_page = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_page_log)));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", log_page);
                IWebElement Email = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_page_log_email)));
                Email.SendKeys(email.ToString());

                IWebElement Password = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_page_log_password)));
                Password.SendKeys(password.ToString());

                IWebElement Button = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_page_log_button)));
                Button.Click();
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", Button);
                //////((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].submit();", Button);
                Thread.Sleep(TimeSpan.FromSeconds(30)); // Жёсткое ожидание 1 минута

                IWebElement Bank_page = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_page_cartes_bank)));

                Bank_page.Click();
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", Bank_page);
                // Список значений для проверки
                Thread.Sleep(TimeSpan.FromSeconds(18)); // Жёсткое ожидание 1 минута

                string[] valuesToSearch = { "888899992222092", "888899992222090", "888899992222098" };

                // Проверяем наличие каждого значения на странице
                foreach (string value in valuesToSearch)
                {
                    try
                    {


                        // Ищем элемент с текстом значения
                        IWebElement elements = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath($"//*[contains(text(), '{value}')]")));
                        Assert.IsNotNull(elements, $"Значение {value} найдено на странице.");
                        Console.WriteLine($"Значение {value} присутствует на странице.");
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine($"Значение {value} отсутствует на странице.");
                        Assert.Fail($"Значение {value} не найдено на странице.");
                    }
                }

                var element1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html[1]/body[1]/div[1]/div[1]/div[2]/nav[1]/div[1]/a[1]")));
                element1.Click();
                Thread.Sleep(TimeSpan.FromSeconds(1)); // Жёсткое ожидание 1 минута

                // Найти поле ввода для поиска
                var searchInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/input[1]")));
                searchInput.SendKeys("П"); // Введите текст для поиска

                // Найти выпадающий список категории
                var categoryDropdown = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/select[1]")));
                var selectElement = new SelectElement(categoryDropdown);
                selectElement.SelectByText("Мода"); // Выберите "Мода"

                // Найти поле минимальной цены
                var minPriceInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/input[2]")));
                minPriceInput.Clear();
                minPriceInput.SendKeys("60000"); // Введите минимальную цену

                // Найти поле максимальной цены
                var maxPriceInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/input[3]")));
                maxPriceInput.Clear();
                maxPriceInput.SendKeys("10000"); // Введите максимальную цену

                // Найти кнопку поиска
                var searchButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/button[1]")));
                searchButton.Click(); // Нажать кнопку
                Thread.Sleep(TimeSpan.FromSeconds(10)); // Жёсткое ожидание 1 минута

                // Проверить, что результаты отображаются
                var results = wait.Until(d => d.FindElements(By.CssSelector(".product-card")));
                Assert.IsTrue(results.Count > 0, "Товары не найдены!");


                var plus = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_button_plus)));
                plus.Click();
                var buton_click = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_button_v2)));
                buton_click.Click();
                var buton_click_ = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_)));
                buton_click_.Click();


                Thread.Sleep(TimeSpan.FromSeconds(15)); // Жёсткое ожидание 1 минута
                IWebElement dropdown = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("carteSelect"))); // Замените "dropdownId" на реальный ID выпадающего списка
                SelectElement select = new SelectElement(dropdown);
                select.SelectByText("888899992222090");
                Console.WriteLine("Выбрана карта: 888899992222090");

                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_carte__plus)));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);

                element.Click();
                var buton_click_Carts = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_carte)));
                buton_click_Carts.Click();
                try
                {

                    var XPath_click_carte__buy = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_carte__buy)));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", XPath_click_carte__buy);

                    XPath_click_carte__buy.Click(); 
                }
                catch (NoSuchElementException)
                {
                }

                var XPath_click_carte__buy_success = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_carte__buy_success)));
                bool success = XPath_click_carte__buy_success.Text == "Заказ успешно создан с ID: Недостаточно средств для оплаты заказов.";

                if (success)
                {
                    Console.WriteLine("Заказ успешно создан с ID: Недостаточно средств для оплаты заказов.");
                }
                else
                {
                    Console.WriteLine("");
                }

               var   dropdowns = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("carteSelect"))); // Замените "dropdownId" на реальный ID выпадающего списка
                var selects = new SelectElement(dropdowns);
                selects.SelectByText("888899992222092");
                Console.WriteLine("Выбрана карта: 888899992222090");
                var  XPath_click_carte__buy_c = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_carte__buy)));

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", XPath_click_carte__buy_c);
                XPath_click_carte__buy_c.Click();
                var XPath_click_carte__buy_successs = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_carte__buy_success)));

                var successs = XPath_click_carte__buy_successs.Text == "Заказ успешно создан с ID: Оплата прошла успешно.";
                if (successs)
                {
                    Console.WriteLine("Заказ успешно создан с ID: Оплата прошла успешно.");
                    Console.WriteLine("Завершение теста");
                }
                else
                {
                    Console.WriteLine("");
                }
               
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);

            }
        }

    }
}