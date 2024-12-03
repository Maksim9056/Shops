using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopClassLibrary.ModelShop;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace Story_Test
{
    internal class Product_test: Tests
    {


        [Test] 
        public void TestProductAddition()
        {
            try
            {


                foreach (var product in Data_product.Products)
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                    // Проверяем, если продукт уже существует
                    if (!IsProductPresent(product.Name_Product, wait))
                    {
                        IWebElement fileInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_click_product_button_add)));
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", fileInput);

                        //fileInput.Click();
                        // Если продукт не существует, добавляем его
                        AddProduct(product, wait);

                        //Thread.Sleep(2000); // 2 секунды ожидания (можете изменить на WebDriverWait для лучших результатов)

                        // Проверяем, что продукт был добавлен
                        Assert.IsTrue(IsProductPresent(product.Name_Product, wait), $"{product.Name_Product} was not added successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"{product.Name_Product} уже существует в системе.");
                    }
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);

            }
        }


        private void AddProduct(Product product, WebDriverWait wait)
        {
            try
            {

                // Выбираем категорию
                SelectCategory(product.Category_Id);
                IWebElement Name_Product = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_Name_Product)));
                Name_Product.SendKeys(product.Name_Product);
                // Вводим данные о продукте
                //driver.FindElement(By.XPath()).SendKeys();


                IWebElement ProductsDescription = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_ProductsDescription)));
                ProductsDescription.SendKeys(product.ProductsDescription);
                IWebElement ProductCount = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_ProductCount)));

                ProductCount.SendKeys(product.ProductCount.ToString());
                IWebElement ProductPrice = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(ProductPageSelectors.XPath_ProductPrice)));

                ProductPrice.SendKeys(product.ProductPrice.ToString());
        

                // Загрузка изображения
                IWebElement fileInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(ProductPageSelectors.CssSelector_Fille)));
                fileInput.SendKeys(product.Id_ProductDataImage.Description);
                Thread.Sleep(TimeSpan.FromMinutes(1)); // Жёсткое ожидание 1 минута

                // Прокручиваем к кнопке
                IWebElement saveButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("saveButton")));

                Console.WriteLine($"Кнопка найдена: {saveButton != null}");
                Console.WriteLine($"Enabled: {saveButton.Enabled}, Displayed: {saveButton.Displayed}");

                // Если элемент перекрыт
                bool isObstructed = (bool)((IJavaScriptExecutor)driver).ExecuteScript(@"
                 var element = arguments[0];
                 var rect = element.getBoundingClientRect();
                 var centerX = rect.left + rect.width / 2;
                 var centerY = rect.top + rect.height / 2;
                 var topElement = document.elementFromPoint(centerX, centerY);
                 return topElement !== element;", saveButton);

                if (isObstructed)
                {
                    Console.WriteLine("Элемент перекрыт.");
                    // Удаление перекрывающего элемента
                    IWebElement blocker = driver.FindElement(By.CssSelector("перекрывающий элемент")); // Замените селектор
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].remove();", blocker);
                }

                // Попробуйте обычный клик
                try
                {
                    //saveButton.Click();
                    //Console.WriteLine("Клик выполнен.");
                    //Actions actions = new Actions(driver);
                    //actions.MoveToElement(saveButton).Click().Perform();
                    //Console.WriteLine("Клик через Actions выполнен.");
                    //Console.WriteLine($"Клик через Actions не удался:");
                    //Console.WriteLine("Пробую JavaScript-клик...");
                    // Клик через JavaScript
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", saveButton);
                    //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].submit();", saveButton);

                    Console.WriteLine("Клик через JavaScript выполнен.");
                    //var form = driver.FindElement(By.TagName("form"));
                    //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].submit();", form);
                    Console.WriteLine("Форма отправлена напрямую.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Обычный клик не удался: {ex.Message}");

                    // Попробуйте клик через Actions
               
                }
             

                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", saveButton);

                //// Проверяем, что кнопка готова
                //Assert.IsTrue(saveButton.Enabled, "Кнопка 'Сохранить' заблокирована.");
                //Assert.IsTrue(saveButton.Displayed, "Кнопка 'Сохранить' не отображается.");




                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].focus();", saveButton);

                // ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", saveButton);
                // saveButton.Submit();

                //// Нажатие кнопки "Сохранить" через ID
                //IWebElement saveButton = driver.FindElement(By.Id("saveButton"));
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", saveButton);

                // Сохранение продукта с использованием JavaScript для клика
                //IWebElement saveButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(ProductPageSelectors.XPath_saveButton)));
                //saveButton.Click();
                //IWebElement saveButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit' and text()='Сохранить']")));
                //saveButton.Click();
                // Нажатие кнопки "Сохранить"
                //var saveButton = driver.FindElement(By.XPath("//button[@type='submit' and text()='Сохранить']"));
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", saveButton);
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", saveButton);


                // Проверка успешного редиректа или сообщения
                wait.Until(ExpectedConditions.UrlContains("/products")); // Проверяем, что произошло перенаправление
                Assert.IsTrue(driver.Url.Contains("/products"), "Продукт не был создан успешно.");
            
                // Сохранение продукта
            }
            catch (Exception)
            {

            }
        }


        private void SelectCategory(Category category)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Определяем селектор для каждой категории
                string categorySelector = category.Name_Category switch
                {
                    "Электроника" => ProductPageSelectors.CssSelector_SelectCategory_Электроника,
                    "Бытовая техника" => ProductPageSelectors.CssSelector_SelectCategory_Бытовая_техника,
                    "Мода" => ProductPageSelectors.CssSelector_SelectCategory_Мода,
                    "Спорт и фитнес" => ProductPageSelectors.CssSelector_SelectCategory_Спорт_и_фитнес,
                };


                // Находим чекбокс и кликаем, если он еще не выбран
                IWebElement categoryCheckbox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(categorySelector)));
                if (!categoryCheckbox.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", categoryCheckbox);
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Не удалось найти чекбокс для категории: {category.Name_Category}");
            }
        }
        private bool IsProductPresent(string productName, WebDriverWait wait)
        {
            try
            {
                // Используем XPath для поиска ячейки таблицы с текстом продукта
                IWebElement productElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath($"//td[contains(text(), '{productName}')]")));
                return productElement != null;
            }
            catch (WebDriverTimeoutException)
            {
                // Если элемент не найден в течение времени ожидания
                Console.WriteLine($"Продукт с именем {productName} не найден на странице.");
                return false;
            }
        }
    }
}
